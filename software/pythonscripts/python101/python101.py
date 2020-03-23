import Macros
import serial
import time

port = "COM15"

ser1 = serial.Serial(port, 9600, 8)
print("sending data continiously to bluetooth devices press ctrl + c to stop")
data = {"timestamp0" : 0 , "timestamp1" : 0, "timestamp2" : 0 , "timestamp3" : 0 , "timestamp4" : 0 ,  "timestamp5" : 0 , "timestamp6" : 0 , "timestamp7" : 0 ,
       "flex_1" : 0 , "flex_2" : 0 , "flex_3" : 0 , "flex_4" : 0 , "flex_5" : 0 , "flex_6" : 0 , "flex_7" : 0 , "flex_8" : 0 , "flex_9" : 0 , "flex_10" : 0 ,
      "touch_time" : 0 , "Touch_1" : 0, "Touch_2" : 0, "Touch_3" : 0 , "Touch_4" : 0 , "rot_time" : 0 , "Rot_X" : 0 , "Rot_Y" : 0 , "Rot_Z" : 0 , 
      "accel_time" : 0 , "Accel_X" : 0 , "Accel_Y" : 0 , "Accel_Z" : 0}
def update(): #to edit in release
       #ser1.write(0x01)
       if(ser1.inWaiting() >= 64):
            data_seq = ser1.read(64) #read the buffer as soon as it reached 64 bytes aka a full sequence has entered1

            #forging the library

            data["timestamp0"] = int(data_seq[0])
            data["timestamp1"] = int(data_seq[1])
            data["timestamp2"] = int(data_seq[2])
            data["timestamp3"] = int(data_seq[3])
            data["timestamp4"] = int(data_seq[4])
            data["timestamp5"] = int(data_seq[5])
            data["timestamp6"] = int(data_seq[6])
            data["timestamp7"] = int(data_seq[7])
            data["flex_1"] = int(data_seq[7])
            data["flex_2"] = int(data_seq[8])
            data["flex_3"] = int(data_seq[9])
            data["flex_4"] = int(data_seq[10])
            data["flex_5"] = int(data_seq[11])
            data["flex_6"] = int(data_seq[12])
            data["flex_7"] = int(data_seq[13])
            data["flex_8"] = int(data_seq[14])
            data["flex_9"] = int(data_seq[15])
            data["flex_10"] = int(data_seq[16])
            data["touch_time"] = int(data_seq[17])
            data["Touch_1"] = int(data_seq[18])
            data["Touch_2"] = int(data_seq[19])
            data["Touch_3"] = int(data_seq[20])
            data["Touch_4"] = int(data_seq[21])
            data["rot_time"] = int(data_seq[22])
            data["Rot_X"] = float(data_seq[23])
            data["Rot_Y"] = float(data_seq[24])
            data["Rot_Z"] = float(data_seq[25])
            data["accel_time"] = int(data_seq[26])
            data["Accel_X"] = int(data_seq[27])
            data["Accel_Y"] = int(data_seq[28])
            data["Accel_X"] = int(data_seq[29])


            #debug purposes

            #print(data["timestamp0"])
            print(data["timestamp1"])
            #print(data["Accel_X"])
            #print(int(data_seq[6]))
            #print(int(data_seq[2])) #test program (byte 0 of timestamp bytes 8)
            #print(data_seq)
            #print("/") #print a slash so we know where the full line of data is at
            #time.sleep(5) #wait for 5 seconds to keep the terminal readable
            ser1.reset_input_buffer() #clear the buffer of any data so the next line can come through properly
       #data["value1"] = ser1.read()
       #time.sleep(0.005) #decide the frequency of pulls esp data is presumed live
#endupdate
while True:
    update()
