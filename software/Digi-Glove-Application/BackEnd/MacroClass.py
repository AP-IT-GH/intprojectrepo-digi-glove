import pyautogui
from ctypes import windll, Structure, c_long, byref

class POINT(Structure):
    _fields_ = [("x", c_long), ("y", c_long)]

class Macro():
    def __init__(self, name, macro, trigger):
        self.Name = name
        self.Excecutable = Excecutable()
        self.Trigger = trigger
        self.CanTrigger = True
        self.Locked = False
        if macro == "leftmouseclick" or macro == "rightmouseclick" == macro or "disable" == macro:
            self.Excecutable = SpecialExcecutable(macro)
        elif("+" in macro):
            self.Excecutable = Hotkey(macro.split("+"))
        else:
            self.Excecutable = TypeWrite(macro)

    def Excecute(self):
        return self.Excecutable.Excecute()

    def TryTrigger(self, trigger):
        if self.Trigger==trigger and self.CanTrigger == True and self.Locked == False:
            self.CanTrigger = False
            return self.Excecute()
    def ResetTrigger(self, trigger):
        if self.Trigger == trigger:
            self.CanTrigger = True

    def ChangeLock(self):
        self.Locked = not self.Locked

class Excecutable():
    def __init__(self):
        self.data = []
    def Excecute(self):
        print("Excecute was not overridden")
        

class Hotkey(Excecutable):
    def __init__(self, args):
        super().__init__()
        for arg in args:
            if arg != "":
                self.data.append(arg)
    def Excecute(self):
        print("Excecute called on Hotkey : " + str(self.data))
        if len(self.data) == 1:
            pyautogui.hotkey(self.data[0])
        if len(self.data) == 2:
            pyautogui.hotkey(self.data[0], self.data[1])
        if len(self.data) == 3:
            pyautogui.hotkey(self.data[0], self.data[1], self.data[2])
        if len(self.data) == 4:
            pyautogui.hotkey(self.data[0], self.data[1], self.data[2], self.data[3])
        if len(self.data) > 4:
            print("Too many arguments in hotkey")
        #for arg in self.data:
            #print(arg)
        return True

class TypeWrite(Excecutable):
    def __init__(self, args):
        self.data = args       

    def Excecute(self):
        print("Excecute called on TypeWrite: " + str(self.data))
        pyautogui.typewrite(self.data[0])
        #print(self.data)
        return True

class SpecialExcecutable(Excecutable):
    def __init__(self, args):
        self.data = args
    def rightmouseclick(self):
        pt = POINT()
        windll.user32.GetCursorPos(byref(pt))
        pyautogui.rightClick(x=pt.x, y=pt.y)
        return True

    def leftmouseclick(self):
        pt = POINT()
        windll.user32.GetCursorPos(byref(pt))
        pyautogui.click(x=pt.x, y=pt.y)
        return True
    
    def disable(self):
        print("Disabled all macro's")
        return False
        #the class macro should lock all other macro's

    def Excecute(self):
        print("Excecute called on SpecialExcecute: " + self.data +'()')
        return eval("self." + self.data + '()')