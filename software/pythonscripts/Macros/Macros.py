#!/usr/bin/python3.8
from ctypes import windll, Structure, c_long, byref
import pyautogui


class POINT(Structure):
    _fields_ = [("x", c_long), ("y", c_long)]

import datetime

#Variable flex time


#Variables for fingers - 10 sensors, each finger has 2
flexFinger1 = 0.00J
flexFinger2 = 0.00J
flexFinger3 = 0.00J
flexFinger4 = 0.00J
flexFinger5 = 0.00J
flexFinger6 = 0.00J
flexFinger7 = 0.00J
flexFinger8 = 0.00J
flexFinger9= 0.00J
flexFinger10 = 0.00J

wijsVinger = False

#Variables for touch sensors
touchFinger1 = 0.00J
touchFinger2 = 0.00J
touchFinger3 = 0.00J
touchFinger4 = 0.00J

#Variable rotation time
timeRotation = 0

#Variables rotation
rotationXaxis = 0.00
rotationYaxis = 0.00
rotationZaxis = 0.00

#Variables acceleration
accelerationTime = 0
accelerationXaxis = 0.00
accelerationYaxis = 0.00
accelerationZaxis = 0.00

#the indexfinger is bend when the value of the flex resistor (2 flex sensors on each finger) is larger than 230 for each
#the indexfinger is not bend when the value is smaller than 20
if(flexFinger3<=20 and flexFflexFinger4<=20):
    wijsVinger=False
elif(flexFinger3>=230 and flexFinger4>=230):
    wijsVinger=True

#the CallMacro function gets the value of each finger
#in the MacroClass the corresponding macro gets activated
#def CallMacro(wijsVinger)

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

PrintScreen()
CloseCommandPrompt()