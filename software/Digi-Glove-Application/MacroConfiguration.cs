using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digi_Glove_Application
{
    public partial class MacroConfiguration : UserControl
    {
        public Configurations Configurations { get; set; }
        //public MacroConfiguration()
        //{
        //    InitializeComponent();
        //    MacroTrigger.DataSource = LoadMacroTriggers();
        //    //MacroExecutable.DataSource = GetMacros();
        //}
        public MacroConfiguration(Configurations conf)
        {
            InitializeComponent();
            MacroTrigger.DataSource = LoadMacroTriggers();
            //MacroExecutable.DataSource = GetMacros();
            Configurations = conf;
        }
        private List<string> LoadMacroTriggers()
        {
            return new List<String>()
            {
                "Thumb Bend",
                "Index Finger Touch",
                "Index Finger Bend",
                "Middle Finger Touch",
                "Middle Finger Bend",
                "Ring Finger Touch",
                "Ring Finger Bend",
                "Pinky Touch",
                "Pinky Bend"
            };
        }
        
        private List<string> GetMacros()
        {
            return new List<string>()
            {
                "Rightmouseclick",
                "Leftmouseclick",
                "Closepage",
                "Copy",
                "Paste",
                "PrintScreen",
                "Save",
                "Undo",
                "Refresh",
                "SelectAll",
                "Cut",
                "Bold",
                "PauseGlove"
            };
        }

        private void Delete_Macro(object sender, EventArgs e)
        {
            Configurations.DeleteMacro(this);
        }

        private void MacroExcecutable_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MacroExecutable.Text))
            {
                e.Cancel = true;
                MacroExecutable.Focus();
                errorProvider.SetError(MacroExecutable, "Please enter a Excecutable");
            }
            else if (MacroExecutable.Text.Contains("§"))
            {
                e.Cancel = true;
                MacroExecutable.Focus();
                errorProvider.SetError(MacroExecutable, "Excecutable cannot contain §");
            }
            else if (MacroExecutable.Text.Contains("~"))
            {
                e.Cancel = true;
                MacroExecutable.Focus();
                errorProvider.SetError(MacroExecutable, "Excecutable cannot contain ~");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(MacroExecutable, string.Empty);
            }
        }
    }
}
