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
        public MacroConfiguration()
        {
            InitializeComponent();
            MacroTrigger.DataSource = LoadMacroTriggers();
        }
        private List<string> LoadMacroTriggers()
        {
            return new List<String>()
            {
                "Thumb Touch",
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
    }
}
