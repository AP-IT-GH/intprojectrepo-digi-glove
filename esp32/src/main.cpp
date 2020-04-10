/*
    * Copyright (c) 2020 All rights reserved.
    * 
    * This work is licensed under the terms of the MIT license.  
    * For a copy, see <https://opensource.org/licenses/MIT>
*/
#include "freertos/FreeRTOS.h"
#include "sdkconfig.h"
#include "freertos/task.h"
#include "freertos/semphr.h"
#include "freertos/queue.h"

#include "bt_spp.h"

extern "C" {
	void app_main(void);
    void sensors_task(void* ignore);
    void dummydata_task(void* ignore);
}


extern void imu_task(void* ignore);
extern void task_initI2C(void* ignore);
    
void app_main(void)
{
    bt_init(BT_SERVER_NAME);    // Not a legit way of doing things
    xTaskCreate(task_initI2C, "mpu_task", 2048, NULL, configMAX_PRIORITIES, NULL);
    //xTaskCreate(sensors_task, "sensors_task", 6144, NULL, configMAX_PRIORITIES, NULL);
    xTaskCreate(imu_task, "imu_task", 4096, NULL, configMAX_PRIORITIES, NULL);
    //xTaskCreate(dummydata_task, "sensors_task", 2048, NULL, configMAX_PRIORITIES-1, NULL);

}