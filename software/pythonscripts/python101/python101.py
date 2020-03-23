import Macros
import serial
import time

port = "COM15"

ser1 = serial.Serial(port, 9600, 8)
print("sending data continiously to bluetooth devices press ctrl + c to stop")
data = {"value1" : 101 , "value2" : 101}

def update(): #to edit in release
       #ser1.write(0x01)
       if(ser1.inWaiting() >= 64):
            data = ser1.read(64)
            #data.split("x")
            str(data)
            print(data)
            print(data[0: 4])
            print("/")
            time.sleep(10) #wait for 10 seconds
            ser1.reset_input_buffer() #clear the buffer
       #data["value1"] = ser1.read()
       #time.sleep(0.005) #decide the frequency of pulls esp data is presumed live
#endupdate
while True:
    update()
