#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "freertos/queue.h"
#include "driver/gpio.h"

#include "pins.h"
#include "RGB_led.h"

#define GPIO_INPUT_PIN_SEL  ((1ULL<<BTN_1) | (1ULL<<BTN_2))
#define ESP_INTR_FLAG_DEFAULT 0
#define FREQUENCY 5 // 5 = 200Hz, 10 = 100Hz

static xQueueHandle gpio_evt_queue = NULL;
volatile int64_t prevTime_1 = 0;
volatile int64_t prevTime_2 = 0;

static void IRAM_ATTR gpio_isr_handler_1(void* arg)
{
    uint32_t gpio_num = (uint32_t) arg;
    if((esp_timer_get_time() - prevTime_1) > 1000000) {
        xQueueSendFromISR(gpio_evt_queue, &gpio_num, NULL);
        prevTime_1 = esp_timer_get_time();
        }
}
static void IRAM_ATTR gpio_isr_handler_2(void* arg)
{
    uint32_t gpio_num = (uint32_t) arg;
    if((esp_timer_get_time() - prevTime_2) > 1000000) {
        xQueueSendFromISR(gpio_evt_queue, &gpio_num, NULL);
        prevTime_2 = esp_timer_get_time();
        }
}

void buttons_task(void* ignore)
{
    gpio_config_t io_conf;
    io_conf.intr_type = GPIO_PIN_INTR_POSEDGE;              //interrupt of rising edge
    io_conf.pin_bit_mask = GPIO_INPUT_PIN_SEL;              //bit mask of the pins
    io_conf.mode = GPIO_MODE_INPUT;                         //set as input mode
    io_conf.pull_down_en = 1; io_conf.pull_up_en = 0;       //disable pulldown and pullup                 
    gpio_config(&io_conf);                                  //configure GPIO with the given settings
    gpio_evt_queue = xQueueCreate(40, sizeof(uint32_t));    //create a queue to handle gpio event from isr

    gpio_install_isr_service(ESP_INTR_FLAG_DEFAULT);        //install gpio isr service
    gpio_isr_handler_add(BTN_1, gpio_isr_handler_1, (void*) BTN_1);
    gpio_isr_handler_add(BTN_2, gpio_isr_handler_2, (void*) BTN_2);

    uint32_t io_num;
    const TickType_t xFrequency = FREQUENCY;
    TickType_t xLastWakeTime = xTaskGetTickCount();
    
    while(1) {
        if(xQueueReceive(gpio_evt_queue, &io_num, 0)) {
            if(gpio_get_level(io_num)) {
                if(io_num == BTN_1) {
                    /* Stuff here */
                    rgb_set(5, 0, 0, 100);  //test example
                }
                else if(io_num == BTN_2) {
                    /* Stuff here */
                    rgb_set(0, 5, 0, 100);  //test example
                }
            }      
        }
        vTaskDelayUntil(&xLastWakeTime, xFrequency);
    }
}
