using Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digi_Glove_Application
{
    public partial class Form1 : Form
    {
        Button SelectedButton;
        Panel DropDownPanel;
        bool IsCollapsed;

        public Form1()
        {
            InitializeComponent();
            IsCollapsed = false;
            home_usercontrol.BringToFront();
            this.Text = "Home";
            ExcecuteCommand("RunBackEnd");
        }
        private void UpdateSelectedButton(Button button)
        {
            if (SelectedButton != null)
            {
                SelectedButton.BackColor = Color.FromArgb(74, 88, 100);
            }

            button.BackColor = Color.FromArgb(106, 209, 223);
            SelectedButton = button;
        }
        private void UpdateSelectedButton()
        {
            if (SelectedButton != null)
            {
                SelectedButton.BackColor = Color.FromArgb(74, 88,100);
            }
        }
        private void ActivateDropPanel(Panel panel)
        {
            if (!timer_drop_panel.Enabled)
            {
                DropDownPanel = panel;
                timer_drop_panel.Start();
            }
        }

        private void button_main_Click(object sender, EventArgs e)
        {
            ActivateDropPanel(panel_button_main);
            UpdateSelectedButton();
        }

        private void timer_drop_panel_Tick(object sender, EventArgs e)
        {
            if (IsCollapsed)
            {
                DropDownPanel.Height += 20;
                if (DropDownPanel.Size.Height >= DropDownPanel.MaximumSize.Height)
                {
                    DropDownPanel.Height = DropDownPanel.MaximumSize.Height;
                    timer_drop_panel.Stop();
                    IsCollapsed = false;
                }
            }
            else
            {
                DropDownPanel.Height -=20;
                if (DropDownPanel.Size.Height <= DropDownPanel.MinimumSize.Height)
                {
                    DropDownPanel.Height = DropDownPanel.MinimumSize.Height;
                    timer_drop_panel.Stop();
                    IsCollapsed = true;
                }
            }
        }

        private void button_home_Click(object sender, EventArgs e)
        {
            UpdateSelectedButton(sender as Button);
            home_usercontrol.BringToFront();
            this.Text = "Home";
        }

        private void button_configuration_Click(object sender, EventArgs e)
        {
            UpdateSelectedButton(sender as Button);
            configurations_usercontrol.BringToFront();
            this.Text = "Configuration";
        }

        private void button_info_Click(object sender, EventArgs e)
        {
            UpdateSelectedButton(sender as Button);
            info_usercontrol.BringToFront();
            this.Text = "Info";
        }

        private void buttonclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_maximize_Click(object sender, EventArgs e)
        {
            MaximizeBox = true;
        }

        private void button_minimize_Click(object sender, EventArgs e)
        {
            MinimizeBox = true;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //Determines whether the cursor is in the taskbar
            bool cursorNotInBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if (this.WindowState == FormWindowState.Minimized && cursorNotInBar)
            {
                this.ShowInTaskbar = false;

                this.Hide();
            }
        }

        private void ExcecuteCommand(string command)
        {
            string directoryPath = Directory.GetCurrentDirectory().Replace(@"bin\Debug","");
            this.Text = directoryPath;
            try
            {
                var processInfo = new ProcessStartInfo("cmd.exe", "/c " + directoryPath + "\\" + command);
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;

                var process = Process.Start(processInfo);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            configurations_usercontrol.SendData("exit");
            base.OnFormClosing(e);
        }
    }
}
