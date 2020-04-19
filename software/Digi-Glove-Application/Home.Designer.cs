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
            this.button_title_calibration = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_glove.SuspendLayout();
            this.panel_text.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(20, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(647, 362);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel_glove
            // 
            this.panel_glove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.panel_glove.Controls.Add(this.panel_text);
            this.panel_glove.Controls.Add(this.button_title_calibration);
            this.panel_glove.Location = new System.Drawing.Point(20, 20);
            this.panel_glove.Name = "panel_glove";
            this.panel_glove.Size = new System.Drawing.Size(687, 440);
            this.panel_glove.TabIndex = 3;
            // 
            // panel_text
            // 
            this.panel_text.Controls.Add(this.pictureBox1);
            this.panel_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_text.Location = new System.Drawing.Point(0, 36);
            this.panel_text.Name = "panel_text";
            this.panel_text.Padding = new System.Windows.Forms.Padding(20, 21, 20, 21);
            this.panel_text.Size = new System.Drawing.Size(687, 404);
            this.panel_text.TabIndex = 2;
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
            this.button_title_calibration.Text = "Glove Activity";
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
            this.Size = new System.Drawing.Size(727, 483);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_glove.ResumeLayout(false);
            this.panel_text.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel_text;
        private System.Windows.Forms.Button button_title_calibration;
        private System.Windows.Forms.Panel panel_glove;
    }
}
