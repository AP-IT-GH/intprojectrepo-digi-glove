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

#include "bt_spp.h"

#define GENERATE_IMU 1
#define GENERATE_SENSOR 0

#define FREQUENCY 5 // 5 = 200Hz, 10 = 100Hz  

void dummydata_task(void* ignore)
 {
     const TickType_t xFrequency = FREQUENCY;
     TickType_t xLastWakeTime = xTaskGetTickCount();
     while(1) {
     #if GENERATE_IMU
        imu_data_t imuPacket;
        imuPacket.capture_time = esp_timer_get_time();
        for (int j = 0; j < IMU_VAR_COUNT; j++)
            imuPacket.data[j] = 0;
        bt_create_packet(NULL,&imuPacket);
     #endif

     #if GENERATE_SENSOR
        sensor_data_t sensPacket;
        sensPacket.capture_time = esp_timer_get_time();
        for (int j = 0; j < SENSOR_DATA_SIZE; j++)
            sensPacket.data[j] = 0;
        bt_create_packet(&sensPacket,NULL);
     #endif

     vTaskDelayUntil(&xLastWakeTime, xFrequency);
     }
 }
 