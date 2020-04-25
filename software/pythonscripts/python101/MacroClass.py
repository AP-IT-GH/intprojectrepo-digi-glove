import pyautogui

class Macro():
    def __init__(self, name, macro, trigger):
        self.Name = name
        self.Excecutable = Excecutable()
        self.Trigger = trigger
        self.CanTrigger = True
        if("+" in macro):
            self.Excecutable = Hotkey(macro.split("+"))
        else:
            self.Excecutable = TypeWrite(macro)
    def Excecute(self):
        self.Excecutable.Excecute()

    def TryTrigger(self, trigger):
        if self.Trigger==trigger and self.CanTrigger == True:
            self.CanTrigger = False
            self.Excecute()

    def ResetTrigger():
        self.CanTrigger = True

class Excecutable():
    def __init__(self):
        self.data = []
    def Excecute(self):
        print("Excecute wasn't overriden")

class Hotkey(Excecutable):
    def __init__(self, args):
        super().__init__()
        for arg in args:
            if arg != "":
                self.data.append(arg)
    def Excecute(self):
        print("Excecute called on Hotkey")
        if len(self.data) == 1:
            pyautogui.hotkey(self.data[0])
        if len(self.data) == 2:
            pyautogui.hotkey(self.data[0], self.data[1])
        if len(self.data) == 3:
            pyautogui.hotkey(self.data[0], self.data[1], self.data[2])
        if len(self.data) == 4:
            pyautogui.hotkey(self.data[0], self.data[1], self.data[2], self.data[3])
        for arg in self.data:
            print(arg)

class TypeWrite(Excecutable):
    def __init__(self, args):
        self.data = args       

    def Excecute(self):
        print("Excecute called on TypeWrite")
        pyautogui.typewrite(self.data[0])
        print(self.data)



