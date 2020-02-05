using System;
using InTheHand.Net.Bluetooth;
using System.Collections.Generic;
using InTheHand.Net.Sockets;
using InTheHand.Net;

namespace bluetooth_test
{
    class Program
    {
        static void Main(string[] args) {
            BT test = new BT();
            test.scan();
            BluetoothAddress address = new BluetoothAddress(0XB827EBE12D86); //bluetoothaddress of the device
            Guid guid = new Guid();
            BluetoothEndPoint myendpoint = new BluetoothEndPoint(address, new Guid()); //address of the rasperry pi
            BluetoothClient client = new BluetoothClient(); //this computer
            client.Connect(myendpoint);
            while (client.Connected)
            {
                Console.WriteLine(client.GetStream());
            }
            
        }


    }
}

