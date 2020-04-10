/*
   This example code is in the Public Domain (or CC0 licensed, at your option.)

   Unless required by applicable law or agreed to in writing, this
   software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
   CONDITIONS OF ANY KIND, either express or implied.
*/

#include <stdint.h>
#include <string.h>
#include <stdbool.h>
#include <stdio.h>
#include <nvs.h>
#include <nvs_flash.h>
#include <freertos/FreeRTOS.h>
#include <freertos/task.h>
#include <esp_log.h>
#include <esp_bt.h>
#include "esp_bt_main.h"
#include "esp_gap_bt_api.h"
#include "esp_bt_device.h"
#include "esp_spp_api.h"

#include "time.h"
#include "sys/time.h"

#define SPP_TAG "SPP_BLUETOOTH"
#define SPP_SERVER_NAME "SPP_SERVER"
#define EXAMPLE_DEVICE_NAME "DigiGlove_SPP"
#define SPP_SHOW_DATA 0
#define SPP_SHOW_SPEED 1
#define SPP_REPLY 1
#define SPP_SHOW_MODE SPP_SHOW_DATA /*Choose show mode: show data or speed*/

// Choose which dummy data to generate
#define EN_GEN_FLEX_DATA 1
// Flex sensor mode, 1 for sweep or 0 for simulate quick flick of index finger:
#define FLEX_MODE_SWEEP 0

#define EN_GEN_GYRO_DATA 1
#define EN_GEN_ACC_DATA 1
#define EN_GEN_PRESS_DATA 1

// Define the delays between value changes:
#define FLEX_DELAY portTICK_PERIOD_MS
#define FLEX_DELAY_AFTER_BEND 1000
#define PRESS_DELAY portTICK_PERIOD_MS
#define GYRO_DELAY 10
#define ACC_DELAY 5

#define BT_DELAY 5

// CPU Core the generate functions are pinned to (-1 for no pin):
#define GEN_DATA_CORE 1

#define SPP_DATA_LEN 64 // msg data Array length 64 bytes

#define FLEX_DATA_LEN 9
#define FLEX_LIMIT_MAX 255
#define FLEX_LIMIT_MIN 0
#define FLEX_STEP 1

#define PRESS_DATA_LEN 4
#define GYRO_DATA_LEN 4
#define ACC_DATA_LEN 3

// Position of data in packet:
#define PKG_OFFSET_IMU_TIME 8
#define PKG_OFFSET_SENSOR_TIME 16
#define PKG_OFFSET_FLEX 38
#define PKG_OFFSET_PRESS 47
#define PKG_OFFSET_GYRO 24
#define PKG_OFFSET_ACC 32

#define GEN_DATA_NAME "GEN_DUMMY_DATA"
#define BT_SEND_NAME "BT_SPP_SEND"
#define GEN_GYRO_DATA_NAME "GEN_GYRO_DATA"
#define GEN_ACC_DATA_NAME "GEN_ACC_DATA"
#define GEN_PRESS_DATA_NAME "GEN_PRESS_DATA"

TaskHandle_t genDataHandle = "GEN_DUMMY_DATA";
TaskHandle_t btSendHandle = "BT_SPP_SEND";
TaskHandle_t genAccDataHandle = "GEN_ACC_DATA";
TaskHandle_t genGyroDataHandle = "GEN_GYRO_DATA";
TaskHandle_t genPressDataHandle = "GEN_PRESS_DATA";

static const esp_spp_mode_t esp_spp_mode = ESP_SPP_MODE_CB;

static const esp_spp_sec_t sec_mask = ESP_SPP_SEC_AUTHENTICATE;
static const esp_spp_role_t role_slave = ESP_SPP_ROLE_SLAVE;

uint32_t device_handle;
uint8_t spp_data[SPP_DATA_LEN];

uint8_t flex_data[FLEX_DATA_LEN];

uint8_t press_data[PRESS_DATA_LEN];
static uint8_t press_limit_max = 255;
static uint8_t press_limit_min = 0;

uint16_t gyro_data[GYRO_DATA_LEN];
static uint16_t gyro_limit_max = 16383;
static uint16_t gyro_limit_min = 0;
static uint16_t gyro_step = 100;

uint16_t acc_data[ACC_DATA_LEN];
static uint16_t acc_limit_max = 16383;
static uint16_t acc_limit_min = 0;
static uint16_t acc_step = 100;

int64_t cur_time = 0;

bool bt_congested = false;
bool bt_available = false;

void generate_data()
{
    /*
        Test function to generate dummy data

        Currently the fingers do a "wave" motion. Each finger will go one by one 
          from 0 to 255 and when all are at 255, they will go to 0 in reverse direction,
          one by one.
    */

   //TickType_t xLastWakeTime;

    while (1)
    {

        //If mode is bend test or sweep:
        if (FLEX_MODE_SWEEP){
            
            //Generate flex sensor data:
            if (flex_data[0] == FLEX_LIMIT_MIN)
            {
                
                for (int i = 0; i < FLEX_DATA_LEN; i++)
                {
                    for(int j = FLEX_LIMIT_MIN; j <= FLEX_LIMIT_MAX; j = j + FLEX_STEP)
                    {
                        // Get current (run)time:
                        //cur_time = esp_timer_get_time();
                        //xLastWakeTime = xTaskGetTickCount ();
                        
                        //Bend two joints:
                        flex_data[i] = j;

                        //vTaskDelayUntil(&xLastWakeTime, FLEX_DELAY/portTICK_PERIOD_MS);
                        vTaskDelay(FLEX_DELAY/portTICK_PERIOD_MS);
                    }
                }
            }
            else
            {
                
                for (int i = (FLEX_DATA_LEN - 1); i >= 0; i--)
                {
                    for (int j = FLEX_LIMIT_MAX; j >= FLEX_LIMIT_MIN; j = j - FLEX_STEP)
                    {
                        // Get current (run)time:
                        //cur_time = esp_timer_get_time();
                        //xLastWakeTime = xTaskGetTickCount ();
                        flex_data[i] = j;

                        // vTaskDelayUntil(&xLastWakeTime, FLEX_DELAY/portTICK_PERIOD_MS);
                        vTaskDelay(FLEX_DELAY/portTICK_PERIOD_MS);
                    }   
                    
                }
                vTaskDelay(FLEX_DELAY_AFTER_BEND/portTICK_PERIOD_MS);
            }
            vTaskDelay(FLEX_DELAY/portTICK_PERIOD_MS);
        }
        // If mode is simulate gesture then
        else{
            if (flex_data[0] == FLEX_LIMIT_MIN){
                for (int j = FLEX_LIMIT_MIN; j <= FLEX_LIMIT_MAX; j = j + FLEX_STEP){
                    flex_data[0] = j;
                    flex_data[4] = j;
                    vTaskDelay(FLEX_DELAY/portTICK_PERIOD_MS);
                }
            }
            else{
                for (int j = FLEX_LIMIT_MAX; j >= FLEX_LIMIT_MIN; j = j - FLEX_STEP){
                    flex_data[0] = j;
                    flex_data[4] = j;
                    vTaskDelay(FLEX_DELAY/portTICK_PERIOD_MS);
                }
                vTaskDelay(FLEX_DELAY_AFTER_BEND/portTICK_PERIOD_MS);
            }
            vTaskDelay(FLEX_DELAY/portTICK_PERIOD_MS);
        }
    }
}

void generate_gyro_data()
{
    while (1)
    {
        //Generate gyro data:
        if (gyro_data[0] == 0)
        {
            
            for (int i = 0; i < GYRO_DATA_LEN; i++)
            {
                for(float j = gyro_limit_min; j <= gyro_limit_max; j = j + gyro_step)
                {   
                    gyro_data[i] = j;
                    vTaskDelay(GYRO_DELAY/portTICK_PERIOD_MS);
                }
            
            }
        }
        else
        {
            for (int i = (GYRO_DATA_LEN - 1); i >= 0; i--)
            {
                for (float j = gyro_limit_max; j >= gyro_limit_min; j = j - gyro_step)
                {
                    gyro_data[i] = j;
                    vTaskDelay(GYRO_DELAY/portTICK_PERIOD_MS);
                }   
                gyro_data[i] = 0.00;
            }
        }
        vTaskDelay(GYRO_DELAY/portTICK_PERIOD_MS);
    }
}

void generate_pressure_data()
{
    while (1) {
        //Generate pressure sensor data:
        if (press_data[0] == press_limit_min) {
            for (int x = 0; x < PRESS_DATA_LEN;x++) {
                for(int y = press_limit_min; y < press_limit_max; y++) {
                    press_data[x] = y;
                    vTaskDelay(PRESS_DELAY/portTICK_PERIOD_MS);
                    
                }
            }
        }
        else {
            for (int x = (PRESS_DATA_LEN - 1); x >= 0; x--) {
                for (int y = press_limit_max; y >= press_limit_min; y--) {
                    press_data[x] = y;
                    vTaskDelay(PRESS_DELAY/portTICK_PERIOD_MS);
                }
            }
        }
        vTaskDelay(PRESS_DELAY/portTICK_PERIOD_MS);
    }
}


void generate_acc_data()
{
    
     while (1)
    {
        
        //Generate acceleration data:
        if (acc_data[0] == acc_limit_min)
        {
            
            for (int i = 0; i < ACC_DATA_LEN; i++)
            {
                for(int16_t j = acc_limit_min; j <= acc_limit_max; j = j + acc_step)
                {   
                    acc_data[i] = j;
                    vTaskDelay(ACC_DELAY/portTICK_PERIOD_MS);
                    //printf("i = %i, j = %i\r\n", i, j);
                }
            
            }
        }
        else
        {
            for (int i = (ACC_DATA_LEN - 1); i >= 0; i--)
            {
                for (int16_t j = acc_limit_max; j >= acc_limit_min; j = j - acc_step)
                {
                    acc_data[i] = j;
                    vTaskDelay(ACC_DELAY/portTICK_PERIOD_MS);
                    //printf("i = %i, j = %i\r\n", i, j);
                }   
                
            }
        }
        vTaskDelay(ACC_DELAY/portTICK_PERIOD_MS);
    }
    
}

void send_BT()
{
    /*  
        * Procedure to copy available data to bluetooth packet data frame and
        * send it.
        * 
    */

    ESP_LOGI(BT_SEND_NAME, "Started Send_BT");
    //vTaskSuspend(NULL);
    int last_time;
    bool timeout = false;

    TickType_t xLastWTime;
    TickType_t xDelay = BT_DELAY/portTICK_PERIOD_MS;

    last_time = esp_log_timestamp();
    while(true)
    {
        cur_time = esp_timer_get_time();
        //copy timestamp to message array start:
        memcpy(spp_data + PKG_OFFSET_IMU_TIME, &cur_time, sizeof(int64_t));
        memcpy(spp_data + PKG_OFFSET_SENSOR_TIME, &cur_time, sizeof(int64_t));

        // copy flex data array to message array starting from the end of planned timestamp...
        memcpy(spp_data + PKG_OFFSET_FLEX, flex_data, sizeof(int8_t) * FLEX_DATA_LEN);

        if(EN_GEN_GYRO_DATA)
        {
            memcpy(spp_data + PKG_OFFSET_GYRO, gyro_data, sizeof(float) * GYRO_DATA_LEN);
        }

        if(EN_GEN_ACC_DATA)
        {
            memcpy(spp_data + PKG_OFFSET_ACC, acc_data, sizeof(int16_t) * ACC_DATA_LEN);
        }

        if(EN_GEN_PRESS_DATA)
        {
            memcpy(spp_data + PKG_OFFSET_PRESS, press_data, sizeof(uint8_t) * PRESS_DATA_LEN);
        }

        // while ((bt_congested == true) || bt_available == false)
        while(bt_available == false)
        {
            // ESP_LOGE(BT_SEND_NAME,"BT Congested or not available.");
            vTaskDelay(BT_DELAY/portTICK_PERIOD_MS);
        }

        esp_err_t ret = esp_spp_write(device_handle, SPP_DATA_LEN, spp_data);
        
        if (ret != ESP_OK)
        {
            ESP_LOGE(BT_SEND_NAME, "%s Sending data failed: %s\n", __func__, esp_err_to_name(ret));
        }
        else
        {
            //ESP_LOGI(BT_SEND_NAME,"Sent data to %i, ", device_handle);
        }

        //xLastWTime = xTaskGetTickCount();
        vTaskDelay(BT_DELAY/portTICK_PERIOD_MS);
        //vTaskDelayUntil(&xLastWTime, xDelay);

        //ESP_LOG_BUFFER_HEXDUMP("SPP_DATA", spp_data, SPP_DATA_LEN, ESP_LOG_INFO);

    }
}


static void esp_spp_cb(esp_spp_cb_event_t event, esp_spp_cb_param_t *param)
{
    switch (event)
    {
    case ESP_SPP_INIT_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_INIT_EVT");
        esp_bt_dev_set_device_name(EXAMPLE_DEVICE_NAME);
        //esp_bt_gap_set_scan_mode(ESP_BT_CONNECTABLE, ESP_BT_GENERAL_DISCOVERABLE); // Different constants for newer esp-idf
        esp_bt_gap_set_scan_mode(ESP_BT_SCAN_MODE_CONNECTABLE_DISCOVERABLE); // ! use this with PlatformIO !
        esp_spp_start_srv(sec_mask, role_slave, 0, SPP_SERVER_NAME);
        break;
    case ESP_SPP_DISCOVERY_COMP_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_DISCOVERY_COMP_EVT");
        break;
    case ESP_SPP_OPEN_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_OPEN_EVT");
        break;
    case ESP_SPP_CLOSE_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_CLOSE_EVT");
        bt_available = false;
        break;
    case ESP_SPP_START_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_START_EVT");
        break;
    case ESP_SPP_CL_INIT_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_CL_INIT_EVT");
        break;
    case ESP_SPP_DATA_IND_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_DATA_IND_EVT");
//         device_handle = param->data_ind.handle;
// #if (SPP_SHOW_MODE == SPP_SHOW_DATA)
//         ESP_LOGI(SPP_TAG, "ESP_SPP_DATA_IND_EVT len=%d handle=%d",
//                 param->data_ind.len, param->data_ind.handle);
//         esp_log_buffer_hex("", param->data_ind.data, param->data_ind.len);
// #else
//         gettimeofday(&time_new, NULL);
//         data_num += param->data_ind.len;
//         if (time_new.tv_sec - time_old.tv_sec >= 3)
//         {
//             print_speed();
//         }
// #endif

        // // Reply since we got the message:
        // printf("Sending reply to device handle: %d", device_handle);
        // // ESP_LOGI(SPP_TAG, device_handle);
        // printf("\n");

        // esp_err_t ret = esp_spp_write(device_handle, SPP_DATA_LEN, spp_data);
        
        // if (ret != ESP_OK)
        // {
        //     ESP_LOGE(SPP_TAG, "%s Sending data failed: %s\n", __func__, esp_err_to_name(ret));
        // }
        // else
        // {
        //     //printf("Ack error was fine???");
        // }
        break;

    case ESP_SPP_CONG_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_CONG_EVT");
        //bt_congested = param->cong.status;
        break;
    case ESP_SPP_WRITE_EVT:
        //ESP_LOGI(SPP_TAG, "ESP_SPP_WRITE_EVT");
        break;
    case ESP_SPP_SRV_OPEN_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_SRV_OPEN_EVT");
        device_handle = param->srv_open.handle;
        bt_available = true;
        break;
    default:
        break;
    }
}

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
    case ESP_BT_GAP_PIN_REQ_EVT:
    {
        ESP_LOGI(SPP_TAG, "ESP_BT_GAP_PIN_REQ_EVT min_16_digit:%d", param->pin_req.min_16_digit);
        if (param->pin_req.min_16_digit)
        {
            ESP_LOGI(SPP_TAG, "Input pin code: 0000 0000 0000 0000");
            esp_bt_pin_code_t pin_code = {0};
            esp_bt_gap_pin_reply(param->pin_req.bda, true, 16, pin_code);
        }
        else
        {
            ESP_LOGI(SPP_TAG, "Input pin code: 1234");
            esp_bt_pin_code_t pin_code;
            pin_code[0] = '1';
            pin_code[1] = '2';
            pin_code[2] = '3';
            pin_code[3] = '4';
            esp_bt_gap_pin_reply(param->pin_req.bda, true, 4, pin_code);
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

void app_main(void)
{

    // Fill the spp_data array:
    for (int i = 0; i < SPP_DATA_LEN; i++)
    {
        spp_data[i] = 0;
    }

    for(int i = 0; i < PRESS_DATA_LEN; i++){
        press_data[i] = 0;
    }

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
        return;
    }

    if ((ret = esp_bt_controller_enable(ESP_BT_MODE_CLASSIC_BT)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s enable controller failed: %s\n", __func__, esp_err_to_name(ret));
        return;
    }
    
    if ((ret = esp_bluedroid_init()) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s initialize bluedroid failed: %s\n", __func__, esp_err_to_name(ret));
        return;
    }

    if ((ret = esp_bluedroid_enable()) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s enable bluedroid failed: %s\n", __func__, esp_err_to_name(ret));
        return;
    }

    if ((ret = esp_bt_gap_register_callback(esp_bt_gap_cb)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s gap register failed: %s\n", __func__, esp_err_to_name(ret));
        return;
    }

    if ((ret = esp_spp_register_callback(esp_spp_cb)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s spp register failed: %s\n", __func__, esp_err_to_name(ret));
        return;
    }

    if ((ret = esp_spp_init(esp_spp_mode)) != ESP_OK)
    {
        ESP_LOGE(SPP_TAG, "%s spp init failed: %s\n", __func__, esp_err_to_name(ret));
        return;
    }

    // Start generating flex data:
    xTaskCreatePinnedToCore(generate_data, GEN_DATA_NAME, 2048, NULL, configMAX_PRIORITIES, &genDataHandle, GEN_DATA_CORE);

    // Start generating gyro data:
    if (EN_GEN_GYRO_DATA)
    {
        xTaskCreatePinnedToCore(generate_gyro_data, GEN_GYRO_DATA_NAME, 2048, NULL, configMAX_PRIORITIES, &genGyroDataHandle, GEN_DATA_CORE);
    }

    if (EN_GEN_PRESS_DATA)
    {
        xTaskCreatePinnedToCore(generate_pressure_data, GEN_PRESS_DATA_NAME, 2048, NULL, configMAX_PRIORITIES, &genPressDataHandle, GEN_DATA_CORE);
    }

    if (EN_GEN_ACC_DATA){
        xTaskCreatePinnedToCore(generate_acc_data, GEN_ACC_DATA_NAME, 2048, NULL, configMAX_PRIORITIES, &genAccDataHandle, GEN_DATA_CORE);
    }
    xTaskCreate(send_BT, BT_SEND_NAME, 2048, NULL, 10, &btSendHandle);
    configASSERT(btSendHandle);

#if (CONFIG_BT_SSP_ENABLED == true)
    /* Set default parameters for Secure Simple Pairing */
    esp_bt_sp_param_t param_type = ESP_BT_SP_IOCAP_MODE;
    esp_bt_io_cap_t iocap = ESP_BT_IO_CAP_IO;
    esp_bt_gap_set_security_param(param_type, &iocap, sizeof(uint8_t));
#endif
    /*
     * Set default parameters for Legacy Pairing
     * Use variable pin, input pin code when pairing
     */
    esp_bt_pin_type_t pin_type = ESP_BT_PIN_TYPE_VARIABLE;
    esp_bt_pin_code_t pin_code;
    esp_bt_gap_set_pin(pin_type, 0, pin_code);
}
