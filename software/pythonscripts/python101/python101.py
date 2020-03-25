import Macros
import serial
import time

port = "COM15"

ser1 = serial.Serial(port, 9600, 8)
print("sending data continiously to bluetooth devices press ctrl + c to stop")
data = {"reserved_0" : 0 , "reserved_1" : 0 , "reserved_2" : 0 , "reserved_3" : 0 , "reserved_4" : 0 , "reserved_5" : 0 , "reserved_6" : 0 , "reserved_7" : 0 ,  #reserved0
        "timestamp_0" : 0 , "timestamp_1" : 0 , "timestamp_2" : 0, "timestamp_3" : 0, "timestamp_4" : 0, "timestamp_5" : 0, "timestamp_6" : 0, "timestamp_7" : 0, #timestamp
        "Sensortime_0" : 0 , "Sensortime_1" : 0 , "Sensortime_2" : 0 , "Sensortime_3" : 0 , "Sensortime_4" : 0 , "Sensortime_5" : 0 , "Sensortime_6" : 0 , "Sensortime_7" : 0, #sensortime
        "W" : 0 , "X" : 0 , "Y" : 0 , "Z" : 0 , "accel_X" : 0 , "accel_Y" : 0 , "accel_Z" : 0 , #gyroscope
       "IndexF_0" : 0 , "MiddleF_0" : 0 , "RingF_0" : 0 , "LittleF_0" : 0 , "IndexF_1" : 0 , "MiddleF_1" : 0 , "RingF_1" : 0 , "LittleF_1" : 0 , "Thumb_0" : 0 , "Thumb_1" : 0 , #flexstrips
      "IndexF_tip" : 0 , "MiddleF_tip" : 0 , "RingF_tip" : 0 , "LittleF_tip" : 0 , #tips
        "reserved_10" : 0 , "reserved_11" : 0 , "reserved_12" : 0 , "reserved_13" : 0 , "reserved_14" : 0 , "reserved_15" : 0 , "reserved_16" : 0 , "reserved_17" : 0 #reserved1
        }
def update(): #to edit in release
       #ser1.write(0x01)
       if(ser1.inWaiting() >= 72):
            data_seq = ser1.read(74) #read the buffer as soon as it reached 64 bytes aka a full sequence has entered1
            #print(data_seq[0])
            data["reserved_0"] = int(data_seq[0])
            data["reserved_1"] = int(data_seq[1])
            data["reserved_2"] = int(data_seq[2])
            data["reserved_3"] = int(data_seq[3])
            data["reserved_4"] = int(data_seq[4])
            data["reserved_5"] = int(data_seq[5])
            data["reserved_6"] = int(data_seq[6])
            data["reserved_7"] = int(data_seq[7])

            data["timestamp_0"] = int(data_seq[8])
            data["timestamp_1"] = int(data_seq[9])
            data["timestamp_2"] = int(data_seq[10])
            data["timestamp_3"] = int(data_seq[11])
            data["timestamp_4"] = int(data_seq[12])
            data["timestamp_5"] = int(data_seq[13])
            data["timestamp_6"] = int(data_seq[14])
            data["timestamp_7"] = int(data_seq[15])

            data["Sensortime_0"] = int(data_seq[16])
            data["Sensortime_1"] = int(data_seq[17])
            data["Sensortime_2"] = int(data_seq[18])
            data["Sensortime_3"] = int(data_seq[19])
            data["Sensortime_4"] = int(data_seq[20])
            data["Sensortime_5"] = int(data_seq[21])
            data["Sensortime_6"] = int(data_seq[22])
            data["Sensortime_7"] = int(data_seq[23])

            data["W0"] = int(data_seq[24])
            data["W1"] = int(data_seq[25])
            data["W2"] = int(data_seq[26])
            data["W3"] = int(data_seq[27])

            data["X0"] = int(data_seq[28])
            data["X1"] = int(data_seq[29])
            data["X2"] = int(data_seq[30])
            data["X3"] = int(data_seq[31])

            data["Y0"] = int(data_seq[32])
            data["Y1"] = int(data_seq[33])
            data["Y2"] = int(data_seq[34])
            data["Y3"] = int(data_seq[35])

            data["Z0"] = int(data_seq[36])
            data["Z1"] = int(data_seq[37])
            data["Z2"] = int(data_seq[38])
            data["Z3"] = int(data_seq[39])

            data["accel_X0"] = int(data_seq[40])
            data["accel_X1"] = int(data_seq[41])
            data["accel_X2"] = int(data_seq[42])
            data["accel_X3"] = int(data_seq[43])
            data["accel_Y0"] = int(data_seq[44])
            data["accel_Y1"] = int(data_seq[45])
            data["accel_Y2"] = int(data_seq[46])
            data["accel_Y3"] = int(data_seq[47])
            data["accel_Z0"] = int(data_seq[48])
            data["accel_Z1"] = int(data_seq[49])
            data["accel_Z2"] = int(data_seq[50])
            data["accel_Z3"] = int(data_seq[51])

            data["IndexF_0"] = int(data_seq[52])
            data["MiddleF_0"] = int(data_seq[53])
            data["RingF_0"] = int(data_seq[54])
            data["LittleF_0"] = int(data_seq[55])
            data["IndexF_1"] = int(data_seq[56])
            data["MiddleF_1"] = int(data_seq[57])
            data["RingF_1"] = int(data_seq[58])
            data["LittleF_1"] = int(data_seq[59])
            data["Thumb0"] = int(data_seq[60])
            data["thumb1"] = int(data_seq[61])
            data["IndexF_tip"] = int(data_seq[62])
            data["MiddleF_tip"] = int(data_seq[63])
            data["RingF_tip"] = int(data_seq[64])
            data["LittleF_tip"] = int(data_seq[65])

            data["reserved_10"] = int(data_seq[66])
            data["reserved_11"] = int(data_seq[67])
            data["reserved_12"] = int(data_seq[68])
            data["reserved_13"] = int(data_seq[69])
            data["reserved_14"] = int(data_seq[70])
            data["reserved_15"] = int(data_seq[71])
            data["reserved_16"] = int(data_seq[72])
            data["reserved_17"] = int(data_seq[73])
            #print(str(data_seq["reserved_ALL"]))
            time.sleep(1) # set frequency of fetches
            #forging the library


            #debug purposes

            print(data["timestamp_0"])
            print(data["reserved_17"])
            #print(data["timestamp_1"])
            #print(data["timestamp_2"])
            #print(data["timestamp_3"])
            #print(data["timestamp_4"])

            #print(data["reserved_0"])
            #print(data["reserv#ed_1"])
            #print(data["reserv#ed_2"])
            #print(data["reserv#ed_3"])
            #print(data["reserv#ed_4"])
#
            #buffer reset
#
#            ser1.reset_input_buffer() #clear the buffer of any data so the next line can come through properly
#       #data["value1"] = ser1.read()
#       #time.sleep(0.005) #decide the frequency of pulls esp data is presumed live
##endupdate
while True:
    update()
