#!/usr/bin/python3.8

import pyautogui
import socket
import datetime
import python101
import threading
from threading import Thread
import time
import MacroClass
from tkinter import Tk,Label,Button
from random import randrange
import math
import numpy
import os

message = "PrintScreen-PrintScreen-PrintScreen-PrintScreen-PrintScreen" #overidden from Zeno's gui
print("loaded libraries")          
MacrosList = []

#Variables for fingers - 10 sensors, each finger has 2
flexIndex0 = python101.data["IndexF_0"]
flexIndex1 = python101.data["IndexF_1"]
flexMiddle0 = python101.data["MiddleF_0"]
flexMiddle1 = python101.data["MiddleF_1"]
flexRing0 = python101.data["RingF_0"]
flexRing1 = python101.data["RingF_1"]
flexPink0 = python101.data["LittleF_0"]
flexPink1 = python101.data["LittleF_1"]
flexThumb = python101.data["Thumb_0"]

touchIndex = 200 #python101.data["IndexF_tip"]
touchMiddle = 200 #python101.data["MiddleF_tip"]
touchRing = python101.data["RingF_tip"]
touchPink = python101.data["LittleF_tip"]

Triggerpoint = 200
ResetTriggerPoint = 150

xmin, ymin = 0, 0
xmax = 1920    # Width of the monitor
ymax = 1080   # Height of the 
PreviousstateX = xmax/2            # starts in the middle of the screen
PreviousstateY = ymax/2
duration = 0.05  # Duration of mouse movement on seconds (float)

#Variable rotation time
timeRotation = 0

#Variables rotation
rotationXaxis = 0
rotationYaxis = 0
rotationZaxis = 0

#Variables acceleration
accelerationXaxis = 0
accelerationYaxis = 0


def ClosePage():
    pyautogui.hotkey('ctrl', 'w')  

def Copy():
    pyautogui.hotkey('ctrl', 'c')  

def Paste():
    pyautogui.hotkey('ctrl', 'v')

def PrintScreen():
    pyautogui.hotkey('ctrl', 'PrtSc')

def CloseCommandPrompt():
    pyautogui.hotkey("esc")

def Save():
    pyautogui.hotkey('ctrl','s')

def Undo():
    pyautogui.hotkey('ctrl','z')

def Refresh():
    pyautogui.hotkey('F5')

def SelectAll():
    pyautogui.hotkey('ctrl','a')

def Cut():
    pyautogui.hotkey('ctrl','x')

def Bold():
    pyautogui.hotkey('ctrl','b')

def PauseGlove():
    global gloveActivated
    if(gloveActivated==True): 
        print("Glove OFF")
        #gloveActivated=False #true for debugs
        return
    if(gloveActivated==False): 
        print("Glove ON")
        #gloveActivated=True
        return

def CheckFingers():
    #Trigger checking
    if(flexThumb>=Triggerpoint):
        CheckTrigger("flexthumb")
    if(flexIndex1>=Triggerpoint):
        CheckTrigger("flexindex")
    if(flexMiddle1>=Triggerpoint): # 200 200
        CheckTrigger("flexmiddle")
    if(flexRing1>=Triggerpoint):
        CheckTrigger("flexring")
    if(flexPink0>=Triggerpoint):
        CheckTrigger("flexpink")

    if(touchIndex>=Triggerpoint):
        CheckTrigger("touchindex")
    if(touchMiddle>=Triggerpoint):
        CheckTrigger("touchmiddle")
    if(touchRing>=Triggerpoint):
        CheckTrigger("touchring")
    if(touchPink>=Triggerpoint):
        CheckTrigger("touchpink")

    #CanTrigger again
    if(flexThumb<=ResetTriggerPoint):
        ResetTrigger ("flexthumb")
    if(flexIndex1<=ResetTriggerPoint):
        ResetTrigger("flexindex")
    if(flexMiddle1<=ResetTriggerPoint): # 200 200
        ResetTrigger("flexmiddle")
    if(flexRing1<=ResetTriggerPoint):
        ResetTrigger("flexring")
    if(flexPink0<=ResetTriggerPoint):
        ResetTrigger("flexpink")

    if(touchIndex<=ResetTriggerPoint):
        ResetTrigger("touchindex")
    if(touchMiddle<=ResetTriggerPoint):
        ResetTrigger("touchmiddle")
    if(touchRing<=ResetTriggerPoint):
        ResetTrigger("touchring")
    if(touchPink<=ResetTriggerPoint):
        ResetTrigger("touchpink")

def DisableAllMacrosExcept(name):
    global MacrosList
    print("Disabling all marco's")
    for macroToDisable in MacrosList:
        if not macroToDisable.Name == name: #All other macro's other than the disabling macro
            print("Disabled: " + macroToDisable.Name)
            macroToDisable.ChangeLock()

def CheckTrigger(trigger):
    for macro in MacrosList:
        if macro.TryTrigger(trigger) == False:
            DisableAllMacrosExcept(macro.Name)
            

def ResetTrigger(trigger):
    for macro in MacrosList:
        macro.ResetTrigger(trigger)



       #callable function for the thread
def CallUpdate():
    global flexThumb, flexIndex0, flexIndex1, flexMiddle0, flexMiddle1, flexRing0, flexRing1, flexPink0, accelerationXaxis, accelerationYaxis
    #update values from the Bluetooth
    flexThumb = 200 #python101.data["Thumb_0"]
    flexIndex0 = python101.data["IndexF_0"]
    flexIndex1 = python101.data["IndexF_1"]
    flexMiddle0 = python101.data["MiddleF_0"]
    flexMiddle1 = python101.data["MiddleF_1"]
    flexRing0 = python101.data["RingF_0"]
    flexRing1 = python101.data["RingF_1"]
    flexPink0 = python101.data["LittleF_0"]
    flexPink1 = python101.data["LittleF_1"]

    touchIndex = 200 #python101.data["IndexF_tip"]
    touchMiddle = 200 #python101.data["MiddleF_tip"]
    touchRing = python101.data["RingF_tip"]
    touchPink = python101.data["LittleF_tip"]
    accelerationXaxis = python101.data["Accel_X"]
    accelerationYaxis = python101.data["Accel_Y"]
    #endloop
#endcallupdate

def CheckMousemovement():
    #HIER DE CODE DIE ELKE KEER MOET GERUND WORDEN. (eigenlijk een while loop dus niet te zwaar belasten maar enkel de variabelen die nodig zijn of stuk code)
     
        # Ignore fails:
        pyautogui.FAILSAFE = False

        global xmin, ymin 
        global xmax
        global ymax  
        global PreviousstateX
        global PreviousstateY 
        global duration


        accelerationXaxis = python101.data["Accel_X"]
        accelerationYaxis = python101.data["Accel_Y"]

        
        MaccelerationXaxis = (accelerationXaxis/16383)*9.80665#*xmax #m/s²
        MaccelerationYaxis = (accelerationYaxis/16383)*9.80665#*ymax
        #print(str(MaccelerationXaxis) + "  " + str(MaccelerationYaxis))
        PreviousstateX += MaccelerationXaxis
        PreviousstateY += MaccelerationYaxis
        PreviousstateX = math.floor(PreviousstateX)
        PreviousstateY = math.floor(PreviousstateY)
        #print(str(PreviousstateX) + " " + str(PreviousstateY))
        #pyautogui.moveTo(x=PreviousstateX,y=PreviousstateY,duration=duration)


class updateThread(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
    def run(self):
        while True:
            CallUpdate() #this is nonblocking it is a background thread
            #make it run at aprox 200Hz
            time.sleep(0.005)
updateThread()

class updateFingers(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
    def run(self):
        while True:
           CheckFingers()
           #run this at aprox 200Hz
           time.sleep(0.005)
updateFingers()

class updateMousemovement(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
    def run(self):
        while True:
            CheckMousemovement()
            time.sleep(0.005)
updateMousemovement()


print("initiating sockets")
socket
listensocket=socket.socket()
Port=8000
maxConnections=999
IP=socket.gethostname()

listensocket.bind(('',Port))

listensocket.listen(maxConnections);
print("server started at "+IP+" on port "+str(Port))

(clientsocket, address)=listensocket.accept()
print("New connection made!")

#start the threading
print("now running")
#updateThread.start(); #thread is started autmatically by importing
#checkFingerThread.start();


while True:
    #socket-message
    data=clientsocket.recv(1024).decode()
    if(data!=""):
        print("message received")
        splitData=data.split("%")
        print(str(splitData))
        for dataRow in splitData:
            print(dataRow)
            if dataRow=="exit":
                os._exit(1)
            if dataRow!="":
                macroData = dataRow.split("~")
                duplicate=False
                for macro in MacrosList:
                    if(macroData[0]==macro.Name):
                        duplicate=True
                if (duplicate):
                    print("Macro " + macroData[0] + " already exists")
                else:
                    MacrosList.append(MacroClass.Macro(macroData[0],macroData[1],macroData[2]))
