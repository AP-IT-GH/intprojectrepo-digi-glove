#!/usr/bin/python3.8
from ctypes import windll, Structure, c_long, byref
import pyautogui
import socket
import datetime
import python101
import threading
from threading import Thread
import time
from tkinter import Tk,Label,Button
from random import randrange
import math
import numpy

message = "PrintScreen-PrintScreen-PrintScreen-PrintScreen-PrintScreen" #overidden from Zeno's gui
print("loaded libraries")

class POINT(Structure):
    _fields_ = [("x", c_long), ("y", c_long)]



#Variables for fingers - 10 sensors, each finger has 2
flexFinger1 = python101.data["Thumb_0"]
#flexFinger2 = python101.data["Thumb_1"] # missing as of 10/04/2020 because of hardware and software changes
flexFinger2 = python101.data["Thumb_0"] #copied from thumb 0 to not create a null reference
flexFinger3 = python101.data["IndexF_1"]
flexFinger4 = python101.data["IndexF_0"]
flexFinger5 = python101.data["MiddleF_1"]
flexFinger6 = python101.data["MiddleF_0"]
flexFinger7 = python101.data["RingF_tip"]
flexFinger8 = python101.data["RingF_0"]
flexFinger9 = python101.data["LittleF_1"]
flexFinger10 = python101.data["LittleF_0"]


gloveActivated = True
indexSplitMessage=0
gloveActivatedFinger=0

thumb = False
indexFinger = False
middleFinger = False
ringFinger = False
littleFinger = False

thumbHalf = False
indexHalf = False
middleHalf = False
ringHalf = False
littleHalf = False

thumbMacro="";
indexMacro="";
middleMacro="";
ringMacro="";
littleMacro="";

#Variables for touch sensors
touchFinger1 = 0
touchFinger2 = 0
touchFinger3 = 0
touchFinger4 = 0



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
    pyautogui.Click(x=pt.x, y=pt.y)

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
    #the indexfinger is bend when the value of the flex resistor (2 flex sensors on each finger) is larger than 200 for each
    if(flexFinger1>=200 and flexFinger2>=200):
        thumb=True
    else:
        thumb=False
    if(flexFinger3>= 200 and flexFinger4>=200): # 200 200
        global indexFinger
        indexFinger=True
    else:
        indexFinger=False
    if(flexFinger5>=200 and flexFinger6>=200):
        middleFinger=True
    else:
        middleFinger=False

    if(flexFinger7>=200 and flexFinger8>=200):
        ringFinger=True
    else:
        ringFinger=False

    if(flexFinger9>=200 and flexFinger10>=200):
        littleFinger=True
    else:
       littleFinger=False

       #callable function for the thread
def CallUpdate():
    global flexFinger1, felxFinger2, flexFinger3, flexFinger4, flexFinger5, flexFinger6, flexFinger7, flexFinger9, flexFinger10, accelerationXaxis, accelerationYaxis
    #update values from the Bluetooth
    python101.update()
    flexFinger1 = python101.data["Thumb_0"]
    #flexFinger2 = python101.data["Thumb_1"] # missing as of 10/04/2020 because of hardware and software changes
    flexFinger2 = python101.data["Thumb_0"] #copied from thumb 0 to not create a null reference
    flexFinger3 = python101.data["IndexF_1"]
    flexFinger4 = python101.data["IndexF_0"]
    flexFinger5 = python101.data["MiddleF_1"]
    flexFinger6 = python101.data["MiddleF_0"]
    flexFinger7 = python101.data["RingF_1"]
    flexFinger8 = python101.data["RingF_0"]
    flexFinger9= python101.data["LittleF_1"]
    flexFinger10 = python101.data["LittleF_0"]
    accelerationXaxis = python101.data["Accel_X"]
    accelerationYaxis = python101.data["Accel_Y"]
    #print(str(flexFinger3) + " " + str(flexFinger4)) #for demo purposes
    #endloop
#endcallupdate

def CheckPauseGlove():
    SplitMessage = message.split("-")
    indexSplitMessage = 0
    for macro in SplitMessage:
        if(macro == "PauseGlove"):
            #print("macro = pause")
            gloveActivatedFinger = indexSplitMessage
            indexSplitMessage = indexSplitMessage + 1
        else:
            gloveActivatedFinger = 5 #failsafe
            #print("macro is not pause")

    #when you bend the finger that is assigned to let the glove be paused and used: if you bend the glove it's inactive, if you bend that finger again, the glove is back active.
    if(gloveActivatedFinger==0):
        if(thumb):
            PauseGlove()
    elif(gloveActivatedFinger==1):
        if(indexFinger):
            PauseGlove()
    elif(gloveActivatedFinger==2):
        if(middleFinger):
            PauseGlove()
    elif(gloveActivatedFinger==3):
       if(ringFinger):
            PauseGlove()
    elif(gloveActivatedFinger==4):
        if(littleFinger):
            PauseGlove()
    elif(gloveActivatedFinger==5):
        pass #failsafe



def ValidationFingers():
    SplitMessage = message.split("-")
    CheckPauseGlove()

    if(True):
        if(thumb): eval(SplitMessage[0]+'()')
        if(indexFinger): eval(SplitMessage[1]+'()')
        if(middleFinger): eval(SplitMessage[2]+'()')
        if(ringFinger): eval(SplitMessage[3]+'()')
        if(littleFinger): eval(SplitMessage[4]+'()')
        #print(SplitMessage[1])


       

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

class validationFingers(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon=True
        self.start()
    def run(self):
        while True:
           ValidationFingers()
           time.sleep(0.05) #2Hz
validationFingers()


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
    print("message received")
    #socket-message
    message=clientsocket.recv(1024).decode()
    if(message!=""):
        SplitMessage=message.split("-")
        print(str(message))

