#!/usr/bin/python3.8
from ctypes import windll, Structure, c_long, byref
import pyautogui
import socket



class POINT(Structure):
    _fields_ = [("x", c_long), ("y", c_long)]

import datetime

#Variable flex time


#Variables for fingers - 10 sensors, each finger has 2
flexFinger1 = 0
flexFinger2 = 0
flexFinger3 = 0
flexFinger4 = 0
flexFinger5 = 0
flexFinger6 = 0
flexFinger7 = 0
flexFinger8 = 0
flexFinger9= 0
flexFinger10 = 0

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

#Variables for touch sensors
touchFinger1 = 0
touchFinger2 = 0
touchFinger3 = 0
touchFinger4 = 0

#Variable rotation time
timeRotation = 0

#Variables rotation
rotationXaxis = 0
rotationYaxis = 0
rotationZaxis = 0

#Variables acceleration
accelerationTime = 0
accelerationXaxis = 0
accelerationYaxis = 0
accelerationZaxis = 0



#socket
listensocket=socket.socket()
Port=8000
maxConnections=999
IP=socket.gethostname()

listensocket.bind(('',Port))
print("server started at "+IP+" on port "+str(Port))

(clientsocket, address)=listensocket.accept()
print("New connection made!")


def RightMouseClick():
    pt = POINT()
    windll.user32.GetCursorPos(byref(pt))
    pyautogui.rightClick(x=pt.x, y=pt.y)

def LeftMouseClick():
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

while True:
    message=clientsocket.recv(1024).decode()
    print(message)

    #convert string to the 5 macro's that have to be executed

    #the indexfinger is bend when the value of the flex resistor (2 flex sensors on each finger) is larger than 200 for each
    if(flexFinger1>=200 and flexFinger2>=200):
        thumb=True
    else:
        thumb=False
    if(flexFinger3>= 200 and flexFinger4>=200):
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

    if(indexfinger): PrinScreen();