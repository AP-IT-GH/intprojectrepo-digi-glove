#include <stdio.h>
#include <stdlib.h>
#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "driver/gpio.h"
#include "driver/adc.h"
#include "esp_adc_cal.h"


#define DEFAULT_VREF    1100        //Use adc2_vref_to_gpio() to obtain a better estimate

#define FILTERING 1
#define FILTER_SIZE 25

static esp_adc_cal_characteristics_t *adc_chars;
static const adc1_channel_t channel_1 = ADC_CHANNEL_0;      //GPIO36 (VP)
static const adc1_channel_t channel_2 = ADC_CHANNEL_3;      //GPIO39 (VN)
static const adc_atten_t atten = ADC_ATTEN_DB_6;            //150 to 1750 mV
static const adc_unit_t unit = ADC_UNIT_1;


void set_muxs(uint8_t address);

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
    } else {
        printf("eFuse Vref characterized: ERROR\n");
    } 
}

void sensors_task(void* ignore)
{   
    sensors_init();

    uint32_t curTime = 0;
    uint32_t prevTime = 0;
    uint16_t adc_reading[16];
    uint16_t voltage[16];
    uint16_t fcontainer[16][FILTER_SIZE] = {0};
    while (1) {
        if (esp_timer_get_time() - prevTime >= 45000) {
            curTime = esp_timer_get_time();
            for (int i = 0; i < 8; i++) {
                adc_reading[i] = adc1_get_raw(channel_1);
                adc_reading[i+8] = adc1_get_raw(channel_2);
                set_muxs((i+1) % 8);
                voltage[i] = esp_adc_cal_raw_to_voltage(adc_reading[i], adc_chars);
                voltage[i+8] = esp_adc_cal_raw_to_voltage(adc_reading[i+8], adc_chars);
            }
            #if FILTERING
                    uint32_t sum[16] = {0};
                    for (int k = 0; k < 16; k++) {
                        for (int i = FILTER_SIZE - 1; i > 0; i--) {
                            fcontainer[k][i] = fcontainer[k][i-1];
                            sum[k] += fcontainer[k][i];
                        } 
                        sum[k] += voltage[k];
                        fcontainer[k][0] = voltage[k];
                        voltage[k] = sum[k] / FILTER_SIZE;
                    }  
            #endif
            // generate_packet();
            // bt_send();
            printf("--ADC1-- Raw: %d\tVoltage: %dmV\n", adc_reading[0], voltage[0]);
            printf("--ADC1-- Raw: %d\tVoltage: %dmV\n", adc_reading[8], voltage[8]);
            
            printf("Microseconds: %d\n", curTime - prevTime);
            //printf("--ADC2-- Raw: %d\tVoltage: %dmV\n", adc_reading_2, voltage_2);
            printf("\r\n");
            prevTime = curTime;
        }
        vTaskDelay(1);
    }
}

void set_muxs(uint8_t address) {

    switch (address)
    {
    case 0:
        /* set pins */
        break;
    case 1:
        /* set pins */
        break;
    case 2:
        /* set pins */
        break;
    case 3:
        /* set pins */
        break;
    case 4:
        /* set pins */
        break;
    case 5:
        /* set pins */
        break;
    case 6:
        /* set pins */
        break;
    case 7:
        /* set pins */
        break;
    default:
        break;
    }
}