#include <stdio.h>
#include "freertos/FreeRTOS.h"
#include "freertos/task.h"
#include "driver/ledc.h"
#include "esp_err.h"
#include "pins.h"

#define LEDC_HS_TIMER          LEDC_TIMER_0
#define LEDC_HS_MODE           LEDC_HIGH_SPEED_MODE
#define LEDC_HS_CH0_GPIO       (LED_R)
#define LEDC_HS_CH0_CHANNEL    LEDC_CHANNEL_0
#define LEDC_HS_CH1_GPIO       (LED_G)
#define LEDC_HS_CH1_CHANNEL    LEDC_CHANNEL_1
#define LEDC_HS_CH2_GPIO       (LED_B)
#define LEDC_HS_CH2_CHANNEL    LEDC_CHANNEL_2

#define LEDC_HS_CHANNEL_AMT    (3)

/*
    * Prepare and set configuration of timers
    * that will be used by LED Controller
    */
ledc_timer_config_t ledc_timer = {
    .duty_resolution = LEDC_TIMER_8_BIT, // resolution of PWM duty
    .freq_hz = 5000,                      // frequency of PWM signal
    .speed_mode = LEDC_HS_MODE,           // timer mode
    .timer_num = LEDC_HS_TIMER,            // timer index
    //.clk_cfg = LEDC_AUTO_CLK,              // Auto select the source clock
};


/*
    * Prepare individual configuration
    * for each channel of LED Controller
    * by selecting:
    * - controller's channel number
    * - output duty cycle, set initially to 0
    * - GPIO number where LED is connected to
    * - speed mode, either high or low
    * - timer servicing selected channel
    *   Note: if different channels use one timer,
    *         then frequency and bit_num of these channels
    *         will be the same
    */
ledc_channel_config_t ledc_channel[LEDC_HS_CHANNEL_AMT] = {
    {
        .channel    = LEDC_HS_CH0_CHANNEL,
        .duty       = 0,
        .gpio_num   = LEDC_HS_CH0_GPIO,
        .speed_mode = LEDC_HS_MODE,
        .hpoint     = 0,
        .timer_sel  = LEDC_HS_TIMER
    },
    {
        .channel    = LEDC_HS_CH1_CHANNEL,
        .duty       = 0,
        .gpio_num   = LEDC_HS_CH1_GPIO,
        .speed_mode = LEDC_HS_MODE,
        .hpoint     = 0,
        .timer_sel  = LEDC_HS_TIMER
    },
    {
        .channel    = LEDC_HS_CH2_CHANNEL,
        .duty       = 0,
        .gpio_num   = LEDC_HS_CH2_GPIO,
        .speed_mode = LEDC_HS_MODE,
        .hpoint     = 0,
        .timer_sel  = LEDC_HS_TIMER
    },
};

void rgb_init()
{
    // Set configuration of timer0 for high speed channels
    ledc_timer_config(&ledc_timer);
    // Set LED Controller with previously prepared configuration
    for (int ch = 0; ch < LEDC_HS_CHANNEL_AMT; ch++) {
        ledc_channel_config(&ledc_channel[ch]);
    }

    // Install the fade function
    ledc_fade_func_install(0);
}

void rgb_set(uint32_t r_duty, uint32_t g_duty, uint32_t b_duty, uint32_t fade_time)
{
    ledc_set_fade_time_and_start(ledc_channel[LEDC_HS_CH0_CHANNEL].speed_mode, ledc_channel[LEDC_HS_CH0_CHANNEL].channel, r_duty, fade_time, LEDC_FADE_NO_WAIT);

    ledc_set_fade_time_and_start(ledc_channel[LEDC_HS_CH1_CHANNEL].speed_mode, ledc_channel[LEDC_HS_CH1_CHANNEL].channel, g_duty, fade_time, LEDC_FADE_NO_WAIT);

    ledc_set_fade_time_and_start(ledc_channel[LEDC_HS_CH2_CHANNEL].speed_mode, ledc_channel[LEDC_HS_CH2_CHANNEL].channel, b_duty, fade_time, LEDC_FADE_NO_WAIT);
}