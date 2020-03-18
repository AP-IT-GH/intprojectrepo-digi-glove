import Macros
import serial
import time

port = "COM13"

ser1 = serial.Serial(port, 9600, 8)
print("sending data continiously to bluetooth devices press ctrl + c to stop")
data = {"value1" : 101 , "value2" : 101}

def update(): #to edit in release
       #ser1.write(0x01)
       if(ser1.inWaiting() >= 64):
            print(ser1.read(64))
       print("/")
       #data["value1"] = ser1.read()
       #time.sleep(0.005) #decide the frequency of pulls esp data is presumed live
#endupdate
while True:
    update()
