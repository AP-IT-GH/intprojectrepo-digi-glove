import pyautogui, time
from tkinter import Tk,Label,Button
from random import randrange

#todo: add a button for stop (Thread?)

class Application(Tk):

    def __init__(self):
        # build parents:
        Tk.__init__(self)

        # Ignore fails:
        pyautogui.FAILSAFE = False

        # state flag for switch on/off:
        self.state = False

        # Settings:
        self.xmin, self.ymin = 0, 0
        self.xmax = self.winfo_screenwidth()    # Width of the monitor
        self.ymax = self.winfo_screenheight()   # Height of the monitor
        self.duration = 0.8  # Duration of mouse movement on seconds (float)
        self.waitTime = 3000   # wait time on seconds (int)

        # Interface:
        Label(self,text='Move Automatically:').grid(row='1',column='1',columnspan='2',padx='5',pady='5')
        Button(self,text='On',command=self.on).grid(row='2',column='1',padx='5',pady='5')
        #Button(self,text='Off',command=self.off).grid(row='2',column='2',padx='5',pady='5')
        Label(self,text="Taille de l'écran: "+str(self.xmax)+"x"+str(self.ymax)).grid(row='3',column='1',columnspan='2',padx='5',pady='5')

    def on(self):
        "Actions when is turned on"

        # Switch the flag to on:
        self.state = True

        # Do this while is on:
        while self.state == True:
            # Moving mouse:
            pyautogui.moveTo(x=randrange(self.xmin,self.xmax),y=randrange(self.ymin,self.ymax),duration=self.duration)
            pyautogui.click()
            # Time to sleep:
            self.after(self.waitTime)

    def off(self):
        "Turn off"

        # Switch the flag to off
        self.state = False

App = Application()
App.mainloop()