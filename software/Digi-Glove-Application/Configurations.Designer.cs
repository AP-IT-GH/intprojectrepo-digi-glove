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
            this.panel8 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox_Pinky = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.comboBox_RingFinger = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.comboBox_MiddleFinger = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.comboBox_IndexFinger = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_Thumb = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel_buttoncontrol = new System.Windows.Forms.Panel();
            this.AddMacro = new System.Windows.Forms.Button();
            this.button_config_connect = new System.Windows.Forms.Button();
            this.button_config_save = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel_macro.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_buttoncontrol.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_macro
            // 
            this.panel_macro.AutoScroll = true;
            this.panel_macro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.panel_macro.Controls.Add(this.panel8);
            this.panel_macro.Controls.Add(this.panel7);
            this.panel_macro.Controls.Add(this.panel5);
            this.panel_macro.Controls.Add(this.panel2);
            this.panel_macro.Controls.Add(this.panel1);
            this.panel_macro.Controls.Add(this.panel_buttoncontrol);
            this.panel_macro.Controls.Add(this.button2);
            this.panel_macro.Location = new System.Drawing.Point(20, 20);
            this.panel_macro.Margin = new System.Windows.Forms.Padding(0);
            this.panel_macro.Name = "panel_macro";
            this.panel_macro.Size = new System.Drawing.Size(687, 470);
            this.panel_macro.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.textBox3);
            this.panel8.Controls.Add(this.comboBox_Pinky);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 212);
            this.panel8.Margin = new System.Windows.Forms.Padding(4);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel8.Size = new System.Drawing.Size(687, 33);
            this.panel8.TabIndex = 5;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(19, 5);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(57, 17);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = "Pinky";
            // 
            // comboBox_Pinky
            // 
            this.comboBox_Pinky.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.comboBox_Pinky.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox_Pinky.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Pinky.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.comboBox_Pinky.FormattingEnabled = true;
            this.comboBox_Pinky.Location = new System.Drawing.Point(524, 2);
            this.comboBox_Pinky.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_Pinky.Name = "comboBox_Pinky";
            this.comboBox_Pinky.Size = new System.Drawing.Size(160, 27);
            this.comboBox_Pinky.TabIndex = 1;
            this.comboBox_Pinky.DropDown += new System.EventHandler(this.comboBox_Macro_DropDown);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.comboBox_RingFinger);
            this.panel7.Controls.Add(this.textBox2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 182);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel7.Size = new System.Drawing.Size(687, 30);
            this.panel7.TabIndex = 4;
            // 
            // comboBox_RingFinger
            // 
            this.comboBox_RingFinger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.comboBox_RingFinger.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox_RingFinger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_RingFinger.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.comboBox_RingFinger.FormattingEnabled = true;
            this.comboBox_RingFinger.Location = new System.Drawing.Point(524, 2);
            this.comboBox_RingFinger.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_RingFinger.Name = "comboBox_RingFinger";
            this.comboBox_RingFinger.Size = new System.Drawing.Size(160, 27);
            this.comboBox_RingFinger.TabIndex = 1;
            this.comboBox_RingFinger.DropDown += new System.EventHandler(this.comboBox_Macro_DropDown);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(19, 5);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(80, 17);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Ring Finger";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBox4);
            this.panel5.Controls.Add(this.comboBox_MiddleFinger);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 152);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Size = new System.Drawing.Size(687, 30);
            this.panel5.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(19, 5);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(103, 17);
            this.textBox4.TabIndex = 3;
            this.textBox4.Text = "Middle Finger";
            // 
            // comboBox_MiddleFinger
            // 
            this.comboBox_MiddleFinger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.comboBox_MiddleFinger.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox_MiddleFinger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_MiddleFinger.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.comboBox_MiddleFinger.FormattingEnabled = true;
            this.comboBox_MiddleFinger.Location = new System.Drawing.Point(524, 2);
            this.comboBox_MiddleFinger.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_MiddleFinger.Name = "comboBox_MiddleFinger";
            this.comboBox_MiddleFinger.Size = new System.Drawing.Size(160, 27);
            this.comboBox_MiddleFinger.TabIndex = 1;
            this.comboBox_MiddleFinger.DropDown += new System.EventHandler(this.comboBox_Macro_DropDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.comboBox_IndexFinger);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 119);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Size = new System.Drawing.Size(687, 33);
            this.panel2.TabIndex = 3;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(19, 5);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(91, 17);
            this.textBox5.TabIndex = 4;
            this.textBox5.Text = "Index Finger";
            // 
            // comboBox_IndexFinger
            // 
            this.comboBox_IndexFinger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.comboBox_IndexFinger.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox_IndexFinger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_IndexFinger.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.comboBox_IndexFinger.FormattingEnabled = true;
            this.comboBox_IndexFinger.Location = new System.Drawing.Point(524, 2);
            this.comboBox_IndexFinger.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_IndexFinger.Name = "comboBox_IndexFinger";
            this.comboBox_IndexFinger.Size = new System.Drawing.Size(160, 27);
            this.comboBox_IndexFinger.TabIndex = 1;
            this.comboBox_IndexFinger.DropDown += new System.EventHandler(this.comboBox_Macro_DropDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_Thumb);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Size = new System.Drawing.Size(687, 33);
            this.panel1.TabIndex = 2;
            // 
            // comboBox_Thumb
            // 
            this.comboBox_Thumb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.comboBox_Thumb.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox_Thumb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Thumb.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.comboBox_Thumb.FormattingEnabled = true;
            this.comboBox_Thumb.Location = new System.Drawing.Point(524, 2);
            this.comboBox_Thumb.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_Thumb.Name = "comboBox_Thumb";
            this.comboBox_Thumb.Size = new System.Drawing.Size(160, 27);
            this.comboBox_Thumb.TabIndex = 1;
            this.comboBox_Thumb.DropDown += new System.EventHandler(this.comboBox_Macro_DropDown);
            this.comboBox_Thumb.SelectedValueChanged += new System.EventHandler(this.comboBox_SelectedValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(19, 4);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(57, 17);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Thumb";
            // 
            // panel_buttoncontrol
            // 
            this.panel_buttoncontrol.Controls.Add(this.AddMacro);
            this.panel_buttoncontrol.Controls.Add(this.button_config_connect);
            this.panel_buttoncontrol.Controls.Add(this.button_config_save);
            this.panel_buttoncontrol.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_buttoncontrol.Location = new System.Drawing.Point(0, 52);
            this.panel_buttoncontrol.Name = "panel_buttoncontrol";
            this.panel_buttoncontrol.Size = new System.Drawing.Size(687, 34);
            this.panel_buttoncontrol.TabIndex = 7;
            // 
            // AddMacro
            // 
            this.AddMacro.Location = new System.Drawing.Point(19, 3);
            this.AddMacro.Name = "AddMacro";
            this.AddMacro.Size = new System.Drawing.Size(122, 24);
            this.AddMacro.TabIndex = 6;
            this.AddMacro.Text = "Add Macro";
            this.AddMacro.UseVisualStyleBackColor = true;
            this.AddMacro.Click += new System.EventHandler(this.AddMacro_Click);
            // 
            // button_config_connect
            // 
            this.button_config_connect.Location = new System.Drawing.Point(550, 3);
            this.button_config_connect.Name = "button_config_connect";
            this.button_config_connect.Size = new System.Drawing.Size(134, 24);
            this.button_config_connect.TabIndex = 5;
            this.button_config_connect.Text = "Connect";
            this.button_config_connect.UseVisualStyleBackColor = true;
            this.button_config_connect.Click += new System.EventHandler(this.button_config_connect_Click);
            // 
            // button_config_save
            // 
            this.button_config_save.Location = new System.Drawing.Point(413, 3);
            this.button_config_save.Margin = new System.Windows.Forms.Padding(0);
            this.button_config_save.Name = "button_config_save";
            this.button_config_save.Size = new System.Drawing.Size(134, 24);
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
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(687, 52);
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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.panel_macro);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Configurations";
            this.Size = new System.Drawing.Size(727, 513);
            this.panel_macro.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_buttoncontrol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_macro;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_Thumb;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox_Pinky;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox comboBox_RingFinger;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ComboBox comboBox_MiddleFinger;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox comboBox_IndexFinger;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button_config_save;
        private System.Windows.Forms.Button button_config_connect;
        private System.Windows.Forms.Button AddMacro;
        public List<MacroConfiguration> Macros;
        private System.Windows.Forms.Panel panel_buttoncontrol;
    }
}
