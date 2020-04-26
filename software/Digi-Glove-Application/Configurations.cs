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
using System.IO;
using Newtonsoft.Json;

namespace Digi_Glove_Application
{
    public partial class Configurations : UserControl
    {
        int port=8000;
        int byteCount;
        NetworkStream stream;
        byte[] sendData;
        TcpClient client;

        string savePath = Application.UserAppDataPath + "/macro_configurations.txt";
        string macro_json;
        List<Macro> macros;
        List<MacroConfiguration> macroConfigurations;
        
        public Configurations()
        {
            macros = new List<Macro>();
            macroConfigurations = new List<MacroConfiguration>();
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

            if (File.Exists(savePath))
            {
                macro_json = File.ReadAllText(savePath);
                macros = JsonConvert.DeserializeObject<List<Macro>>(macro_json);
            }

            InitializeComponent();
            
            if (macros != null)
            {
                foreach (Macro macro in macros)
                {
                    MacroConfiguration macroconfig = new MacroConfiguration(this);
                    macroconfig.MacroName.Text = macro.Name;
                    macroconfig.MacroExecutable.Text = macro.Excecutable;
                    macroconfig.MacroTrigger.SelectedItem = macro.Trigger;
                    macroConfigurations.Add(macroconfig);
                    panel_macro.Controls.Add(macroconfig);
                    macroconfig.BringToFront();
                    macroconfig.Dock = DockStyle.Top;
                }
            }
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

            ComboBox comboBox = (ComboBox)sender;
        }

        private void button_config_save_Click(object sender, EventArgs e)
        {
            macros = new List<Macro>();
            foreach (MacroConfiguration macroconfig in macroConfigurations)
            {
                if (!string.IsNullOrEmpty(macroconfig.MacroExecutable.Text) && !string.IsNullOrEmpty(macroconfig.MacroName.Text) && !macroconfig.MacroExecutable.Text.Contains("~") && !macroconfig.MacroExecutable.Text.Contains("%"))
                {
                    macros.Add(new Macro()
                    {
                        Name = macroconfig.MacroName.Text.ToLower(),
                        Trigger = macroconfig.MacroTrigger.SelectedItem.ToString(),
                        Excecutable = macroconfig.MacroExecutable.Text.ToLower()
                    });
                }
            }

            macro_json = JsonConvert.SerializeObject(macros, Formatting.Indented);
            File.WriteAllText(savePath, macro_json);

            Debug.WriteLine("Saved in " + savePath);

            if (client != null)
            {
                string configurations = "";
                foreach (Macro macro in macros)
                {
                    configurations += macro.Name + "~" + macro.Excecutable+ "~" + ConvertMacroTriggers()[macro.Trigger] + "%";
                }
                //string configurations = (string)comboBox_Thumb.SelectedItem + "-" + (string)comboBox_IndexFinger.SelectedItem + "-" + (string)comboBox_MiddleFinger.SelectedItem + "-" + (string)comboBox_RingFinger.SelectedItem + "-" + (string)comboBox_Pinky.SelectedItem;
                Debug.WriteLine(configurations);

                try
                {
                    byteCount=Encoding.ASCII.GetByteCount(configurations);
                    sendData=new byte[byteCount];
                    sendData=Encoding.ASCII.GetBytes(configurations);
                    stream=client.GetStream();
                    try
                    {
                        stream.Write(sendData, 0, sendData.Length);
                        Debug.WriteLine(sendData);
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Connection failed");
                        button_config_connect.Enabled = true;
                        button_config_connect.Text = "Connect";
                        client = null;
                        MessageBox.Show( "Connection failed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (System.NullReferenceException)
                {
                    Debug.WriteLine("No connection");
                }
            }

        }

        private void button_config_connect_Click(object sender, EventArgs e)
        {
            try
            {
                client=new TcpClient("localhost", port);
                Debug.WriteLine("connection made");
                button_config_connect.Enabled = false;
                button_config_connect.Text = "Connected";
            }
            catch (System.Net.Sockets.SocketException)
            {
                Debug.WriteLine("Connection failed");
                button_config_connect.Enabled = true;
                MessageBox.Show("Failed to make connection.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        } 
        

        private void AddMacro_Click(object sender, EventArgs e)
        {
            MacroConfiguration macro = new MacroConfiguration(this);
            panel_macro.Controls.Add(macro);
            macro.BringToFront();
            macro.Dock = DockStyle.Top;
            macroConfigurations.Add(macro);
        }
        

        private Dictionary<string, string> ConvertMacroTriggers()
        {
            return new Dictionary<string, string>()
            {
                { "Thumb Bend", "flexthumb"},
                { "Index Finger Touch", "touchindex"},
                { "Index Finger Bend", "flexindex"},
                { "Middle Finger Touch", "touchmiddle"},
                { "Middle Finger Bend", "flexmiddle"},
                { "Ring Finger Touch", "touchring"},
                { "Ring Finger Bend", "flexring"},
                { "Pinky Touch", "touchpink"},
                { "Pinky Bend", "flexpink"}
            };
        }
        public void DeleteMacro(MacroConfiguration m)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this macro?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                macroConfigurations.Remove(m);
                panel_macro.Controls.Remove(m);

            }
        }

        public bool IsNameUnique(string name, MacroConfiguration macroconf)
        {
            foreach (MacroConfiguration mconf in macroConfigurations)
            {
                if (mconf.MacroName.Text == name && macroconf != mconf)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
