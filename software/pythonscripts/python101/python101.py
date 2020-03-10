
import serial
#ser = serial.Serial(r"\\.\COM15")
#ser1 = serial.Serial(r"\\.\COM13")
#ser = serial.Serial("COM15", 9600, 8)
ser1 = serial.Serial("COM13", 9600, 8)
print("sending data continiously to bluetooth devices press ctrl + c to stop")
while True:
    #ser.write(0XFF)
    ser1.write(0x01)
    #print(ser.read(2))
    print(ser1.read(2))
