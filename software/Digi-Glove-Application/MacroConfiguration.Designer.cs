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
            this.components = new System.ComponentModel.Container();
            this.MacroTrigger = new System.Windows.Forms.ComboBox();
            this.panel_macroname = new System.Windows.Forms.Panel();
            this.MacroName = new System.Windows.Forms.TextBox();
            this.label_macroname = new System.Windows.Forms.Label();
            this.panel_macroexcecutable = new System.Windows.Forms.Panel();
            this.MacroExecutable = new System.Windows.Forms.TextBox();
            this.label_macroexcecutable = new System.Windows.Forms.Label();
            this.panel_macrotrigger = new System.Windows.Forms.Panel();
            this.label_macrotrigger = new System.Windows.Forms.Label();
            this.delete_butn = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel_macroname.SuspendLayout();
            this.panel_macroexcecutable.SuspendLayout();
            this.panel_macrotrigger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // MacroTrigger
            // 
            this.MacroTrigger.FormattingEnabled = true;
            this.MacroTrigger.Location = new System.Drawing.Point(6, 24);
            this.MacroTrigger.Name = "MacroTrigger";
            this.MacroTrigger.Size = new System.Drawing.Size(175, 24);
            this.MacroTrigger.TabIndex = 1;
            // 
            // panel_macroname
            // 
            this.panel_macroname.Controls.Add(this.MacroName);
            this.panel_macroname.Controls.Add(this.label_macroname);
            this.panel_macroname.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_macroname.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.panel_macroname.Location = new System.Drawing.Point(10, 0);
            this.panel_macroname.Name = "panel_macroname";
            this.panel_macroname.Size = new System.Drawing.Size(182, 54);
            this.panel_macroname.TabIndex = 3;
            // 
            // MacroName
            // 
            this.MacroName.Location = new System.Drawing.Point(7, 24);
            this.MacroName.Name = "MacroName";
            this.MacroName.Size = new System.Drawing.Size(153, 23);
            this.MacroName.TabIndex = 1;
            this.MacroName.Validating += new System.ComponentModel.CancelEventHandler(this.MacroName_Validating);
            // 
            // label_macroname
            // 
            this.label_macroname.AutoSize = true;
            this.label_macroname.Location = new System.Drawing.Point(3, 4);
            this.label_macroname.Name = "label_macroname";
            this.label_macroname.Size = new System.Drawing.Size(51, 19);
            this.label_macroname.TabIndex = 0;
            this.label_macroname.Text = "Name";
            // 
            // panel_macroexcecutable
            // 
            this.panel_macroexcecutable.Controls.Add(this.MacroExecutable);
            this.panel_macroexcecutable.Controls.Add(this.label_macroexcecutable);
            this.panel_macroexcecutable.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_macroexcecutable.Location = new System.Drawing.Point(395, 0);
            this.panel_macroexcecutable.Name = "panel_macroexcecutable";
            this.panel_macroexcecutable.Size = new System.Drawing.Size(207, 54);
            this.panel_macroexcecutable.TabIndex = 1;
            // 
            // MacroExecutable
            // 
            this.MacroExecutable.Location = new System.Drawing.Point(6, 25);
            this.MacroExecutable.Name = "MacroExecutable";
            this.MacroExecutable.Size = new System.Drawing.Size(175, 22);
            this.MacroExecutable.TabIndex = 3;
            this.MacroExecutable.Validating += new System.ComponentModel.CancelEventHandler(this.MacroExcecutable_Validating);
            // 
            // label_macroexcecutable
            // 
            this.label_macroexcecutable.AutoSize = true;
            this.label_macroexcecutable.Location = new System.Drawing.Point(6, 4);
            this.label_macroexcecutable.Name = "label_macroexcecutable";
            this.label_macroexcecutable.Size = new System.Drawing.Size(77, 17);
            this.label_macroexcecutable.TabIndex = 2;
            this.label_macroexcecutable.Text = "Executable";
            // 
            // panel_macrotrigger
            // 
            this.panel_macrotrigger.Controls.Add(this.label_macrotrigger);
            this.panel_macrotrigger.Controls.Add(this.MacroTrigger);
            this.panel_macrotrigger.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_macrotrigger.Location = new System.Drawing.Point(192, 0);
            this.panel_macrotrigger.Name = "panel_macrotrigger";
            this.panel_macrotrigger.Size = new System.Drawing.Size(203, 54);
            this.panel_macrotrigger.TabIndex = 2;
            // 
            // label_macrotrigger
            // 
            this.label_macrotrigger.AutoSize = true;
            this.label_macrotrigger.Location = new System.Drawing.Point(6, 4);
            this.label_macrotrigger.Name = "label_macrotrigger";
            this.label_macrotrigger.Size = new System.Drawing.Size(54, 17);
            this.label_macrotrigger.TabIndex = 1;
            this.label_macrotrigger.Text = "Trigger";
            // 
            // delete_butn
            // 
            this.delete_butn.Dock = System.Windows.Forms.DockStyle.Right;
            this.delete_butn.Location = new System.Drawing.Point(602, 0);
            this.delete_butn.Margin = new System.Windows.Forms.Padding(0);
            this.delete_butn.Name = "delete_butn";
            this.delete_butn.Size = new System.Drawing.Size(65, 54);
            this.delete_butn.TabIndex = 4;
            this.delete_butn.Text = "Delete";
            this.delete_butn.UseVisualStyleBackColor = true;
            this.delete_butn.Click += new System.EventHandler(this.Delete_Macro);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MacroConfiguration
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.panel_macroname);
            this.Controls.Add(this.panel_macrotrigger);
            this.Controls.Add(this.panel_macroexcecutable);
            this.Controls.Add(this.delete_butn);
            this.Name = "MacroConfiguration";
            this.Size = new System.Drawing.Size(667, 54);
            this.panel_macroname.ResumeLayout(false);
            this.panel_macroname.PerformLayout();
            this.panel_macroexcecutable.ResumeLayout(false);
            this.panel_macroexcecutable.PerformLayout();
            this.panel_macrotrigger.ResumeLayout(false);
            this.panel_macrotrigger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ComboBox MacroTrigger;
        private System.Windows.Forms.Panel panel_macroname;
        private System.Windows.Forms.Label label_macroname;
        private System.Windows.Forms.Panel panel_macroexcecutable;
        private System.Windows.Forms.Label label_macroexcecutable;
        private System.Windows.Forms.Panel panel_macrotrigger;
        private System.Windows.Forms.Label label_macrotrigger;
        public System.Windows.Forms.TextBox MacroName;
        private System.Windows.Forms.Button delete_butn;
        public System.Windows.Forms.TextBox MacroExecutable;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
