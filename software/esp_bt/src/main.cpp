#include "BluetoothSerial.h"
   
BluetoothSerial SerialBT;
    
void setup()
{
  SerialBT.begin("ESP32_alpha0.0.1");
  Serial.begin(9600);
  delay(1000);
}
    
void loop()
{
  String inputFromOtherSide;
  if (SerialBT.available()) {
    inputFromOtherSide = SerialBT.readString();
    SerialBT.println("You had entered: ");
    SerialBT.println(inputFromOtherSide);
    Serial.println(inputFromOtherSide);
  }
}