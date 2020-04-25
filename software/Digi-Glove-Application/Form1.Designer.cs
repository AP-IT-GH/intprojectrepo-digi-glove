namespace Digi_Glove_Application
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel_menu = new System.Windows.Forms.Panel();
            this.panel_button_main = new System.Windows.Forms.Panel();
            this.button_info = new System.Windows.Forms.Button();
            this.button_configuration = new System.Windows.Forms.Button();
            this.button_home = new System.Windows.Forms.Button();
            this.button_main = new System.Windows.Forms.Button();
            this.panel_logo = new System.Windows.Forms.Panel();
            this.label_logo = new System.Windows.Forms.Label();
            this.panel_body = new System.Windows.Forms.Panel();
            this.timer_drop_panel = new System.Windows.Forms.Timer(this.components);
            this.configurations_usercontrol = new Digi_Glove_Application.Configurations();
            this.home_usercontrol = new Digi_Glove_Application.Home();
            this.info_usercontrol = new Digi_Glove_Application.Info();
            this.panel_menu.SuspendLayout();
            this.panel_button_main.SuspendLayout();
            this.panel_logo.SuspendLayout();
            this.panel_body.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_menu
            // 
            this.panel_menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.panel_menu.Controls.Add(this.panel_button_main);
            this.panel_menu.Controls.Add(this.panel_logo);
            this.panel_menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_menu.Location = new System.Drawing.Point(0, 0);
            this.panel_menu.Name = "panel_menu";
            this.panel_menu.Size = new System.Drawing.Size(156, 530);
            this.panel_menu.TabIndex = 0;
            // 
            // panel_button_main
            // 
            this.panel_button_main.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel_button_main.Controls.Add(this.button_info);
            this.panel_button_main.Controls.Add(this.button_configuration);
            this.panel_button_main.Controls.Add(this.button_home);
            this.panel_button_main.Controls.Add(this.button_main);
            this.panel_button_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_button_main.Location = new System.Drawing.Point(0, 47);
            this.panel_button_main.MaximumSize = new System.Drawing.Size(156, 160);
            this.panel_button_main.MinimumSize = new System.Drawing.Size(156, 40);
            this.panel_button_main.Name = "panel_button_main";
            this.panel_button_main.Size = new System.Drawing.Size(156, 160);
            this.panel_button_main.TabIndex = 1;
            // 
            // button_info
            // 
            this.button_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.button_info.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_info.FlatAppearance.BorderSize = 0;
            this.button_info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_info.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.button_info.ForeColor = System.Drawing.Color.White;
            this.button_info.Image = ((System.Drawing.Image)(resources.GetObject("button_info.Image")));
            this.button_info.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_info.Location = new System.Drawing.Point(0, 120);
            this.button_info.Name = "button_info";
            this.button_info.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.button_info.Size = new System.Drawing.Size(156, 40);
            this.button_info.TabIndex = 7;
            this.button_info.Text = "Info";
            this.button_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_info.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_info.UseVisualStyleBackColor = false;
            this.button_info.Click += new System.EventHandler(this.button_info_Click);
            // 
            // button_configuration
            // 
            this.button_configuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.button_configuration.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_configuration.FlatAppearance.BorderSize = 0;
            this.button_configuration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_configuration.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.button_configuration.ForeColor = System.Drawing.Color.White;
            this.button_configuration.Image = ((System.Drawing.Image)(resources.GetObject("button_configuration.Image")));
            this.button_configuration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_configuration.Location = new System.Drawing.Point(0, 80);
            this.button_configuration.Name = "button_configuration";
            this.button_configuration.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.button_configuration.Size = new System.Drawing.Size(156, 40);
            this.button_configuration.TabIndex = 6;
            this.button_configuration.Text = "Configuration";
            this.button_configuration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_configuration.UseVisualStyleBackColor = false;
            this.button_configuration.Click += new System.EventHandler(this.button_configuration_Click);
            // 
            // button_home
            // 
            this.button_home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.button_home.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_home.FlatAppearance.BorderSize = 0;
            this.button_home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_home.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_home.ForeColor = System.Drawing.Color.White;
            this.button_home.Image = ((System.Drawing.Image)(resources.GetObject("button_home.Image")));
            this.button_home.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_home.Location = new System.Drawing.Point(0, 40);
            this.button_home.Name = "button_home";
            this.button_home.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.button_home.Size = new System.Drawing.Size(156, 40);
            this.button_home.TabIndex = 5;
            this.button_home.Text = "Home";
            this.button_home.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_home.UseVisualStyleBackColor = false;
            this.button_home.Click += new System.EventHandler(this.button_home_Click);
            // 
            // button_main
            // 
            this.button_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(88)))), ((int)(((byte)(100)))));
            this.button_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_main.FlatAppearance.BorderSize = 0;
            this.button_main.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_main.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_main.ForeColor = System.Drawing.Color.Gainsboro;
            this.button_main.Location = new System.Drawing.Point(0, 0);
            this.button_main.Name = "button_main";
            this.button_main.Size = new System.Drawing.Size(156, 40);
            this.button_main.TabIndex = 4;
            this.button_main.Text = "Main";
            this.button_main.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_main.UseVisualStyleBackColor = false;
            this.button_main.Click += new System.EventHandler(this.button_main_Click);
            // 
            // panel_logo
            // 
            this.panel_logo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(74)))));
            this.panel_logo.Controls.Add(this.label_logo);
            this.panel_logo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_logo.Location = new System.Drawing.Point(0, 0);
            this.panel_logo.Name = "panel_logo";
            this.panel_logo.Size = new System.Drawing.Size(156, 47);
            this.panel_logo.TabIndex = 0;
            // 
            // label_logo
            // 
            this.label_logo.AutoSize = true;
            this.label_logo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_logo.ForeColor = System.Drawing.Color.White;
            this.label_logo.Location = new System.Drawing.Point(25, 14);
            this.label_logo.Name = "label_logo";
            this.label_logo.Size = new System.Drawing.Size(112, 23);
            this.label_logo.TabIndex = 0;
            this.label_logo.Text = "Digi-Glove";
            // 
            // panel_body
            // 
            this.panel_body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.panel_body.Controls.Add(this.configurations_usercontrol);
            this.panel_body.Controls.Add(this.home_usercontrol);
            this.panel_body.Controls.Add(this.info_usercontrol);
            this.panel_body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_body.Location = new System.Drawing.Point(156, 0);
            this.panel_body.Name = "panel_body";
            this.panel_body.Size = new System.Drawing.Size(727, 530);
            this.panel_body.TabIndex = 1;
            // 
            // timer_drop_panel
            // 
            this.timer_drop_panel.Interval = 1;
            this.timer_drop_panel.Tick += new System.EventHandler(this.timer_drop_panel_Tick);
            // 
            // configurations_usercontrol
            // 
            this.configurations_usercontrol.AutoScroll = true;
            this.configurations_usercontrol.AutoScrollMinSize = new System.Drawing.Size(20, 0);
            this.configurations_usercontrol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.configurations_usercontrol.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.configurations_usercontrol.Location = new System.Drawing.Point(0, 0);
            this.configurations_usercontrol.Margin = new System.Windows.Forms.Padding(0);
            this.configurations_usercontrol.Name = "configurations_usercontrol";
            this.configurations_usercontrol.Size = new System.Drawing.Size(727, 513);
            this.configurations_usercontrol.TabIndex = 4;
            // 
            // home_usercontrol
            // 
            this.home_usercontrol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.home_usercontrol.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.home_usercontrol.Location = new System.Drawing.Point(0, 0);
            this.home_usercontrol.Name = "home_usercontrol";
            this.home_usercontrol.Size = new System.Drawing.Size(727, 483);
            this.home_usercontrol.TabIndex = 2;
            // 
            // info_usercontrol
            // 
            this.info_usercontrol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(237)))), ((int)(((byte)(245)))));
            this.info_usercontrol.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_usercontrol.Location = new System.Drawing.Point(0, 0);
            this.info_usercontrol.Name = "info_usercontrol";
            this.info_usercontrol.Size = new System.Drawing.Size(727, 483);
            this.info_usercontrol.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(883, 530);
            this.Controls.Add(this.panel_body);
            this.Controls.Add(this.panel_menu);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel_menu.ResumeLayout(false);
            this.panel_button_main.ResumeLayout(false);
            this.panel_logo.ResumeLayout(false);
            this.panel_logo.PerformLayout();
            this.panel_body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_menu;
        private System.Windows.Forms.Panel panel_logo;
        private System.Windows.Forms.Label label_logo;
        private System.Windows.Forms.Panel panel_body;
        private System.Windows.Forms.Panel panel_button_main;
        private System.Windows.Forms.Button button_info;
        private System.Windows.Forms.Button button_configuration;
        private System.Windows.Forms.Button button_home;
        private System.Windows.Forms.Button button_main;
        private System.Windows.Forms.Timer timer_drop_panel;
        private Home home_usercontrol;
        private Info info_usercontrol;
        private Configurations configurations_usercontrol;
    }
}

