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
            //Configuration of the server

            //ip = Dns.GetHostEntry(serverIP).AddressList[0];
            //server = new TcpListener(ip, port);
            //client = default(TcpClient);

            //try
            //{
            //    server.Start();
            //    Debug.WriteLine("Server started...");
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e.ToString());
            //}

            //backgroundWorker1.RunWorkerAsync();

            InitializeComponent();
        }

        //Server Background task

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    //BackgroundWorker worker = sender as BackgroundWorker;

        //    //for (int i = 1; i <= 10; i++)
        //    //{
        //    //    if (worker.CancellationPending == true)
        //    //    {
        //    //        e.Cancel = true;
        //    //        break;
        //    //    }
        //    //    else
        //    //    {
        //    //        // Perform a time consuming operation and report progress.
        //    //        System.Threading.Thread.Sleep(500);
        //    //        worker.ReportProgress(i * 10);
        //    //    }
        //    //}
        //    while (true)
        //    {
        //        client = server.AcceptTcpClient();

        //        byte[] receivedBuffer = new byte[100];
        //        NetworkStream stream = client.GetStream();

        //        stream.Read(receivedBuffer, 0, receivedBuffer.Length);

        //        //string msg = Encoding.ASCII.GetString(receivedBuffer, 0, receivedBuffer.Length);
        //        StringBuilder msg = new StringBuilder();

        //        foreach (byte b in receivedBuffer)
        //        {
        //            if (b.Equals(00))
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                msg.Append(Convert.ToChar(b).ToString());
        //            }
        //        }

        //        Debug.WriteLine(msg.ToString() + msg.Length);

        //    }
        //}


        //async void InfiniteLoop()
        //{
        //    while (true)
        //    {
        //        await Task.Delay(100);

        //        client = server.AcceptTcpClient();

        //        byte[] receivedBuffer = new byte[100];
        //        NetworkStream stream = client.GetStream();

        //        stream.Read(receivedBuffer, 0, receivedBuffer.Length);

        //        //string msg = Encoding.ASCII.GetString(receivedBuffer, 0, receivedBuffer.Length);
        //        StringBuilder msg = new StringBuilder();

        //        foreach (byte b in receivedBuffer)
        //        {
        //            if (b.Equals(00))
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                msg.Append(Convert.ToChar(b).ToString());
        //            }
        //        }

        //        Debug.WriteLine(msg.ToString() + msg.Length);

        //    }
        //}

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
                client=new TcpClient("LAPTOP-UVPKI186",port);
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
