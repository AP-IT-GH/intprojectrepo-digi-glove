/*
 * Display.c
 *
 *  Created on: 14.08.2017
 *      Author: darek
 */
#include <driver/i2c.h>
#include <esp_log.h>
#include <esp_err.h>
#include <freertos/FreeRTOS.h>
#include <freertos/task.h>
#include "MPU6050.h"
#include "MPU6050_6Axis_MotionApps_V6_12.h"
//#include "MPU6050_6Axis_MotionApps20.h"
#include "sdkconfig.h"

#define PIN_SDA 21
#define PIN_CLK 22
#define OUTPUT_READABLE_WORLDACCEL
// MPU control/status vars
bool dmpReady = false;  // set true if DMP init was successful
uint8_t mpuIntStatus;   // holds actual interrupt status byte from MPU
uint8_t devStatus;      // return status after each device operation (0 = success, !0 = error)
uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

// orientation/motion vars
Quaternion q;           // [w, x, y, z]         quaternion container
VectorInt16 aa;         // [x, y, z]            accel sensor measurements
VectorInt16 gy;         // [x, y, z]            gyro sensor measurements
VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
VectorFloat gravity;    // [x, y, z]            gravity vector
float euler[3];         // [psi, theta, phi]    Euler angle container
float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector

// packet structure for InvenSense teapot demo
uint8_t teapotPacket[14] = { '$', 0x02, 0, 0, 0, 0, 0, 0, 0, 0, 0x00, 0x00, '\r', '\n' };

volatile bool mpuInterrupt = false;     // indicates whether MPU interrupt pin has gone high
void dmpDataReady() {
    mpuInterrupt = true;
}

void task_initI2C(void *ignore) {
	i2c_config_t conf;
	conf.mode = I2C_MODE_MASTER;
	conf.sda_io_num = (gpio_num_t)PIN_SDA;
	conf.scl_io_num = (gpio_num_t)PIN_CLK;
	conf.sda_pullup_en = GPIO_PULLUP_ENABLE;
	conf.scl_pullup_en = GPIO_PULLUP_ENABLE;
	conf.master.clk_speed = 400000;
	ESP_ERROR_CHECK(i2c_param_config(I2C_NUM_0, &conf));
	ESP_ERROR_CHECK(i2c_driver_install(I2C_NUM_0, I2C_MODE_MASTER, 0, 0, 0));
	vTaskDelete(NULL);
}

void task_display(void*){
	MPU6050 mpu = MPU6050();
	mpu.initialize();
	mpu.dmpInitialize();

	// This need to be setup individually
	mpu.setXGyroOffset(-21);
    mpu.setYGyroOffset(-17);
    mpu.setZGyroOffset(-58);
    mpu.setXAccelOffset(-6989);
    mpu.setYAccelOffset(-7580);
    mpu.setZAccelOffset(9726);

	mpu.setDMPEnabled(true);
	dmpReady = true;

	while(1){
				// if programming failed, don't try to do anything
		if (!dmpReady) return;
		// read a packet from FIFO
		if (mpu.dmpGetCurrentFIFOPacket(fifoBuffer)) { // Get the Latest packet 

		#ifdef OUTPUT_READABLE_QUATERNION
			// display quaternion values in easy matrix form: w x y z
			mpu.dmpGetQuaternion(&q, fifoBuffer);
			printf("quat\t");
			printf(q.w);
			printf("\t");
			printf(q.x);
			printf("\t");
			printf(q.y);
			printf("\t");
			Serial.println(q.z);
		#endif

		#ifdef OUTPUT_READABLE_EULER
			// display Euler angles in degrees
			mpu.dmpGetQuaternion(&q, fifoBuffer);
			mpu.dmpGetEuler(euler, &q);
			printf"euler\t");
			printfeuler[0] * 180 / M_PI);
			printf"\t");
			printfeuler[1] * 180 / M_PI);
			printf"\t");
			Serial.println(euler[2] * 180 / M_PI);
		#endif

		#ifdef OUTPUT_READABLE_YAWPITCHROLL
			// display Euler angles in degrees
			mpu.dmpGetQuaternion(&q, fifoBuffer);
			mpu.dmpGetGravity(&gravity, &q);
			mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);
			printf("%f \t %f \t %f \r\n", ypr[0] * 180/PI,ypr[1] * 180/PI,ypr[2] * 180/PI);
		#endif

		#ifdef OUTPUT_READABLE_REALACCEL
			// display real acceleration, adjusted to remove gravity
			mpu.dmpGetQuaternion(&q, fifoBuffer);
			mpu.dmpGetAccel(&aa, fifoBuffer);
			mpu.dmpGetGravity(&gravity, &q);
			mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
			printf"areal\t");
			printfaaReal.x);
			printf"\t");
			printfaaReal.y);
			printf"\t");
			Serial.println(aaReal.z);
		#endif

		#ifdef OUTPUT_READABLE_WORLDACCEL
			// display initial world-frame acceleration, adjusted to remove gravity
			// and rotated based on known orientation from quaternion
			mpu.dmpGetQuaternion(&q, fifoBuffer);
			mpu.dmpGetAccel(&aa, fifoBuffer);
			mpu.dmpGetGravity(&gravity, &q);
			mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);
			mpu.dmpGetLinearAccelInWorld(&aaWorld, &aaReal, &q);
			printf("aworld\t");
			printf("%d,",aaWorld.x);
			printf("\t");
			printf("%d",aaWorld.y);
			printf("\t");
			printf("%d",aaWorld.z);
			printf("\n");
		#endif

		#ifdef OUTPUT_TEAPOT
			// display quaternion values in InvenSense Teapot demo format:
			teapotPacket[2] = fifoBuffer[0];
			teapotPacket[3] = fifoBuffer[1];
			teapotPacket[4] = fifoBuffer[4];
			teapotPacket[5] = fifoBuffer[5];
			teapotPacket[6] = fifoBuffer[8];
			teapotPacket[7] = fifoBuffer[9];
			teapotPacket[8] = fifoBuffer[12];
			teapotPacket[9] = fifoBuffer[13];
			Serial.write(teapotPacket, 14);
			teapotPacket[11]++; // packetCount, loops at 0xFF on purpose
		#endif
		}
	}
}
