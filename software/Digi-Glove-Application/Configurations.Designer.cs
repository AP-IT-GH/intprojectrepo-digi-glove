using System.Collections.Generic;

namespace Digi_Glove_Application
{
    partial class Configurations
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configurations));
            this.panel_macro = new System.Windows.Forms.Panel();
            this.panel_buttoncontrol = new System.Windows.Forms.Panel();
            this.AddMacro = new System.Windows.Forms.Button();
            this.button_config_connect = new System.Windows.Forms.Button();
            this.button_config_save = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel_macro.SuspendLayout();
            this.panel_buttoncontrol.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_macro
            // 
            this.panel_macro.AutoScroll = true;
            this.panel_macro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.panel_macro.Controls.Add(this.panel_buttoncontrol);
            this.panel_macro.Controls.Add(this.button2);
            this.panel_macro.Location = new System.Drawing.Point(20, 20);
            this.panel_macro.Margin = new System.Windows.Forms.Padding(0);
            this.panel_macro.Name = "panel_macro";
            this.panel_macro.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panel_macro.Size = new System.Drawing.Size(687, 470);
            this.panel_macro.TabIndex = 2;
            // 
            // panel_buttoncontrol
            // 
            this.panel_buttoncontrol.Controls.Add(this.AddMacro);
            this.panel_buttoncontrol.Controls.Add(this.button_config_connect);
            this.panel_buttoncontrol.Controls.Add(this.button_config_save);
            this.panel_buttoncontrol.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_buttoncontrol.Location = new System.Drawing.Point(10, 52);
            this.panel_buttoncontrol.Name = "panel_buttoncontrol";
            this.panel_buttoncontrol.Size = new System.Drawing.Size(667, 26);
            this.panel_buttoncontrol.TabIndex = 7;
            // 
            // AddMacro
            // 
            this.AddMacro.Dock = System.Windows.Forms.DockStyle.Left;
            this.AddMacro.Location = new System.Drawing.Point(0, 0);
            this.AddMacro.Name = "AddMacro";
            this.AddMacro.Size = new System.Drawing.Size(122, 26);
            this.AddMacro.TabIndex = 6;
            this.AddMacro.Text = "Add Macro";
            this.AddMacro.UseVisualStyleBackColor = true;
            this.AddMacro.Click += new System.EventHandler(this.AddMacro_Click);
            // 
            // button_config_connect
            // 
            this.button_config_connect.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_config_connect.Location = new System.Drawing.Point(399, 0);
            this.button_config_connect.Name = "button_config_connect";
            this.button_config_connect.Size = new System.Drawing.Size(134, 26);
            this.button_config_connect.TabIndex = 5;
            this.button_config_connect.Text = "Connect";
            this.button_config_connect.UseVisualStyleBackColor = true;
            this.button_config_connect.Click += new System.EventHandler(this.button_config_connect_Click);
            // 
            // button_config_save
            // 
            this.button_config_save.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_config_save.Location = new System.Drawing.Point(533, 0);
            this.button_config_save.Margin = new System.Windows.Forms.Padding(0);
            this.button_config_save.Name = "button_config_save";
            this.button_config_save.Size = new System.Drawing.Size(134, 26);
            this.button_config_save.TabIndex = 4;
            this.button_config_save.Text = "Save";
            this.button_config_save.UseVisualStyleBackColor = true;
            this.button_config_save.Click += new System.EventHandler(this.button_config_save_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(10, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(667, 52);
            this.button2.TabIndex = 1;
            this.button2.Text = "Macro\'s";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Configurations
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(20, 0);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.panel_macro);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Configurations";
            this.Size = new System.Drawing.Size(727, 513);
            this.panel_macro.ResumeLayout(false);
            this.panel_buttoncontrol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_macro;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button_config_save;
        private System.Windows.Forms.Button button_config_connect;
        private System.Windows.Forms.Button AddMacro;
        public List<MacroConfiguration> Macros;
        private System.Windows.Forms.Panel panel_buttoncontrol;
    }
}
