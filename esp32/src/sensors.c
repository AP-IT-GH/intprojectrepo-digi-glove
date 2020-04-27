/*
    * Copyright (c) 2020 All rights reserved.
    * 
    * This work is licensed under the terms of the MIT license.  
    * For a copy, see <https://opensource.org/licenses/MIT>
*/
#include <stdio.h>
#include <stdlib.h>
#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "driver/gpio.h"
#include "driver/adc.h"
#include "esp_adc_cal.h"
#include "math.h"

#include "pins.h"
#include "bt_spp.h"

#define FREQUENCY 10 // 5 = 200Hz, 10 = 100Hz

#define DEFAULT_VREF    1100                // Use adc2_vref_to_gpio() to obtain a better estimate
#define RESOLUTION 8                        // Output resolution bits
#define MUX_MAX_ADDRESS 7                   // 13 elements and 2 8bit muxes
#define OFFSET MUX_MAX_ADDRESS
#define NUM_OF_ELEMENTS 13

/* Filter settings */
#define FILTERING 1
#define FILTER_SIZE 15
#define SELFCALIBRATING 1

static esp_adc_cal_characteristics_t *adc_chars;
static const adc1_channel_t channel_1 = MUX_1_Y;
static const adc1_channel_t channel_2 = MUX_2_Y;
static const adc_atten_t atten = ADC_ATTEN_DB_11;   // 150 to 2450 mV
static const adc_unit_t unit = ADC_UNIT_1;

volatile int btn_2_flag = 0;

void set_muxs(uint8_t address);
void reset_calibration(void);

int maxValue[NUM_OF_ELEMENTS];             // Pressing a button will...
int minValue[NUM_OF_ELEMENTS];             // ...reset these

void sensors_init(void)
{
   if (esp_adc_cal_check_efuse(ESP_ADC_CAL_VAL_EFUSE_VREF) == ESP_OK) {
        printf("eFuse Vref: OK\n");
    } else {
        printf("eFuse Vref: ERROR\n");
    }
    /* Configure ADC */
    adc1_config_width(ADC_WIDTH_BIT_12);
    adc1_config_channel_atten(channel_1, atten);
    adc1_config_channel_atten(channel_2, atten);

    /* Characterize ADC */
    adc_chars = calloc(1, sizeof(esp_adc_cal_characteristics_t));
    esp_adc_cal_value_t val_type = esp_adc_cal_characterize(unit, atten, ADC_WIDTH_BIT_12, DEFAULT_VREF, adc_chars);
    if (val_type == ESP_ADC_CAL_VAL_EFUSE_VREF) {
        printf("eFuse Vref characterized: OK\n");
        /* MUX 1*/
        gpio_pad_select_gpio(MUX_1_A); 
        gpio_pad_select_gpio(MUX_1_B); 
        gpio_pad_select_gpio(MUX_1_C);
        gpio_set_direction(MUX_1_A, GPIO_MODE_OUTPUT);
        gpio_set_direction(MUX_1_B, GPIO_MODE_OUTPUT);
        gpio_set_direction(MUX_1_C, GPIO_MODE_OUTPUT);
        /* MUX 2*/
        gpio_pad_select_gpio(MUX_2_A); 
        gpio_pad_select_gpio(MUX_2_B); 
        gpio_pad_select_gpio(MUX_2_C);
        gpio_set_direction(MUX_2_A, GPIO_MODE_OUTPUT);
        gpio_set_direction(MUX_2_B, GPIO_MODE_OUTPUT);
        gpio_set_direction(MUX_2_C, GPIO_MODE_OUTPUT);
    } else {
        printf("eFuse Vref characterized: ERROR\n");
    }
}

void sensors_task(void* ignore)
{   
    sensors_init();
    set_muxs(0);
    const TickType_t xFrequency = FREQUENCY;
    sensor_data_t packet;
    uint16_t adc_reading[NUM_OF_ELEMENTS];
    uint16_t voltage[NUM_OF_ELEMENTS];
    uint16_t fcontainer[NUM_OF_ELEMENTS][FILTER_SIZE] = {0};
    int outputMax = pow(2,RESOLUTION) - 1;

    /* Initialize calibration arrays */
    //xTaskCreate(reset_calibration_task, "reset_calibration_task", 2048, NULL, configMAX_PRIORITIES, NULL);
    reset_calibration();

    TickType_t xLastWakeTime = xTaskGetTickCount();
    while (1) {
        if (btn_2_flag) {
            reset_calibration();
            btn_2_flag = 0;
        }
        /* Had to seperate these, because ADC was not working with them in the same loop */
        for (int i = 0; i < MUX_MAX_ADDRESS; i++) {
            adc_reading[i] = adc1_get_raw(channel_1);               // Mux 1
            set_muxs((i+1) % MUX_MAX_ADDRESS);                      // Increments both muxes, not a problem
            voltage[i] = esp_adc_cal_raw_to_voltage(adc_reading[i], adc_chars);
            
        }
        set_muxs(0);
        for (int i = 0; i < MUX_MAX_ADDRESS - 1; i++) {             // Only 6 sensors in mux 2
            adc_reading[i + OFFSET] = adc1_get_raw(channel_2);      // Mux 2
            set_muxs((i+1) % MUX_MAX_ADDRESS);                      
            voltage[i + OFFSET] = esp_adc_cal_raw_to_voltage(adc_reading[i + OFFSET], adc_chars);

        }
        set_muxs(0);
        packet.capture_time = esp_timer_get_time();

        #if FILTERING
                uint32_t sum[NUM_OF_ELEMENTS] = {0};
                for (int k = 0; k < NUM_OF_ELEMENTS; k++) {
                    for (int i = FILTER_SIZE - 1; i > 0; i--) {
                        fcontainer[k][i] = fcontainer[k][i-1];
                        sum[k] += fcontainer[k][i];
                    } 
                    sum[k] += voltage[k];
                    fcontainer[k][0] = voltage[k];
                    voltage[k] = sum[k] / FILTER_SIZE;
                }
        #endif
        #if SELFCALIBRATING
            for (int i = 0; i < NUM_OF_ELEMENTS; i++) {
                if (voltage[i] > maxValue[i])
                    maxValue[i] = voltage[i];
                else if (voltage[i] < minValue[i])
                    minValue[i] = voltage[i];
                packet.data[i] = round((((float)voltage[i] - (float)minValue[i]) / ((float)maxValue[i] - (float)minValue[i]) * outputMax));
            }
        #else
            for (int i = 0; i < NUM_OF_ELEMENTS; i++) {
                packet.data[i] = round((float)((voltage[i] - MIN_VALUE) / (MAX_VALUE - MIN_VALUE) * outputMax));
            }
        #endif

        bt_create_packet(&packet,NULL);


        /* DEBUG PRINTS HERE */
        //printf("Value = %d \t Voltage = %d \t MIN = %d \t MAX = %d\r\n", packet.data[0], voltage[0],minValue[0],maxValue[0]);
        //printf("Value = %d \t Voltage = %d \t MIN = %d \t MAX = %d\r\n", packet.data[4], voltage[4],minValue[4],maxValue[4]);

        vTaskDelayUntil(&xLastWakeTime, xFrequency);
    }
}

void set_muxs(uint8_t address)
{
    /* MUX 1*/
    gpio_set_level(MUX_1_C, (address >> 2) & 0x1);
    gpio_set_level(MUX_1_B, (address >> 1) & 0x1);
    gpio_set_level(MUX_1_A, address & 0x1);
    /* MUX 2 */
    gpio_set_level(MUX_2_C, (address >> 2) & 0x1);
    gpio_set_level(MUX_2_B, (address >> 1) & 0x1);
    gpio_set_level(MUX_2_A, address & 0x1);
}

void reset_calibration(void) {
    for (int i = 0; i < NUM_OF_ELEMENTS; i++) {
        maxValue[i] = -4000;
        minValue[i] = 5000;
    }
}