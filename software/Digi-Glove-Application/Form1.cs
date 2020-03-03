using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            label_title.Text = "Home";
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
            label_title.Text = "Home";
        }

        private void button_configuration_Click(object sender, EventArgs e)
        {
            UpdateSelectedButton(sender as Button);
            configurations_usercontrol.BringToFront();
            label_title.Text = "Configuration";
        }

        private void button_info_Click(object sender, EventArgs e)
        {
            UpdateSelectedButton(sender as Button);
            info_usercontrol.BringToFront();
            label_title.Text = "Info";
        }
    }
}
