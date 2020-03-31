/**
 * --- Bluetooth ---
 * 
 * Functions in this file create the packet for sending through BT
 *   and eventually send it once it's done.
 * 
 * Call bt_init with its required parameters to start the BT server.
 * 
 * To access BT connection status and the connected device handle, 
 *   use the bt_spp_conn_properties struct.
 * 
 * For creating the packet, all data must be sent to bt_create_packet function.
 * Once it has all the data it needs, it will create the BT packet and send it.
 * 
*/
#ifdef __cplusplus
extern "C" {
#endif

#include "time.h"
#include <freertos/FreeRTOS.h>
#include <freertos/task.h>
#include <stdint.h>
#include <string.h>
#include <stdbool.h>
#include <stdio.h>
#include <nvs.h>
#include <nvs_flash.h>
#include <esp_log.h>
#include <esp_bt.h>
#include "esp_bt_main.h"
#include "esp_gap_bt_api.h"
#include "esp_bt_device.h"
#include "esp_spp_api.h"
#include "bt_spp.h"

#define SPP_TAG "SPP_BLUETOOTH"
#define SPP_SERVER_NAME "SPP_SERVER"

// Size of BT data array:
#define BT_DATA_SIZE 74

// Position of data in BT packet data frame in bytes
#define PKT_OFFSET_SEN_TIME 8
#define PKT_OFFSET_SEN_DATA 52
#define PKT_OFFSET_IMU_TIME 16
#define PKT_OFFSET_IMU_DATA 24

// Local BT channel, 0 for any channel
#define BT_LOCAL_CHANNEL 0

static const esp_spp_mode_t esp_spp_mode = ESP_SPP_MODE_CB;
static const esp_spp_sec_t sec_mask = ESP_SPP_SEC_AUTHENTICATE;
static const esp_spp_role_t role_slave = ESP_SPP_ROLE_SLAVE;

// For storing the device name:
char device_name[64];

uint8_t bt_data[BT_DATA_SIZE] = {0};

// Create structs for sensor and imu data and clear them:
sensor_data_t sensor_data = {.capture_time = 0, .data = {0}};
imu_data_t imu_data = {.capture_time = 0, .data = {0}};

void bt_create_packet(sensor_data_t *sensor_d,imu_data_t *imu_d)
{

    // If sensor data was supplied, copy it to our global struct:
    if (sensor_d != NULL){
        memcpy(sensor_data.data, sensor_d->data, sizeof(uint8_t) * SENSOR_DATA_SIZE);
        sensor_data.capture_time = sensor_d->capture_time;
        //printf("Got sensor data\n");
    }

    // If IMU data was supplied, copy it to our global struct:
    if (imu_d != NULL){
        memcpy(imu_data.data, imu_d->data, sizeof(float) * IMU_DATA_SIZE);
        imu_data.capture_time = imu_d->capture_time;
        //printf("Got imu data\n");
    }

    // If we have all data, create BT packet and send it:
    if ((sensor_data.capture_time != 0) && (imu_data.capture_time != 0)){
        
        // Copy sensor data to BT data array:
        memcpy(bt_data + PKT_OFFSET_SEN_TIME, &sensor_data.capture_time, sizeof(int64_t));
        memcpy(bt_data + PKT_OFFSET_SEN_DATA, sensor_data.data, sizeof(uint8_t) * SENSOR_DATA_SIZE);
        
        // Copy IMU data to BT array:
        memcpy(bt_data + PKT_OFFSET_IMU_TIME, &imu_data.capture_time, sizeof(int64_t));
        memcpy(bt_data + PKT_OFFSET_IMU_DATA, imu_data.data, sizeof(float) * IMU_DATA_SIZE);

        // If BT is available, send the packet:
        if (bt_spp_conn_properties.bt_available == true && bt_spp_conn_properties.bt_congested == false){
            esp_err_t ret = esp_spp_write(bt_spp_conn_properties.device_handle, BT_DATA_SIZE, bt_data);
        }

        // Clear the data we have in global structs:
        for (int s = 0; s < SENSOR_DATA_SIZE; s++){
            sensor_data.data[s] = 0.00;
        }
        sensor_data.capture_time = 0;

        for (int s = 0; s < IMU_DATA_SIZE; s++){
            imu_data.data[s] = 0.00;
        }
        imu_data.capture_time = 0;
    }
}

/**
 * Callback function for BT SPP
*/
static void esp_spp_cb(esp_spp_cb_event_t event, esp_spp_cb_param_t *param)
{
    switch (event)
    {
    case ESP_SPP_INIT_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_INIT_EVT");
        esp_bt_dev_set_device_name(device_name);
        //esp_bt_gap_set_scan_mode(ESP_BT_CONNECTABLE, ESP_BT_GENERAL_DISCOVERABLE); // Different constants for newer esp-idf
        esp_bt_gap_set_scan_mode(ESP_BT_SCAN_MODE_CONNECTABLE_DISCOVERABLE); // ! use this with PlatformIO !
        esp_spp_start_srv(sec_mask, role_slave, BT_LOCAL_CHANNEL, SPP_SERVER_NAME);
        break;
    case ESP_SPP_DISCOVERY_COMP_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_DISCOVERY_COMP_EVT");
        break;
    case ESP_SPP_OPEN_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_OPEN_EVT");
        break;
    case ESP_SPP_CLOSE_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_CLOSE_EVT");
        bt_spp_conn_properties.bt_available = false;
        bt_spp_conn_properties.device_handle = 0;
        break;
    case ESP_SPP_START_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_START_EVT");
        break;
    case ESP_SPP_CL_INIT_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_CL_INIT_EVT");
        break;
    // case ESP_SPP_DATA_IND_EVT:
    //     ESP_LOGI(SPP_TAG, "ESP_SPP_DATA_IND_EVT");
    //     break;
    case ESP_SPP_CONG_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_CONG_EVT");
        bt_spp_conn_properties.bt_congested =  param->cong.cong;
        break;
    case ESP_SPP_WRITE_EVT:
        //ESP_LOGI(SPP_TAG, "ESP_SPP_WRITE_EVT");
        break;
    case ESP_SPP_SRV_OPEN_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_SRV_OPEN_EVT");
        bt_spp_conn_properties.device_handle = param->srv_open.handle;
        bt_spp_conn_properties.bt_available = true;
        break;
    default:
        break;
    }
}

/**
 * Callback function for BT GAP
*/
void esp_bt_gap_cb(esp_bt_gap_cb_event_t event, esp_bt_gap_cb_param_t *param)
{
    switch (event)
    {
    case ESP_BT_GAP_AUTH_CMPL_EVT:
    {
        if (param->auth_cmpl.stat == ESP_BT_STATUS_SUCCESS)
        {
            ESP_LOGI(SPP_TAG, "authentication success: %s", param->auth_cmpl.device_name);
            esp_log_buffer_hex(SPP_TAG, param->auth_cmpl.bda, ESP_BD_ADDR_LEN);
        }
        else
        {
            ESP_LOGE(SPP_TAG, "authentication failed, status:%d", param->auth_cmpl.stat);
        }
        break;
    }

#if (CONFIG_BT_SSP_ENABLED == true)
    case ESP_BT_GAP_CFM_REQ_EVT:
        ESP_LOGI(SPP_TAG, "ESP_BT_GAP_CFM_REQ_EVT Please compare the numeric value: %d", param->cfm_req.num_val);
        esp_bt_gap_ssp_confirm_reply(param->cfm_req.bda, true);
        break;
    case ESP_BT_GAP_KEY_NOTIF_EVT:
        ESP_LOGI(SPP_TAG, "ESP_BT_GAP_KEY_NOTIF_EVT passkey:%d", param->key_notif.passkey);
        break;
    case ESP_BT_GAP_KEY_REQ_EVT:
        ESP_LOGI(SPP_TAG, "ESP_BT_GAP_KEY_REQ_EVT Please enter passkey!");
        break;
#endif

    default:
    {
        ESP_LOGI(SPP_TAG, "event: %d", event);
        break;
    }
    }
    return;
}


esp_err_t bt_init(char *dev_name, esp_bt_pin_code_t pin_number)
{
    pin_number[0] = '1';
    pin_number[1] = '3';
    pin_number[2] = '3';
    pin_number[3] = '7';
    // Copy parameters that were passed to the global variables:
    memcpy(device_name, dev_name, sizeof(char) * 64);
    
    esp_err_t ret = nvs_flash_init();
    if (ret == ESP_ERR_NVS_NO_FREE_PAGES || ret == ESP_ERR_NVS_NEW_VERSION_FOUND)
    {
        ESP_ERROR_CHECK(nvs_flash_erase());
        ret = nvs_flash_init();
    }
    
    ESP_ERROR_CHECK(ret);

    ESP_ERROR_CHECK(esp_bt_controller_mem_release(ESP_BT_MODE_BLE));
    
    esp_bt_controller_config_t bt_cfg = BT_CONTROLLER_INIT_CONFIG_DEFAULT();
    if ((ret = esp_bt_controller_init(&bt_cfg)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s initialize controller failed: %s\n", __func__, esp_err_to_name(ret));
        return ret;
    }

    if ((ret = esp_bt_controller_enable(ESP_BT_MODE_CLASSIC_BT)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s enable controller failed: %s\n", __func__, esp_err_to_name(ret));
        return ret;
    }
    
    if ((ret = esp_bluedroid_init()) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s initialize bluedroid failed: %s\n", __func__, esp_err_to_name(ret));
        return ret;
    }

    if ((ret = esp_bluedroid_enable()) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s enable bluedroid failed: %s\n", __func__, esp_err_to_name(ret));
        return ret;
    }

    if ((ret = esp_bt_gap_register_callback(esp_bt_gap_cb)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s gap register failed: %s\n", __func__, esp_err_to_name(ret));
        return ret;
    }

    if ((ret = esp_spp_register_callback(esp_spp_cb)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s spp register failed: %s\n", __func__, esp_err_to_name(ret));
        return ret;
    }

    if ((ret = esp_spp_init(esp_spp_mode)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s spp init failed: %s\n", __func__, esp_err_to_name(ret));
        return ret;
    }

#if (CONFIG_BT_SSP_ENABLED == true)
    /* Set default parameters for Secure Simple Pairing */
    esp_bt_sp_param_t param_type = ESP_BT_SP_IOCAP_MODE;
    esp_bt_io_cap_t iocap = ESP_BT_IO_CAP_IO;
    esp_bt_gap_set_security_param(param_type, &iocap, sizeof(uint8_t));
#endif
    /*
     * Set default parameters for Legacy Pairing
     * Use fixed PIN, don't ask again.
     */
    esp_bt_pin_type_t pin_type = ESP_BT_PIN_TYPE_FIXED;
    esp_bt_gap_set_pin(pin_type, 4, pin_number);

    return ret;
}

#ifdef __cplusplus
}
#endif