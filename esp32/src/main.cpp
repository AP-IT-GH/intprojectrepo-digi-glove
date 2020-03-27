#include "freertos/FreeRTOS.h"
#include "sdkconfig.h"
#include "freertos/task.h"
#include "bt_spp.h"

#define BT_SERVER_NAME "DigiGlove"

extern "C" {
	void app_main(void);
    // void sensors_init(void);
    // void sensors_task(void);
    // void dummydata_task(void);
}
// extern void imu_init(void);
// extern void imu_task(void);

void app_main(void)
{
    /* TO DO */
    
    esp_bt_pin_code_t pin_code;
    pin_code[0] = '1';
    pin_code[1] = '3';
    pin_code[2] = '3';
    pin_code[3] = '7';

    // Initialize the Bluetooth server:
    bt_init(BT_SERVER_NAME, pin_code);

}