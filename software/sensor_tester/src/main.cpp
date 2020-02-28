/*
    * Copyright (c) 2020 Nasse. All rights reserved.
    * 
    * This work is licensed under the terms of the MIT license.  
    * For a copy, see <https://opensource.org/licenses/MIT>
    *
    * Description:  Convert and filter voltage masurement from analog 
    *               sensor to desired digital value and calculate angle
    *               from that value.
    * 
    * Usage:        First measure the sensors resting value, then measure it's maximum 
    *               value when you bend it. Then calculate the needed front resistor
    *               in voltage divider configuration.
    *               Formula: Rf = min + (max - min) / 2
    *               Rf = front resistor, min/max = min/max resistance from sensor
    *               Update the program to reflect measured/calculated values
    * 
    * Output:       Timestamp(us), Resistance(ohm), Resistance scaled, Angle(degrees)
*/
#include <mbed.h>
#include <math.h>

/* SETTINGS */
#define READ_FREQUENCY 2000 // Sampling frequency in Hz
#define MIN_VALUE 11290 // Sensor minimum value in ohms
#define MAX_VALUE 16050 // Sensor Maximum value in ohms
#define RESOLUTION 8 // Output resolution bits, 8 is fine
#define VOUT 5 // 3.3 or 5
#define RM 13540 // Used Rm resistor exact value in ohms
#define FILTER_SIZE 50 // Larger values mean more high frequency filtering. Good values are between 10 and 500

#define FILTERING 1 // filter enabled or disabled (1 or 0)
#define PLOTTER 0 // 1 if you want to view plotter data instead

int main() 
{
    /* Serial setup */
    Serial pc(USBTX,USBRX);
    pc.baud(500000);
    /* Timer setup */
    Timer timer;
    timer.start();
    timer.reset();
    long long curTime;
    long long prevTime = 0;

    AnalogIn A0_pin(A0);
    float A0_voltage;
    float resistance = 0;
    float scaledValue = 0;
    float angle = 0;
    float sum = 0;
    float convolutionContainer[FILTER_SIZE];
    int output_max = pow(2,RESOLUTION) - 1;

    for (int i = 0; i < FILTER_SIZE; i++) {
        convolutionContainer[i] = 0;
    }
    

    while(true) {
        curTime = timer.read_high_resolution_us();
        if ((curTime - prevTime) >= ((1/(float)READ_FREQUENCY) * 1000000)) {
            A0_voltage = A0_pin.read() * 3.3f;
            
            #if FILTERING
                sum = 0;
                for (int i = FILTER_SIZE - 1; i > 0; i--) {
                    convolutionContainer[i] = convolutionContainer[i-1];
                    sum += convolutionContainer[i];
                } 
                sum += A0_voltage;
                convolutionContainer[0] = A0_voltage;
                A0_voltage = sum / FILTER_SIZE;
            #endif
            
            resistance = -(A0_voltage * RM) / (A0_voltage - VOUT);
            scaledValue = round((resistance - MIN_VALUE) / (MAX_VALUE - MIN_VALUE) * output_max);

            /* Calculate angle: y = -4E-06x^3 + 0.0022x^2 + 0.1353x + 13.466 */
            /* This is not yet accurate and will not work for all the sensors out of the box */
            float x0 = 13.466f;
            float x1 = 0.1353f * scaledValue;
            float x2 = 0.0022f * pow(scaledValue,2);
            float x3 = -0.000004f * pow(scaledValue,3);
            angle = x3 + x2 + x1 + x0;
            
            /* Output */
            #if PLOTTER
                pc.printf("%i, ", 0);
                pc.printf("%i, ", (int)scaledValue);
                pc.printf("%i\r\n", 300);
            #else
                pc.printf("%i, ", curTime);
                pc.printf("%f, ", resistance);
                pc.printf("%i, ", (int)scaledValue);
                pc.printf("%f\r\n ", angle);
            #endif
            prevTime = curTime;
        }
    }
}