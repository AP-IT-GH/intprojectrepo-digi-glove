<<<<<<< HEAD
<<<<<<< HEAD
=======
import Macros
>>>>>>> master
import serial

port = "COM13"
ser1 = serial.Serial(port, 9600, 8)
print("sending data continiously to bluetooth devices press ctrl + c to stop")
<<<<<<< HEAD
while True:
    ser.write(0x00)
    ser1.write(0x01)
=======

import serial
ser = serial.Serial(r"\\.\COM15")
ser1 = serial.Serial(r"\\.\COM13")
print("sending data continiously to bluetooth devices press ctrl + c to stop")
while True:
    ser.write(0x00)
    ser1.write(0x01)
    #print(ser.read(2))
    print(ser1.read(2))
>>>>>>> parent of 5e4181d... bluetooth fixed
=======
data = {"value1" : 101 , "value2" : 101}

def update(): #to edit in release
       ser1.write(0x01)
       print(ser1.read())
       data["value1"] = ser1.read()
#endupdate
>>>>>>> master
