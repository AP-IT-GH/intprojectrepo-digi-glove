#include "freertos/FreeRTOS.h"
#include "sdkconfig.h"
#include "freertos/task.h"

extern "C" {
	void app_main(void);
    void bt_init(void);
    void bt_task(void);
    void sensors_init(void);
    void sensors_task(void);
    void dummydata_task(void);
}
extern void imu_init(void);
extern void imu_task(void);

void app_main(void)
{
    /* TO DO */
}