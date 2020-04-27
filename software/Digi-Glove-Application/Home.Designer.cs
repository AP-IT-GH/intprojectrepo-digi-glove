namespace Digi_Glove_Application
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_glove = new System.Windows.Forms.Panel();
            this.panel_text = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox_info = new System.Windows.Forms.TextBox();
            this.button_title_calibration = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_glove.SuspendLayout();
            this.panel_text.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(20, 175);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(407, 241);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel_glove
            // 
            this.panel_glove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.panel_glove.Controls.Add(this.panel_text);
            this.panel_glove.Controls.Add(this.button_title_calibration);
            this.panel_glove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_glove.Location = new System.Drawing.Point(20, 20);
            this.panel_glove.Name = "panel_glove";
            this.panel_glove.Size = new System.Drawing.Size(687, 473);
            this.panel_glove.TabIndex = 3;
            // 
            // panel_text
            // 
            this.panel_text.Controls.Add(this.textBox1);
            this.panel_text.Controls.Add(this.textBox2);
            this.panel_text.Controls.Add(this.pictureBox1);
            this.panel_text.Controls.Add(this.textBox_info);
            this.panel_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_text.Location = new System.Drawing.Point(0, 36);
            this.panel_text.Name = "panel_text";
            this.panel_text.Padding = new System.Windows.Forms.Padding(20, 21, 20, 21);
            this.panel_text.Size = new System.Drawing.Size(687, 437);
            this.panel_text.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(427, 205);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(240, 211);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // textBox2
            // 
            this.textBox2.AcceptsReturn = true;
            this.textBox2.AcceptsTab = true;
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox2.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(427, 175);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(240, 30);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "Digi-Glove was created by:";
            // 
            // textBox_info
            // 
            this.textBox_info.AcceptsReturn = true;
            this.textBox_info.AcceptsTab = true;
            this.textBox_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.textBox_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_info.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_info.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_info.ForeColor = System.Drawing.Color.Black;
            this.textBox_info.Location = new System.Drawing.Point(20, 21);
            this.textBox_info.Multiline = true;
            this.textBox_info.Name = "textBox_info";
            this.textBox_info.ReadOnly = true;
            this.textBox_info.Size = new System.Drawing.Size(647, 154);
            this.textBox_info.TabIndex = 2;
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
            this.button_title_calibration.Text = "Introducing The Digi-Glove";
            this.button_title_calibration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_title_calibration.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button_title_calibration.UseVisualStyleBackColor = true;
            this.button_title_calibration.Click += new System.EventHandler(this.button_title_calibration_Click);
            // 
            // Home
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.panel_glove);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Home";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(727, 513);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_glove.ResumeLayout(false);
            this.panel_text.ResumeLayout(false);
            this.panel_text.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel_text;
        private System.Windows.Forms.Button button_title_calibration;
        private System.Windows.Forms.Panel panel_glove;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox_info;
    }
}
