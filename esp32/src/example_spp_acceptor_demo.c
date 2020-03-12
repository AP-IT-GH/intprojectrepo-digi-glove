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
//#include "sdkconfig.h"

#include "time.h"
#include "sys/time.h"

#define SPP_TAG "SPP_BLUETOOTH"
#define SPP_SERVER_NAME "SPP_SERVER"
#define EXAMPLE_DEVICE_NAME "DigiGlove_SPP"
#define SPP_SHOW_DATA 0
#define SPP_SHOW_SPEED 1
#define SPP_REPLY 1
#define SPP_SHOW_MODE SPP_SHOW_DATA /*Choose show mode: show data or speed*/

#define SPP_DATA_LEN 64 // msg data Array length 64 bytes

#define FLEX_DATA_LEN 10

#define GEN_DATA_NAME "GEN_DUMMY_DATA"
#define BT_SEND_NAME "BT_SPP_SEND"

TaskHandle_t genDataHandle = "GEN_DUMMY_DATA";
TaskHandle_t btSendHandle = "BT_SPP_SEND";
static const esp_spp_mode_t esp_spp_mode = ESP_SPP_MODE_CB;

static struct timeval time_new, time_old;
static long data_num = 0;

static const esp_spp_sec_t sec_mask = ESP_SPP_SEC_AUTHENTICATE;
static const esp_spp_role_t role_slave = ESP_SPP_ROLE_SLAVE;

uint32_t device_handle;
uint8_t spp_data[SPP_DATA_LEN];

int8_t flex_data[10];

int cur_time = 0;

float message = 123.1234;
char message2[6] = "hello";

bool bt_congested = false;

// static void print_speed(void)
// {
//     float time_old_s = time_old.tv_sec + time_old.tv_usec / 1000000.0;
//     float time_new_s = time_new.tv_sec + time_new.tv_usec / 1000000.0;
//     float time_interval = time_new_s - time_old_s;
//     float speed = data_num * 8 / time_interval / 1000.0;
//     ESP_LOGI(SPP_TAG, "speed(%fs ~ %fs): %f kbit/s" , time_old_s, time_new_s, speed);
//     data_num = spp_data 0;
//     time_old.tv_sec = time_new.tv_sec;
//     time_old.tv_usec = time_new.tv_usec;
// }

void generate_data()
{
    // Test function to generate dummy data
    while (1)
    {

        // Get current (run)time:
        cur_time = esp_log_timestamp();
        
        //Generate flex sensor data:
        if (flex_data[0] < 255)
        {
            for (int i = 0; i < FLEX_DATA_LEN; i++)
            {
                flex_data[i] = flex_data[i] + 1;
            }
        }
        else
        {
            for (int i = (FLEX_DATA_LEN - 1); i > 0; i--)
            {
                flex_data[i] = flex_data[i] - 1;
            }
        }
        vTaskDelay(20/portTICK_PERIOD_MS);
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
    int last_time;
    bool timeout = false;

    last_time = esp_log_timestamp();
    while(true)
    {
        //copy timestamp to message array start:
        memcpy(spp_data, &cur_time, sizeof(int));

        // copy flex data array to message array starting from the end of timestamp...
        memcpy(spp_data + sizeof(int), flex_data, sizeof(int8_t) * FLEX_DATA_LEN);

        // TEMPORARY: Add stop bits to end of message:
        spp_data[62] = 255;
        spp_data[63] = 255;

        while ((bt_congested == true))
        {
            if ((esp_log_timestamp() - last_time) > 30000)
            {
                timeout = true;
                ESP_LOGE(BT_SEND_NAME, "Waiting for BT congestion status timed out.");
            }
            vTaskDelay(10);
        }
        //esp_err_t ret = esp_spp_write(param->data_ind.handle, param->data_ind.len, param->data_ind.data);
        esp_err_t ret = esp_spp_write(device_handle, SPP_DATA_LEN, spp_data);
        
        if (ret != ESP_OK)
        {
            ESP_LOGE(BT_SEND_NAME, "%s Sending data failed: %s\n", __func__, esp_err_to_name(ret));
        }
        else
        {
            ESP_LOGI(BT_SEND_NAME,"Sent data to %i, ", device_handle);
        }
        vTaskDelay(20/portTICK_PERIOD_MS);

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
        vTaskSuspend(btSendHandle);
        vTaskDelete(btSendHandle);
        ESP_LOGI(SPP_TAG, "ESP_SPP_CLOSE_EVT");
        
        break;
    case ESP_SPP_START_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_START_EVT");
        break;
    case ESP_SPP_CL_INIT_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_CL_INIT_EVT");
        break;
    case ESP_SPP_DATA_IND_EVT:
        device_handle = param->data_ind.handle;
#if (SPP_SHOW_MODE == SPP_SHOW_DATA)
        ESP_LOGI(SPP_TAG, "ESP_SPP_DATA_IND_EVT len=%d handle=%d",
                param->data_ind.len, param->data_ind.handle);
        esp_log_buffer_hex("", param->data_ind.data, param->data_ind.len);
#else
        gettimeofday(&time_new, NULL);
        data_num += param->data_ind.len;
        if (time_new.tv_sec - time_old.tv_sec >= 3)
        {
            print_speed();
        }
#endif

        // Reply since we got the message:
        printf("Sending reply to device handle: %d", device_handle);
        // ESP_LOGI(SPP_TAG, device_handle);
        printf("\n");

        memcpy(spp_data + 20, message2, 5);

        //esp_err_t ret = esp_spp_write(param->data_ind.handle, param->data_ind.len, param->data_ind.data);
        esp_err_t ret = esp_spp_write(device_handle, SPP_DATA_LEN, spp_data);
        
        if (ret != ESP_OK)
        {
            ESP_LOGE(SPP_TAG, "%s Sending data failed: %s\n", __func__, esp_err_to_name(ret));
        }
        else
        {
            //printf("Ack error was fine???");
        }
        break;

    case ESP_SPP_CONG_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_CONG_EVT");
        break;
    case ESP_SPP_WRITE_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_WRITE_EVT");
        break;
    case ESP_SPP_SRV_OPEN_EVT:
        ESP_LOGI(SPP_TAG, "ESP_SPP_SRV_OPEN_EVT");
        gettimeofday(&time_old, NULL);
        device_handle = param->srv_open.handle;
        xTaskCreate(send_BT, BT_SEND_NAME, 2048, NULL, 10, &btSendHandle);
        configASSERT(genDataHandle);
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
    for (int i = 0; i < SPP_DATA_LEN; ++i)
    {
        spp_data[i] = 0;
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
    xTaskCreate(generate_data, GEN_DATA_NAME, 2048, NULL, configMAX_PRIORITIES, &genDataHandle);

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
