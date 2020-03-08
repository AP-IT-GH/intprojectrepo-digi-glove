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

/*
    * Does not work as it should atm. 
    * 
    * Maybe implement running the stepper as a separate thread?
    * 
    * - Jaakko -
*/

#include <mbed.h>
#include <math.h>

/* SETTINGS */
#define READ_FREQUENCY 2000 // Sampling frequency in Hz
#define MIN_VALUE 12230 // Sensor minimum value in ohms
#define MAX_VALUE 15350 // Sensor Maximum value in ohms
#define RESOLUTION 8 // Output resolution bits, 8 is fine
#define VOUT 5 // 3.3 or 5
#define RM 12030 // Used Rm resistor exact value in ohms
#define FILTER_SIZE 50 // Larger values mean more high frequency filtering. Good values are between 10 and 500

#define FILTERING 0 // filter enabled or disabled (1 or 0)
#define PLOTTER 0 // 1 if you want to view plotter data instead

#define PINA PB_3
#define PINB PB_5
#define PINA_PRIME PB_4
#define PINB_PRIME PB_10

DigitalOut pins[] = {(PINA), (PINB),(PINA_PRIME),(PINB_PRIME)};

int runStepper(int Rotations){
    /*
        * THIS DOESN'T WORK AS IT SHOULD, MODIFY THE LOOPS OR DON'T USE IT
        * 
        *   :)
    */


    int delay1 = 3; // Delay between steps
    // Send current to
    // 1. The aPin
    // 2. The aPin, and the bPin
    // 3. The bPin
    // 4. Then to the bPin and the aPrimePin
    // 5. Then to the aPrimePin
    // 6. Then to the aPrimePin and the bPrime Pin
    // 7. Then to the bPrimePin
    // 8. Then the bPrimePin and the aPin.
    // Thus producing steps using the half step method
    
    int numPins = sizeof(pins)/sizeof(DigitalOut);
    int i, previous;
    

    //Reversive direction after each turn
    if ((Rotations) % 2 == 0) { // Check if number of rotations is odd

        for(i=0; i < numPins; i++){
            
            if (i == 0) previous = (numPins - 1);
            else previous = i - 1;

            if (pins[previous] == 0 & pins[i] == 0 ){
                // pins[previous] = 0;
                pins[i] = 1;
                
                //pins[previous] = 0;
            }
            else if(pins[previous] == 1 & pins[i] == 0){
                pins[i] = 1;
                i = i--;
            }
            else if(pins[previous] == 1 & pins[i] == 1){
                pins[i] = 1;
                pins[previous] = 0;
            }
            else if(pins[previous] == 0 & pins[i] == 1){

            }
            
            //pins[i] = 1;
            //pins[previous] = 0;

            wait_ms(delay1);

        }
        //pins[i] = 0;
        //pins[numPins] = 0;
    }
    else if ((Rotations) % 2 != 0){  // If number of rotations is even

            for(i=(numPins-1); i >= 0; i = i-1){
            
                if (i == (numPins-1)) previous = (0);
                else previous = i + 1;

                if (pins[previous] == 0 & pins[i] == 0 ){
                    // pins[previous] = 0;
                    pins[i] = 1;
                    
                    //pins[previous] = 0;
                }
                else if(pins[previous] == 1 & pins[i] == 0){
                    pins[i] = 1;
                    i = i++;
                }
                else if(pins[previous] == 1 & pins[i] == 1){
                    pins[i] = 1;
                    pins[previous] = 0;
                }
                else if(pins[previous] == 0 & pins[i] == 1){
                    
                }
                
                //pins[i] = 1;
                //pins[previous] = 0;

                wait_ms(delay1);

            }
    }

}

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
    DigitalOut aPin(PINA); // Pin D3
    DigitalOut bPin(PINB); // Pin D4
    DigitalOut aPrimePin(PINA_PRIME); // Pin D5
    DigitalOut bPrimePin(PINB_PRIME);// Pin D6
    DigitalIn  trigPin(PA_8); // Pin D7


    int delay1 = 3; // Delay between steps
    int delay2 = 300; // Delay after a full turn
    int calibdelay = 20; //Delay between steps during calibration
    int count = 0; 
    int numberOfRotations = 0;
    float degrees = 0.00;

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
                if(numberOfRotations != 0){
                    //runStepper(numberOfRotations);

                    if ((numberOfRotations) % 2 == 0) { // Check if number of rotations is odd
                        // If yes, turn the stepper clockwise.

                        // 1. Set the aPin High
                        aPin = 1;
                        bPin = 0;
                        aPrimePin = 0;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 2. Energize aPin and bPin to HIGH
                        aPin = 1;
                        bPin = 1;
                        aPrimePin = 0;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 3. Set the bPin to High
                        aPin = 0;
                        bPin = 1;
                        aPrimePin = 0;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 4. Set the bPin and the aPrimePin to HIGH
                        aPin = 0;
                        bPin = 1;
                        aPrimePin = 1;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        aPin = 0;
                        bPin = 0;
                        aPrimePin = 1;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 6. Set the aPrimePin and the bPrime Pin to HIGH
                        aPin = 0;
                        bPin = 0;
                        aPrimePin = 1;
                        bPrimePin = 1;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 7. Set the bPrimePin to HIGH
                        aPin = 0;
                        bPin = 0;
                        aPrimePin = 0;
                        bPrimePin = 1;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 8. Set the bPrimePin and the aPin to HIGH
                        aPin = 1;
                        bPin = 0;
                        aPrimePin = 0;
                        bPrimePin = 1;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    }
                    else if ((numberOfRotations) % 2 != 0){  // If number of rotations is even...
                        // ... rotate to opposite direction.

                        // 1. Set the aPin High
                        aPin = 0;
                        bPin = 0;
                        aPrimePin = 0;
                        bPrimePin = 1;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 2. Energize aPin and bPin to HIGH
                        aPin = 0;
                        bPin = 0;
                        aPrimePin = 1;
                        bPrimePin = 1;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 3. Set the bPin to High
                        aPin = 0;
                        bPin = 0;
                        aPrimePin = 1;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 4. Set the bPin and the aPrimePin to HIGH
                        aPin = 0;
                        bPin = 1;
                        aPrimePin = 1;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        aPin = 0;
                        bPin = 1;
                        aPrimePin = 0;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 6. Set the aPrimePin and the bPrime Pin to HIGH
                        aPin = 1;
                        bPin = 1;
                        aPrimePin = 0;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 7. Set the bPrimePin to HIGH
                        aPin = 1;
                        bPin = 0;
                        aPrimePin = 0;
                        bPrimePin = 0;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds
                    
                        // 8. Set the bPrimePin and the aPin to HIGH
                        aPin = 1;
                        bPin = 0;
                        aPrimePin = 0;
                        bPrimePin = 1;
                        // Allow some delay between energizing the coils to allow
                        //  the stepper rotor time to respond.
                        wait_ms(delay1); // So, delay1 milliseconds

                    }
                    count = count + 8;
                    
                    //count = count + 6;
                    if ((numberOfRotations) % 2 != 0){
                        degrees = degrees - (360.0 * (8 / 4096.0));
                    }
                    else{
                        degrees = degrees + (360.0 * (8 / 4096.0));
                    }

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
                        // float x0 = 13.466f;
                        // float x1 = 0.1353f * scaledValue;
                        // float x2 = 0.0022f * pow(scaledValue,2);
                        // float x3 = -0.000004f * pow(scaledValue,3);

                        // The angle is calculated from the steps of the motor and values collected manually
                        //   (check angle from drawn meter compared to the stepper angle).
                        // y = (1*10^-5)x^4 - 0.0012x^3 + 0.045x^2 + 0.8071x + 1.6844
                        // or y = 0.0004x^3 - 0.0222x^2 + 1.6784x + 0.632

                        float x0 = 0.632f;
                        float x1 = 1.6784f * degrees;
                        float x2 = -0.0222f * pow(degrees, 2);
                        float x3 = 0.0004f * pow(degrees, 3);
                        //float x4 = 0.00001f * pow(degrees, 4);

                        angle = x3 + x2 + x1 + x0;
                        
                        /* Output */
                        #if PLOTTER
                            pc.printf("%i, ", 0);
                            pc.printf("%i, ", (int)scaledValue);
                            pc.printf("%i\r\n", 300);
                        #else
                            pc.printf("%i, ", curTime);
                            pc.printf("%i, ", numberOfRotations);
                            pc.printf("%f, ", resistance);
                            pc.printf("%i, ", (int)scaledValue);
                            pc.printf("%f, ", degrees);
                            pc.printf("%f\r\n ", angle);
                        #endif

                        prevTime = curTime;
                    }
                    
                    // If we reached endpoint:
                    if (count >= 730) {
                        
                        wait_ms(delay2); // delay2/1000 second(s) after each full rotation
                        count = 0; // Reset step counter to zero
                        numberOfRotations = numberOfRotations + 1;
                    }

                }
                else if(numberOfRotations == 0){
                    
                    // 1. Set the aPin High
                    aPin = 1;
                    bPin = 0;
                    aPrimePin = 0;
                    bPrimePin = 0;
                    // Allow some delay between energizing the coils to allow
                    //  the stepper rotor time to respond.
                    wait_ms(calibdelay); // So, delay1 milliseconds
                
                    // 2. Energize aPin and bPin to HIGH
                    aPin = 1;
                    bPin = 1;
                    aPrimePin = 0;
                    bPrimePin = 0;
                    // Allow some delay between energizing the coils to allow
                    //  the stepper rotor time to respond.
                    wait_ms(calibdelay); // So, delay1 milliseconds
                
                    // 3. Set the bPin to High
                    aPin = 0;
                    bPin = 1;
                    aPrimePin = 0;
                    bPrimePin = 0;
                    // Allow some delay between energizing the coils to allow
                    //  the stepper rotor time to respond.
                    wait_ms(calibdelay); // So, delay1 milliseconds
                
                    // 4. Set the bPin and the aPrimePin to HIGH
                    aPin = 0;
                    bPin = 1;
                    aPrimePin = 1;
                    bPrimePin = 0;
                    // Allow some delay between energizing the coils to allow
                    //  the stepper rotor time to respond.
                    wait_ms(calibdelay); // So, delay1 milliseconds
                
                    aPin = 0;
                    bPin = 0;
                    aPrimePin = 1;
                    bPrimePin = 0;
                    // Allow some delay between energizing the coils to allow
                    //  the stepper rotor time to respond.
                    wait_ms(calibdelay); // So, delay1 milliseconds
                
                    // 6. Set the aPrimePin and the bPrime Pin to HIGH
                    aPin = 0;
                    bPin = 0;
                    aPrimePin = 1;
                    bPrimePin = 1;
                    // Allow some delay between energizing the coils to allow
                    //  the stepper rotor time to respond.
                    wait_ms(calibdelay); // So, delay1 milliseconds
                
                    // 7. Set the bPrimePin to HIGH
                    aPin = 0;
                    bPin = 0;
                    aPrimePin = 0;
                    bPrimePin = 1;
                    // Allow some delay between energizing the coils to allow
                    //  the stepper rotor time to respond.
                    wait_ms(calibdelay); // So, delay1 milliseconds
                
                    // 8. Set the bPrimePin and the aPin to HIGH
                    aPin = 1;
                    bPin = 0;
                    aPrimePin = 0;
                    bPrimePin = 1;
                    // Allow some delay between energizing the coils to allow
                    //  the stepper rotor time to respond.
                    wait_ms(calibdelay); // So, delay1 milliseconds
                    
                    count = count + 8;
                    
                    // If trigPin is HIGH, it means we reached end stop:
                    if (trigPin) {
                        wait_ms(delay2); // delay2/1000 second(s) after each full rotation
                        degrees = (360.0 * (736 / 4096.0));
                        count = 0; // Reset step counter to zero
                        numberOfRotations = numberOfRotations + 1;
                    }
                    
                }
        
        
    }
}

