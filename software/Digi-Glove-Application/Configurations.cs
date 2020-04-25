using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace Digi_Glove_Application
{
    public partial class Configurations : UserControl
    {
        int port=8000;
        int byteCount;
        NetworkStream stream;
        byte[] sendData;
        TcpClient client;

        public Configurations()
        {
            InitializeComponent();
        }

        private void comboBox_Macro_DropDown(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                ComboBox comboBox = sender as ComboBox;
                comboBox.DataSource = GetMacros();
            }
        }
        private List<string> GetMacros()
        {
            return new List<string>()
            {
                "Rightmouseclick",
                "Leftmouseclick",
                "Closepage",
                "Copy",
                "Paste",
                "PrintScreen",
                "Save",
                "Undo",
                "Refresh",
                "SelectAll",
                "Cut",
                "Bold",
                "PauseGlove"
            };
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Selected Value was changed");

            ComboBox comboBox = (ComboBox)sender;        }

        private void button_config_save_Click(object sender, EventArgs e)
        {
            string configurations = (string)comboBox_Thumb.SelectedItem + "-" + (string)comboBox_IndexFinger.SelectedItem + "-" + (string)comboBox_MiddleFinger.SelectedItem + "-" + (string)comboBox_RingFinger.SelectedItem + "-" + (string)comboBox_Pinky.SelectedItem;
            Debug.WriteLine(configurations);

            try
            {
                byteCount=Encoding.ASCII.GetByteCount(configurations);
                sendData=new byte[byteCount];
                sendData=Encoding.ASCII.GetBytes(configurations);
                stream=client.GetStream();
                stream.Write(sendData,0,sendData.Length);
                Debug.WriteLine(sendData);

            }
            catch (System.NullReferenceException)
            {
                Debug.WriteLine("No connection");
            }

        }

        private void button_config_connect_Click(object sender, EventArgs e)
        {
            try
            {
                client=new TcpClient("localhost", port);
                Debug.WriteLine("connection made");
                button_config_connect.Enabled = false;
            }
            catch(System.Net.Sockets.SocketException)
            {
                Debug.WriteLine("Connection failed");
                button_config_connect.Enabled = true;
            }
        }
    }
}
