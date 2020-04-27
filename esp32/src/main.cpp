/*
    * Copyright (c) 2020 All rights reserved.
    * 
    * This work is licensed under the terms of the MIT license.  
    * For a copy, see <https://opensource.org/licenses/MIT>
*/
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "freertos/semphr.h"
#include "freertos/queue.h"
#include "driver/gpio.h"
#include "sdkconfig.h"

#include "RGB_led.h"
#include "bt_spp.h"
#include "pins.h"

extern "C" {
    void app_main(void);
    void sensors_task(void* ignore);
    void dummydata_task(void* ignore);
    void buttons_task(void* ignore);
}
extern void imu_task(void* ignore);
extern void task_initI2C(void* ignore);
    
void app_main(void)
{
    rgb_init();
    rgb_set(20, 0, 0, 100);

    bt_init(BT_SERVER_NAME);    // Not a legit way of doing things

    xTaskCreate(task_initI2C, "task_initI2C", 2048, NULL, configMAX_PRIORITIES, NULL);
    vTaskDelay(500/portTICK_PERIOD_MS);

    xTaskCreate(buttons_task, "buttons_task", 4096, NULL, configMAX_PRIORITIES, NULL);
    vTaskDelay(100/portTICK_PERIOD_MS);

    xTaskCreate(imu_task, "imu_task", 6144, NULL, configMAX_PRIORITIES, NULL);
    vTaskDelay(100/portTICK_PERIOD_MS);

    xTaskCreate(sensors_task, "sensors_task", 6144, NULL, configMAX_PRIORITIES, NULL);
    
    //xTaskCreate(dummydata_task, "dummydata_task", 4096, NULL, configMAX_PRIORITIES, NULL);
}