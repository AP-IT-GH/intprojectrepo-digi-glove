namespace Digi_Glove_Application
{
    partial class MacroConfiguration
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
            this.MacroExecutables = new System.Windows.Forms.TextBox();
            this.MacroTrigger = new System.Windows.Forms.ComboBox();
            this.MacroName = new System.Windows.Forms.TextBox();
            this.panel_macroname = new System.Windows.Forms.Panel();
            this.label_macroname = new System.Windows.Forms.Label();
            this.panel_macroexcecutable = new System.Windows.Forms.Panel();
            this.label_macroexcecutable = new System.Windows.Forms.Label();
            this.panel_macrotrigger = new System.Windows.Forms.Panel();
            this.label_macrotrigger = new System.Windows.Forms.Label();
            this.panel_macroname.SuspendLayout();
            this.panel_macroexcecutable.SuspendLayout();
            this.panel_macrotrigger.SuspendLayout();
            this.SuspendLayout();
            // 
            // MacroExecutables
            // 
            this.MacroExecutables.Location = new System.Drawing.Point(6, 26);
            this.MacroExecutables.Name = "MacroExecutables";
            this.MacroExecutables.Size = new System.Drawing.Size(239, 22);
            this.MacroExecutables.TabIndex = 0;
            // 
            // MacroTrigger
            // 
            this.MacroTrigger.FormattingEnabled = true;
            this.MacroTrigger.Location = new System.Drawing.Point(20, 24);
            this.MacroTrigger.Name = "MacroTrigger";
            this.MacroTrigger.Size = new System.Drawing.Size(121, 24);
            this.MacroTrigger.TabIndex = 1;
            // 
            // MacroName
            // 
            this.MacroName.Location = new System.Drawing.Point(16, 24);
            this.MacroName.Name = "MacroName";
            this.MacroName.Size = new System.Drawing.Size(151, 23);
            this.MacroName.TabIndex = 2;
            // 
            // panel_macroname
            // 
            this.panel_macroname.Controls.Add(this.label_macroname);
            this.panel_macroname.Controls.Add(this.MacroName);
            this.panel_macroname.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_macroname.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.panel_macroname.Location = new System.Drawing.Point(0, 0);
            this.panel_macroname.Name = "panel_macroname";
            this.panel_macroname.Size = new System.Drawing.Size(200, 54);
            this.panel_macroname.TabIndex = 3;
            // 
            // label_macroname
            // 
            this.label_macroname.AutoSize = true;
            this.label_macroname.Location = new System.Drawing.Point(13, 4);
            this.label_macroname.Name = "label_macroname";
            this.label_macroname.Size = new System.Drawing.Size(51, 19);
            this.label_macroname.TabIndex = 0;
            this.label_macroname.Text = "Name";
            // 
            // panel_macroexcecutable
            // 
            this.panel_macroexcecutable.Controls.Add(this.label_macroexcecutable);
            this.panel_macroexcecutable.Controls.Add(this.MacroExecutables);
            this.panel_macroexcecutable.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_macroexcecutable.Location = new System.Drawing.Point(400, 0);
            this.panel_macroexcecutable.Name = "panel_macroexcecutable";
            this.panel_macroexcecutable.Size = new System.Drawing.Size(287, 54);
            this.panel_macroexcecutable.TabIndex = 4;
            // 
            // label_macroexcecutable
            // 
            this.label_macroexcecutable.AutoSize = true;
            this.label_macroexcecutable.Location = new System.Drawing.Point(3, 4);
            this.label_macroexcecutable.Name = "label_macroexcecutable";
            this.label_macroexcecutable.Size = new System.Drawing.Size(84, 17);
            this.label_macroexcecutable.TabIndex = 2;
            this.label_macroexcecutable.Text = "Excecutable";
            // 
            // panel_macrotrigger
            // 
            this.panel_macrotrigger.Controls.Add(this.label_macrotrigger);
            this.panel_macrotrigger.Controls.Add(this.MacroTrigger);
            this.panel_macrotrigger.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_macrotrigger.Location = new System.Drawing.Point(200, 0);
            this.panel_macrotrigger.Name = "panel_macrotrigger";
            this.panel_macrotrigger.Size = new System.Drawing.Size(200, 54);
            this.panel_macrotrigger.TabIndex = 4;
            // 
            // label_macrotrigger
            // 
            this.label_macrotrigger.AutoSize = true;
            this.label_macrotrigger.Location = new System.Drawing.Point(17, 4);
            this.label_macrotrigger.Name = "label_macrotrigger";
            this.label_macrotrigger.Size = new System.Drawing.Size(54, 17);
            this.label_macrotrigger.TabIndex = 1;
            this.label_macrotrigger.Text = "Trigger";
            // 
            // MacroConfiguration
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.panel_macroexcecutable);
            this.Controls.Add(this.panel_macrotrigger);
            this.Controls.Add(this.panel_macroname);
            this.Name = "MacroConfiguration";
            this.Size = new System.Drawing.Size(687, 54);
            this.panel_macroname.ResumeLayout(false);
            this.panel_macroname.PerformLayout();
            this.panel_macroexcecutable.ResumeLayout(false);
            this.panel_macroexcecutable.PerformLayout();
            this.panel_macrotrigger.ResumeLayout(false);
            this.panel_macrotrigger.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox MacroExecutables;
        public System.Windows.Forms.ComboBox MacroTrigger;
        public System.Windows.Forms.TextBox MacroName;
        private System.Windows.Forms.Panel panel_macroname;
        private System.Windows.Forms.Label label_macroname;
        private System.Windows.Forms.Panel panel_macroexcecutable;
        private System.Windows.Forms.Label label_macroexcecutable;
        private System.Windows.Forms.Panel panel_macrotrigger;
        private System.Windows.Forms.Label label_macrotrigger;
    }
}
