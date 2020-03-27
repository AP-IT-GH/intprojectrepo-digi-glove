#ifndef BT_SPP_H
#define BT_SPP_H

#ifdef __cplusplus
extern "C" {
#endif  

#include <stdbool.h>
#include <stdint.h>
#include <esp_gap_bt_api.h>

#define SENSOR_DATA_SIZE 15
#define IMU_DATA_SIZE 7

/**
 * @brief Sensor data struct.
*/
typedef struct{
    int64_t capture_time;           // Time of capture
    uint8_t data[SENSOR_DATA_SIZE]; // Sensor data itself in pre-defined order
}sensor_data_t;

/**
 * @brief IMU data struct. 
*/
typedef struct{
    int64_t capture_time;       // Time of capture
    float data[IMU_DATA_SIZE];  // IMU data itself in pre-defined order
}imu_data_t;

/**
 * @brief Function for creating packet. Waits until it has both, sensor and IMU data and then sends it
 *   through bluetooth SPP.
 * 
 * This is intended to be called as a regular function by the sensor and IMU tasks. Once
 *   they have their data, they call this function and this function decides whether to send data or not.
 * 
 * Even when sending, this function only took ~0.16ms to run, thus it is not intended as a task.
 * 
 * Note: bt_init must be called before using this.
 * 
 * @param *sensor_d  Pointer to data coming from flex and pressure sensors (NULL if N/A)
 * @param *imu_d     Pointer to data coming from the IMU (NULL if N/A)
 * 
*/ 
void bt_create_packet(sensor_data_t *sensor_d,imu_data_t *imu_d);

/**
    * @brief Stores bluetooth connection status and properties 
*/
struct bluetooth_spp_property{
    uint32_t device_handle;     /* Handle of the device connected */
    bool bt_congested;          /* Congestion status of SPP bluetooth FALSE = Not congested */
    bool bt_available;          /* Indicates if the bluetooth is setup and a device is connected. TRUE = Connected*/
}bt_spp_conn_properties;

/**
 * @brief This function initiates all the necessary protocols in the BT stack for
 * starting a BT SPP slave server.
 * 
 * @param dev_name - Set the discoverable name of the device.
 * 
 * @param pin_number - Set the PIN number to connect the device.
 * Has to be an array of chars (max 16 characters), even numbers have to be characters.
 * Normally only the first 4 chars of the PIN will be used when pairing.
 * 
 * Example for assigning pin code and calling the function:
 * @code{c}
 * esp_bt_pin_code_t pin_code;
 * pin_code[0] = '1';
 * pin_code[1] = '2';
 * pin_code[2] = '3';
 * pin_code[3] = '4';
 * 
 * bt_init("ESP_32", pin_code);
 * @endcode
*/
esp_err_t bt_init(char *dev_name, esp_bt_pin_code_t pin_number);

#ifdef __cplusplus
}
#endif

#endif /* BT_SPP_H */