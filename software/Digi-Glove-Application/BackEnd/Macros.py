#!/usr/bin/python3.8

import pyautogui
import win32api, win32con
import socket
import datetime
import python101
import threading
from threading import Thread
import time
import MacroClass
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

touchIndex = python101.data["IndexF_tip"]
touchMiddle = python101.data["MiddleF_tip"]
touchRing = python101.data["RingF_tip"]
touchPink = python101.data["LittleF_tip"]

Touchtrigger = 80
Triggerpoint = 200
ResetTriggerPoint = 150


xmin, ymin = 0, 0
xmax = 1920    # Width of the monitor
ymax = 1080   # Height of the 
PreviousstateX = xmax/2            # starts in the middle of the screen
PreviousstateY = ymax/2
duration = 0.25  # Duration of mouse movement on seconds (float) higher is smoother but not too high
offset = 0
offset2 = 0
minread = 0
minread2 = 0
maxread = 0
maxread2 = 0
#Variable rotation time
timeRotation = 0

#Variables rotation
rotationXaxis = 0
rotationYaxis = 0
rotationZaxis = 0

#Variables acceleration
accelerationXaxis = 0
accelerationYaxis = 0

#endregion

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
#endregion

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

    if(touchIndex>=Touchtrigger):
        CheckTrigger("touchindex")
    if(touchMiddle>=Touchtrigger):
        CheckTrigger("touchmiddle")
    if(touchRing>=Touchtrigger):
        CheckTrigger("touchring")
    if(touchPink>=Touchtrigger):
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

    if(touchIndex<=20):
        ResetTrigger("touchindex")
    if(touchMiddle<=20):
        ResetTrigger("touchmiddle")
    if(touchRing<=20):
        ResetTrigger("touchring")
    if(touchPink<=20):
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
    global flexThumb, flexIndex0, flexIndex1, flexMiddle0, flexMiddle1, flexRing0, flexRing1, flexPink0, accelerationXaxis, accelerationYaxis, touchIndex, touchMiddle, touchPink, touchRing
    #update values from the Bluetooth
    python101.update()
    flexThumb = python101.data["Thumb_0"]
    flexIndex0 = python101.data["IndexF_0"]
    flexIndex1 = python101.data["IndexF_1"]
    flexMiddle0 = python101.data["MiddleF_0"]
    flexMiddle1 = python101.data["MiddleF_1"]
    flexRing0 = python101.data["RingF_0"]
    flexRing1 = python101.data["RingF_1"]
    flexPink0 = python101.data["LittleF_0"]
    touchIndex = python101.data["IndexF_tip"]
    touchMiddle = python101.data["MiddleF_tip"]
    flexPink1 = python101.data["LittleF_1"]
    
    touchIndex = python101.data["IndexF_tip"]
    touchMiddle = python101.data["MiddleF_tip"]
    touchRing = python101.data["RingF_tip"]
    touchPink = python101.data["LittleF_tip"]
    accelerationXaxis = python101.data["Accel_X"]
    accelerationYaxis = python101.data["Accel_Y"]
    #print(touchMiddle)
    #endloop
#endcallupdate

def constrain(val, min_val, max_val):

    if val < min_val: 
        val = min_val

    elif val > max_val: 
        val = max_val

    return val

def map(OldValue,OldMin,OldMax,NewMin,NewMax):

    OldRange = (OldMax - OldMin)  
    NewRange = (NewMax - NewMin)  
    NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin

    return NewValue

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
        global offset
        global offset2
        global minread, maxread, minread2, maxread2

        accelerationXaxis = python101.data["yaw"]
        accelerationYaxis = python101.data["pitch"]

        if(accelerationXaxis*2000 > 950 and accelerationXaxis*2000 > maxread):
            maxread = accelerationXaxis*2000
            offset = maxread -950
            minread = 0
        if(accelerationXaxis*2000 <- 950 and accelerationXaxis*2000 < minread):
            minread = accelerationXaxis*2000
            offset = maxread -950
            maxread = 0

        if(accelerationYaxis*2000 > 500 and accelerationXaxis*2000 > maxread2):
            maxread = accelerationXaxis*2000
            offset2 = maxread2 -500
            minread2 = 0
        if(accelerationYaxis*2000 <- 500 and accelerationXaxis*2000 < minread2):
            minread2 = accelerationXaxis*2000
            offset2 = maxread -950
            maxread2 = 0
            
        xx = constrain(accelerationXaxis*2000-offset,-950,950)
        yy = constrain(accelerationYaxis*-2000+offset2,-500,500)
        
        #print(str(MaccelerationXaxis) + "  " + str(MaccelerationYaxis))
      
        PreviousstateX = map(xx,-950,950,0,1920)
        PreviousstateY = map(yy,-500,500,0,1080)

        PreviousstateX = math.floor(PreviousstateX)
        PreviousstateY = math.floor(PreviousstateY)
        if(python101.connected == True):
            try:
                win32api.SetCursorPos((int(PreviousstateX),int(PreviousstateY)))
            except:
                print("failed movement")

class updateThread(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
    def run(self):
        while True:
            CallUpdate() #this is nonblocking it is a background thread
            #make it run at aprox 100Hz
            time.sleep(0.01)
updateThread()

class updateFingers(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
    def run(self):
        while True:
           CheckFingers()
           #run this at aprox 100Hz
           time.sleep(0.01)
updateFingers()

class updateMousemovement(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
    def run(self):
        while True:
            CheckMousemovement()
            time.sleep(0.01) #100Hz
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
                    macro.Name = macroData[0]
                    macro.Trigger = macroData[1]
                    macro.Excecutable = macroData[2]
                    print("Updated macro: " + macro.Name)
                else:
                    MacrosList.append(MacroClass.Macro(macroData[0],macroData[1],macroData[2]))
