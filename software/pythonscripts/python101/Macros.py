#!/usr/bin/python3.8
from ctypes import windll, Structure, c_long, byref
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

message = "PrintScreen-PrintScreen-PrintScreen-PrintScreen-PrintScreen" #overidden from Zeno's gui
print("loaded libraries")          
MacrosList = []

class POINT(Structure):
    _fields_ = [("x", c_long), ("y", c_long)]



#Variables for fingers - 10 sensors, each finger has 2
flexIndex0 = python101.data["IndexF_0"]
flexIndex1 = python101.data["IndexF_1"]
flexMiddle0 = python101.data["MiddleF_0"]
flexMiddle1 = python101.data["MiddleF_1"]
flexRing0 = python101.data["RingF_0"]
flexRing1 = python101.data["RingF_1"]
flexPink0 = python101.data["LittleF_0"]
flexThumb = 200#python101.data["Thumb_0"]

touchIndex = python101.data["IndexF_tip"]
touchMiddle = python101.data["MiddleF_tip"]
touchRing = python101.data["RingF_tip"]
touchPink = python101.data["LittleF_tip"]



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


def Rightmouseclick():
    pt = POINT()
    windll.user32.GetCursorPos(byref(pt))
    pyautogui.rightClick(x=pt.x, y=pt.y)

def Leftmouseclick():
    pt = POINT()
    windll.user32.GetCursorPos(byref(pt))
    pyautogui.click(x=pt.x, y=pt.y)

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
    if(flexThumb>=200):
        CheckTrigger("flexthumb")
    if(flexIndex0>=200 and flexIndex1>=200):
        CheckTrigger("flexindex")
    if(flexMiddle0>= 200 and flexMiddle1>=200): # 200 200
        CheckTrigger("flexmiddle")
    if(flexRing0>=200 and flexRing1>=200):
        CheckTrigger("flexring")
    if(flexPink0>=200):
        CheckTrigger("flexpink")

    if(touchIndex>=200):
        CheckTrigger("touchindex")
    if(touchMiddle>=200):
        CheckTrigger("touchmiddle")
    if(touchRing>=200):
        CheckTrigger("touchring")
    if(touchPink>=200):
        CheckTrigger("touchpink")


def CheckTrigger(trigger):
    for macro in MacrosList:
        macro.TryTrigger(trigger)

       #callable function for the thread
def CallUpdate():
    global flexThumb, flexIndex0, flexIndex1, flexMiddle0, flexMiddle1, flexRing0, flexRing1, flexPink0, accelerationXaxis, accelerationYaxis
    #update values from the Bluetooth
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
    touchRing = python101.data["RingF_tip"]
    touchPink = python101.data["LittleF_tip"]
    accelerationXaxis = python101.data["Accel_X"]
    accelerationYaxis = python101.data["Accel_Y"]
    #endloop
#endcallupdate

#Rewrite Zeno!!!
#def CheckPauseGlove():
    #SplitMessage = message.split("-")
    #indexSplitMessage = 0
    #for macro in SplitMessage:
        #if(macro == "PauseGlove"):
            #print("macro = pause")
            #gloveActivatedFinger = indexSplitMessage
            #indexSplitMessage = indexSplitMessage + 1
        #else:
            #gloveActivatedFinger = 5 #failsafe
            #print("macro is not pause")

    #when you bend the finger that is assigned to let the glove be paused and used: if you bend the glove it's inactive, if you bend that finger again, the glove is back active.
    #if(gloveActivatedFinger==0):
        #if(thumb):
            #PauseGlove()
    #elif(gloveActivatedFinger==1):
        #if(indexFinger):
            #PauseGlove()
    #elif(gloveActivatedFinger==2):
        #if(middleFinger):
            #PauseGlove()
    #elif(gloveActivatedFinger==3):
       #if(ringFinger):
            #PauseGlove()
    #elif(gloveActivatedFinger==4):
        #if(littleFinger):
            #PauseGlove()
    #elif(gloveActivatedFinger==5):
        #pass #failsafe      

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
