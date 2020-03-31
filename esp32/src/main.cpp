#include "freertos/FreeRTOS.h"
#include "sdkconfig.h"
#include "freertos/task.h"
#include "freertos/semphr.h"
#include "freertos/queue.h"
#include "bt_spp.h"

extern "C" {
	void app_main(void);
    void sensors_task(void* ignore);

    void dummydata_task(void);
}
// extern void imu_init(void);
// extern void imu_task(void);

    
void app_main(void)
{
    bt_init(BT_SERVER_NAME, NULL);
    xTaskCreate(sensors_task, "sensors_task", 4096, NULL, configMAX_PRIORITIES, NULL);
}