﻿namespace Digi_Glove_Application
{
    partial class Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Info));
            this.panel_info = new System.Windows.Forms.Panel();
            this.panel_text = new System.Windows.Forms.Panel();
            this.textBox_info = new System.Windows.Forms.TextBox();
            this.button_title_calibration = new System.Windows.Forms.Button();
            this.panel_info.SuspendLayout();
            this.panel_text.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_info
            // 
            this.panel_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.panel_info.Controls.Add(this.panel_text);
            this.panel_info.Controls.Add(this.button_title_calibration);
            this.panel_info.Location = new System.Drawing.Point(20, 20);
            this.panel_info.Name = "panel_info";
            this.panel_info.Size = new System.Drawing.Size(687, 470);
            this.panel_info.TabIndex = 2;
            // 
            // panel_text
            // 
            this.panel_text.Controls.Add(this.textBox_info);
            this.panel_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_text.Location = new System.Drawing.Point(0, 36);
            this.panel_text.Name = "panel_text";
            this.panel_text.Padding = new System.Windows.Forms.Padding(20, 21, 20, 21);
            this.panel_text.Size = new System.Drawing.Size(687, 434);
            this.panel_text.TabIndex = 2;
            // 
            // textBox_info
            // 
            this.textBox_info.AcceptsReturn = true;
            this.textBox_info.AcceptsTab = true;
            this.textBox_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.textBox_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_info.Enabled = false;
            this.textBox_info.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_info.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox_info.Location = new System.Drawing.Point(20, 21);
            this.textBox_info.Multiline = true;
            this.textBox_info.Name = "textBox_info";
            this.textBox_info.Size = new System.Drawing.Size(647, 392);
            this.textBox_info.TabIndex = 1;
            this.textBox_info.Text = resources.GetString("textBox_info.Text");
            // 
            // button_title_calibration
            // 
            this.button_title_calibration.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_title_calibration.FlatAppearance.BorderSize = 0;
            this.button_title_calibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_title_calibration.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.button_title_calibration.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_title_calibration.Image = ((System.Drawing.Image)(resources.GetObject("button_title_calibration.Image")));
            this.button_title_calibration.Location = new System.Drawing.Point(0, 0);
            this.button_title_calibration.Name = "button_title_calibration";
            this.button_title_calibration.Size = new System.Drawing.Size(687, 36);
            this.button_title_calibration.TabIndex = 0;
            this.button_title_calibration.Text = "Info";
            this.button_title_calibration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_title_calibration.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button_title_calibration.UseVisualStyleBackColor = true;
            // 
            // Info
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.panel_info);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Info";
            this.Size = new System.Drawing.Size(727, 513);
            this.panel_info.ResumeLayout(false);
            this.panel_text.ResumeLayout(false);
            this.panel_text.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_info;
        private System.Windows.Forms.Button button_title_calibration;
        private System.Windows.Forms.TextBox textBox_info;
        private System.Windows.Forms.Panel panel_text;
    }
}
