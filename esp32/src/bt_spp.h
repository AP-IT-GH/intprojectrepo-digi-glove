#ifndef BT_SPP_H
#define BT_SPP_H

#ifdef __cplusplus
extern "C" {
#endif  

#include <stdbool.h>
#include <stdint.h>
#include <esp_gap_bt_api.h>
#include "packet.h"


#define BT_SERVER_NAME "DigiGlove"



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


/**
 * @brief This function initiates all the necessary protocols in the BT stack for
 * starting a BT SPP slave server.
 * 
 * @param dev_name - Set the discoverable name of the device.
 * 
*/
esp_err_t bt_init(char *dev_name);

#ifdef __cplusplus
}
#endif

#endif /* BT_SPP_H */