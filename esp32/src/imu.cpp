/*
    * Copyright (c) 2020 All rights reserved.
    * 
    * This work is licensed under the terms of the MIT license.  
    * For a copy, see <https://opensource.org/licenses/MIT>
*/
#include <freertos/FreeRTOS.h>
#include <freertos/task.h>
#include <esp_log.h>
#include <esp_err.h>
#include <driver/i2c.h>
#include "MPU6050.h"
#include "MPU6050_6Axis_MotionApps_V6_12.h"
#include "sdkconfig.h"
#include "pins.h"
#include "bt_spp.h"
#include "RGB_led.h"

#define FREQUENCY 1 // 5 = 200Hz, 10 = 100Hz    1000Hz now, because trying to circumevent the bug where reading sometimes takes half a second

#define ACCEL_X_OFFSET -6989
#define ACCEL_Y_OFFSET -7580
#define ACCEL_Z_OFFSET 9726
#define GYRO_X_OFFSET -21
#define GYRO_Y_OFFSET -17
#define GYRO_Z_OFFSET -58

#define OUTPUT_READABLE 1
#define OUTPUT_PACKET 1

extern int connected;
volatile int btn_1_flag = 0;
void imu_calibration(void);

void task_initI2C(void *ignore) {
    i2c_config_t conf;
    conf.mode = I2C_MODE_MASTER;
    conf.sda_io_num = (gpio_num_t)IMU_SDA;
    conf.scl_io_num = (gpio_num_t)IMU_SCL;
    conf.sda_pullup_en = GPIO_PULLUP_ENABLE;
    conf.scl_pullup_en = GPIO_PULLUP_ENABLE;
    conf.master.clk_speed = 400000;
    ESP_ERROR_CHECK(i2c_param_config(I2C_NUM_0, &conf));
    ESP_ERROR_CHECK(i2c_driver_install(I2C_NUM_0, I2C_MODE_MASTER, 0, 0, 0));
    vTaskDelete(NULL);
}

void imu_task(void* ignore) {
    
    MPU6050 imu = MPU6050();
    uint8_t devId = imu.getDeviceID();
    if (devId <= 57 && devId != 0)
        printf("MPU6050 connected succesfully, device ID = %d \r\n", devId);
    else
        printf("ERROR: MPU6050 might be connected succesfully but, device ID = %d \r\n", devId);
    imu.initialize();
    imu.dmpInitialize();
    imu.setXAccelOffset(ACCEL_X_OFFSET);
    imu.setYAccelOffset(ACCEL_Y_OFFSET);
    imu.setZAccelOffset(ACCEL_Z_OFFSET);
    imu.setYGyroOffset(GYRO_X_OFFSET);
    imu.setZGyroOffset(GYRO_X_OFFSET);
    imu.setXGyroOffset(GYRO_X_OFFSET);
    imu_calibration();
    imu.setDMPEnabled(true);
    imu_data_t packet;
    uint8_t fifoBuffer[64];
    const TickType_t xFrequency = FREQUENCY;
    TickType_t xLastWakeTime = xTaskGetTickCount();
    int curTime = 0;
    int prevTime = 0;
    int tTime = 0;
    float div = 16384.0;

    while(1) {
        if (btn_1_flag) {
            imu_calibration();
            btn_1_flag = 0;
        }
        if (imu.dmpGetCurrentFIFOPacket(fifoBuffer)) { // Get the Latest packet 
            curTime = esp_timer_get_time();
            tTime = curTime - prevTime;
            packet.capture_time = esp_timer_get_time(); 
            packet.data[0] = fifoBuffer[0] << 8 | fifoBuffer[1];	// quaternion W component
            packet.data[1] = fifoBuffer[4] << 8 | fifoBuffer[5];	// quaternion X component
            packet.data[2] = fifoBuffer[8] << 8 | fifoBuffer[9];	// quaternion Y component
            packet.data[3] = fifoBuffer[12] << 8 | fifoBuffer[13];	// quaternion Z component
            packet.data[4] = fifoBuffer[16] << 8 | fifoBuffer[17];	// acceleration X component
            packet.data[5] = fifoBuffer[18] << 8 | fifoBuffer[19];	// acceleration Y component
            packet.data[6] = fifoBuffer[20] << 8 | fifoBuffer[21];	// acceleration Z component


            bt_create_packet(NULL,&packet);
            //printf("%d \r\n", curTime);
            printf("W = %f \t X = %f \t Y = %f \t Z = %f\r\n", packet.data[0] / div,packet.data[1]/ div,packet.data[2]/ div,packet.data[3]/ div);
            prevTime = curTime;
        }
        vTaskDelayUntil(&xLastWakeTime, xFrequency);
        //vTaskDelay(10);
    }
}

void imu_calibration(void) {
    rgb_set(30, 20, 0, 500);
    MPU6050 imu = MPU6050();
    imu.initialize();               
    imu.dmpInitialize();
    imu.PrintActiveOffsets();
    imu.CalibrateAccel(6);
    imu.CalibrateGyro(6);
    imu.PrintActiveOffsets();
    imu.setDMPEnabled(true);
    if (connected)
        rgb_set(0, 20, 0, 500);
    else
        rgb_set(30, 20, 0, 500);
}