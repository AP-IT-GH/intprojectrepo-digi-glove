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
        IPAddress ip = null;
        TcpListener server = null;
        TcpClient client = null;
        string serverIP = "localhost";
        int port = 8087;

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
                "Bold"
            };
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Selected Value was changed");

            ComboBox comboBox = (ComboBox)sender;
            string configurations = (string)comboBox_Thumb.SelectedItem + (string)comboBox_IndexFinger.SelectedItem + (string)comboBox_MiddleFinger.SelectedItem + (string)comboBox_RingFinger.SelectedItem + (string)comboBox_Pinky.SelectedItem;

            TcpClient client = new TcpClient(serverIP, port);

            int byteCount = Encoding.ASCII.GetByteCount(configurations);

            byte[] sendData = new byte[byteCount];

            sendData = Encoding.ASCII.GetBytes(configurations);

            NetworkStream stream = client.GetStream();

            stream.Write(sendData, 0, sendData.Length);

            stream.Close();
            client.Close();
        }
    }
}
