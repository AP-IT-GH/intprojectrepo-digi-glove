using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluetooth_test
{
    class BT
    {
        public string[] rows { get; set; }

        public void scan()
        {


            BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable;
            BluetoothClient client = new BluetoothClient();
            BluetoothDeviceInfo[] devices = client.DiscoverDevices();
            BluetoothClient bluetoothClient = new BluetoothClient();
            String authenticated;
            String classOfDevice;
            String connected;
            String deviceAddress;
            String deviceName;
            String installedServices;
            String lastSeen;
            String lastUsed;
            String remembered;
            String rssi;
            foreach (BluetoothDeviceInfo device in devices)
            {
                authenticated = device.Authenticated.ToString();
                classOfDevice = device.ClassOfDevice.ToString();
                connected = device.Connected.ToString();
                deviceAddress = device.DeviceAddress.ToString();
                deviceName = device.DeviceName.ToString();
                installedServices = device.InstalledServices.ToString();
                lastSeen = device.LastSeen.ToString();
                lastUsed = device.LastUsed.ToString();
                remembered = device.Remembered.ToString();
                rssi = device.Rssi.ToString();
                string[] row = new string[] { authenticated, classOfDevice, connected, deviceAddress, deviceName, installedServices, lastSeen, lastUsed, remembered, rssi };
                Console.WriteLine(row);
            }
        }

    }
}

