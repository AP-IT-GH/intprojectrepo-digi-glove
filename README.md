# DigiGlove
With this project we try to make a digital glove which can be used to perform macro's.

## General Information
### Privacy Policy
If you want to learn more about our privacy policy, open the following file:
- 'PRIVACYPOLICY_DigiGlove.docx'

### Manual of the Application (GUI)
If you want to learn more about how to use the application or GUI (=Graphic User Interface), open the following file:
- 'ManualDigiGloveApplication.docx'

### Manual of the Glove
- information follows

## The Project
### Connections
## Connection between the glove and the computer
The connection between the glove and the computer ensures a channel through which data is continuously send.
This is a Bluetooth module. The Bluetooth-transmitter (the glove) sends captured data continuously to the Bluetooth-receiver (a COM-port on the PC).

## Connection between the GUI and the running script
The connection between the GUI is used to transfer settings/ a configuration to the script
This connection gets made by using a network socket. The user has to establish a connection between the two parties in the GUI. One of the parties is the Application, the other party is the running script. A string of data with the specifications gets send to the running program.

### Handeling the data
The task of handeling the data is to provide a continuous flow of data, so the variables are up-to-date.
When the data is received on the PC, it gets structured in variables. The continuous updates override the values of the variables each time there is an update. This process gets done by a thread called updateThread in the project.

These values get passed on to the validation variables. This is also a continuous process which is done by another thread called updateFingers in the project. It ensures that the validation variables are always up to date with the latest values received from the glove.

### Validating the data
The task of the validation is to define whether a macro has to be executed or not.
The validation gets done by another thread. This thread is called validationFingers. It’s a continuous loop that checks if the values of certain variables are higher than certain border point. For example you are validating a finger. Once the value of the finger reaches the border point, the finger is seen as bended.
If the validation is true, a corresponding macro gets called and is executed.

### Configuration of the GUI
The task of the GUI is to configure the glove's settings:
- In the GUI you can choose between an amount of predefined macro’s.
- You can choose which macro you want to assign to each finger.
- Each finger can have 1 macro assigned to it.
When you have made a configuration, the configuration gets passed on to the program via a network socket (as beeing explained in the connections part).
