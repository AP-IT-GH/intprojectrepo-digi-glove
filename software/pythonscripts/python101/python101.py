import serial
ser = serial.Serial(r"\\.\COM15")
ser1 = serial.Serial(r"\\.\COM13")
print("sending data continiously to bluetooth devices press ctrl + c to stop")
while True:
    ser.write(0x00)
    ser1.write(0x01)
