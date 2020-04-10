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

#define FREQUENCY 10 // 5 = 200Hz, 10 = 100Hz

#define ACCEL_X_OFFSET -6989
#define ACCEL_Y_OFFSET -7580
#define ACCEL_Z_OFFSET 9726
#define GYRO_X_OFFSET -21
#define GYRO_Y_OFFSET -17
#define GYRO_Z_OFFSET -58

#define OUTPUT_READABLE 1
#define OUTPUT_PACKET 1

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
	
void imu_init(MPU6050 imu) {
	vTaskDelay(1000);
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

	imu.CalibrateAccel(6);
    imu.CalibrateGyro(6);
	
	//imu.PrintActiveOffsets();

	imu.setDMPEnabled(true);
}

void imu_task(void* ignore) {
	MPU6050 imu = MPU6050();
	imu_init(imu);
	imu_data_t packet;
	uint8_t fifoBuffer[64];
	const TickType_t xFrequency = FREQUENCY;

	TickType_t xLastWakeTime = xTaskGetTickCount();
	while(1) {
		if (imu.dmpGetCurrentFIFOPacket(fifoBuffer)) { 	// Get the Latest packet 
		#if OUTPUT_PACKET
			packet.capture_time = esp_timer_get_time();
			packet.data[0] = fifoBuffer[0] << 8 | fifoBuffer[1];	// quaternion W component
			packet.data[1] = fifoBuffer[4] << 8 | fifoBuffer[5];	// quaternion X component
			packet.data[2] = fifoBuffer[8] << 8 | fifoBuffer[9];	// quaternion Y component
			packet.data[3] = fifoBuffer[12] << 8 | fifoBuffer[13];	// quaternion Z component
			packet.data[4] = fifoBuffer[22] << 8 | fifoBuffer[23];	// acceleration X component
			packet.data[5] = fifoBuffer[24] << 8 | fifoBuffer[25];	// acceleration Y component
			packet.data[6] = fifoBuffer[26] << 8 | fifoBuffer[27];	// acceleration Z component

			bt_create_packet(NULL,&packet);
		#endif
			
		}

		vTaskDelayUntil(&xLastWakeTime, xFrequency);
	}
}