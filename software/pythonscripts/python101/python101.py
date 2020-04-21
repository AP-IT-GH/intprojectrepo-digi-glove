import Macros
import serial
import time


port = "COM15"
try:
    ser1 = serial.Serial(port, 9600, 8) #attempts to make a connection to a device
except:
    print("error either bluetooth is off or the device in not connected only 0's will be returned")
print("if the device is connected, comms will now start")
data = {"reserved_0" : 0 , "reserved_1" : 0 , "reserved_2" : 0 , "reserved_3" : 0 , "reserved_4" : 0 , "reserved_5" : 0 , "reserved_6" : 0 , "reserved_7" : 0 ,  #reserved0
        "timestamp_0" : 0 , "timestamp_1" : 0 , "timestamp_2" : 0, "timestamp_3" : 0, "timestamp_4" : 0, "timestamp_5" : 0, "timestamp_6" : 0, "timestamp_7" : 0, #timestamp
        "Sensortime_0" : 0 , "Sensortime_1" : 0 , "Sensortime_2" : 0 , "Sensortime_3" : 0 , "Sensortime_4" : 0 , "Sensortime_5" : 0 , "Sensortime_6" : 0 , "Sensortime_7" : 0, #sensortime
        "Quat_W_0" : 0 , "Quat_W_1" : 0  , "Quat_W" : 0 , "Quat_X_0" : 0 , "Quat_X_1" : 0 , "Quat_X" : 0 , "Quat_Y_0" : 0 , "Quat_Y_1" : 0 , "Quat_Y" : 0 , "Quat_Z_0" : 0 , "Quat_Z_1" : 0 , "Quat_Z" : 0, #quats
        "Accel_X_0" : 0 , "Accel_X_1" : 0 , "Accel_X" : 0 , "Accel_Y_0" : 0 , "Accel_Y_1" : 0 , "Accel_Y" : 0 , "Accel_Z_0" : 0 , "Accel_Z_1" : 0 , "Accel_Z" : 0 , # accels
        "IndexF_0" : 0 , "MiddleF_0" : 0 , "RingF_0" : 0 , "LittleF_0" : 0 , "IndexF_1" : 0 , "MidddleF_1" : 0 , "RingF_1" : 0 , "LittleF_1" : 0 , "Thumb_0" : 0 , "IndexF_tip" : 0 , "MiddleF_tip" : 0, "RingF_tip": 0 , "LittleF_tip" : 0, # fingers
        "reserved1_0" : 0 , "reserved1_1" : 0 , "reserved1_2" : 0 , "reserved1_3" : 0 , "reserved1_4" : 0 , "reserved1_5" : 0 ,"reserved1_6" : 0 , "reserved1_7" : 0 , "reserved1_8" : 0 , "reserved1_9" : 0 , "reserved1_10" : 0 , "reserved1_11" : 0 , "reserved1_12" : 0
        }
        #all quats still have to be divided and multiplied for communication speed boost this will not be done here
        # create the full number in int needs to be devided by 16383 and multiplied with 9.80665
def update(): #to edit in release
       global data
       #ser1.write(0x01)
       try:
            if(ser1.inWaiting() >= 64):
                data_seq = ser1.read(64) #read the buffer as soon as it reached 64 bytes aka a full sequence has entered1
                #print(data_seq)
                data["reserved_0"] = int(data_seq[0])
                data["reserved_1"] = int(data_seq[1])
                data["reserved_2"] = int(data_seq[2])
                data["reserved_3"] = int(data_seq[3])
                data["reserved_4"] = int(data_seq[4])
                data["reserved_5"] = int(data_seq[5])
                data["reserved_6"] = int(data_seq[6])
                data["reserved_7"] = int(data_seq[7])
                #
                data["timestamp_0"] = int(data_seq[8])
                data["timestamp_1"] = int(data_seq[9])
                data["timestamp_2"] = int(data_seq[10])
                data["timestamp_3"] = int(data_seq[11])
                data["timestamp_4"] = int(data_seq[12])
                data["timestamp_5"] = int(data_seq[13])
                data["timestamp_6"] = int(data_seq[14])
                data["timestamp_7"] = int(data_seq[15])
                #
                data["Sensortime_0"] = int(data_seq[16])
                data["Sensortime_1"] = int(data_seq[17])
                data["Sensortime_2"] = int(data_seq[18])
                data["Sensortime_3"] = int(data_seq[19])
                data["Sensortime_4"] = int(data_seq[20])
                data["Sensortime_5"] = int(data_seq[21])
                data["Sensortime_6"] = int(data_seq[22])
                data["Sensortime_7"] = int(data_seq[23])
                #
                data["Quat_W_0"] = int(data_seq[24]) # higher order
                data["Quat_W_1"] = int(data_seq[25]) # lower order
                data["Quat_W"] = int((data["Quat_W_1"] << 8) + data["Quat_W_0"]) # create the full number in int needs to be devided by 16383 and multiplied with 9.80665
                #
                data["Quat_X_0"] = int(data_seq[26])
                data["Quat_X_1"] = int(data_seq[27])
                data["Quat_X"] = int((data["Quat_X_1"] << 8) + data["Quat_X_0"]) # create the full number in int needs to be devided by 16383 and multiplied with 9.80665
                #
                data["Quat_Y_0"] = int(data_seq[28])
                data["Quat_Y_1"] = int(data_seq[29])
                data["Quat_Y"] = int((data["Quat_Y_1"] << 8) + data["Quat_Y_0"]) # create the full number in int needs to be devided by 16383 and multiplied with 9.80665
                #
                data["Quat_Z_0"] = int(data_seq[30])
                data["Quat_Z_1"] = int(data_seq[31])
                data["Quat_Z"] = int((data["Quat_Z_1"] << 8) + data["Quat_Z_0"]) # create the full number in int needs to be devided by 16383 and multiplied with 9.80665
                #
                data["Accel_X_0"] = int(data_seq[32])
                data["Accel_X_1"] = int(data_seq[33])
                data["Accel_X"] = int((data["Accel_X_1"] << 8) + data["Accel_X_0"]) # create the full number in int needs to be devided by 16383 and multiplied with 9.80665
                #
                data["Accel_Y_0"] = int(data_seq[34])
                data["Accel_Y_1"] = int(data_seq[35])
                data["Accel_Y"] = int((data["Accel_Y_1"] << 8) + data["Accel_Y_0"]) # create the full number in int needs to be devided by 16383 and multiplied with 9.80665
                #
                data["Accel_Z_0"] = int(data_seq[36])
                data["Accel_Z_1"] = int(data_seq[37])
                data["Accel_Z"] = int((data["Accel_Z_1"] << 8) + data["Accel_Z_0"]) # create the full number in int needs to be devided by 16383 and multiplied with 9.80665
                #
                data["IndexF_0"] = int(data_seq[38])
                data["MiddleF_0"] = int(data_seq[39])
                data["RingF_0"] = int(data_seq[40])
                data["LittleF_0"] = int(data_seq[41])
                data["IndexF_1"] = int(data_seq[42])
                data["MiddleF_1"] = int(data_seq[43])
                data["RingF_1"] = int(data_seq[44])
                data["LittleF_1"] = int(data_seq[45])
                data["Thumb_0"] = int(data_seq[46])
                data["IndexF_tip"] = int(data_seq[47])
                data["MiddleF_tip"] = int(data_seq[48])
                data["RingF_tip"] = int(data_seq[49])
                data["LittleF_tip"] = int(data_seq[50])
                #
                data["reserved1_0"] = int(data_seq[51])
                data["reserved1_1"] = int(data_seq[52])
                data["reserved1_2"] = int(data_seq[53])
                data["reserved1_3"] = int(data_seq[54])
                data["reserved1_4"] = int(data_seq[55])
                data["reserved1_5"] = int(data_seq[56])
                data["reserved1_6"] = int(data_seq[57])
                data["reserved1_7"] = int(data_seq[58])
                data["reserved1_8"] = int(data_seq[59])
                data["reserved1_9"] = int(data_seq[60])
                data["reserved1_10"] = int(data_seq[61])
                data["reserved1_11"] = int(data_seq[62])
                data["reserved1_12"] = int(data_seq[63])
                #debug purposes
                #
                #hardcode debug
                #data["IndexF_1"] = 201
                #print(data["reserved_17"])
                #print("updated")
                #print(str(data["Quat_X"]))
                #
                #buffer reset
                ser1.reset_input_buffer() #clear the buffer of any data so the next line can come through properly
                #endif
                #hardcoded overrides
                #data["IndexF_0"] = 205
                #data["IndexF_tip"] = 205
                #data["Thumb_0"] = 205

       except:
        #print("no connection all data is 0")
        pass

##endupdate
try:
    time.sleep(2) #gives time for the buffer to fill, will only run once during the include
    update()
except:
   print("update failed in python101, is the device connected?, only 0's will be returned")
#lets the update method run infinitely in the background without locking any threads
