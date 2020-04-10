#ifndef RGB_LED_H
#define RGB_LED_H

#ifdef __cplusplus
extern "C" {
#endif  

#include <stdint.h>

/**
 * Set the led configurations and assign duty cycles of 0 for each channel
*/
void rgb_init();

/**
 * @brief Set the duty cycle of three led channels, Red, Green and Blue
 * 
 * Uses non-blocking, thread-safe functions. However, the previous fade operation must be completed
 *  before starting a new one.
 * 
 * @param r_duty    Red duty cycle 0-255
 * @param g_duty    Green duty cycle 0-255
 * @param b_duty    Blue duty cycle 0-255
 * @param fade_time Time the fading takes in milliseconds
*/
void rgb_set(uint32_t r_duty, uint32_t g_duty, uint32_t b_duty, uint32_t fade_time);

#ifdef __cplusplus
}
#endif

#endif