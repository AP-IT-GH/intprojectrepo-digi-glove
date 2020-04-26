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
            else if (MacroExecutable.Text.Contains("%"))
            {
                e.Cancel = true;
                MacroExecutable.Focus();
                errorProvider.SetError(MacroExecutable, "Excecutable cannot contain %");
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

            if (MacroExecutable.Text.Contains("+"))
            {
                bool IsAllowed = true;
                string[] split = MacroExecutable.Text.Split('+');
                foreach (string splitstring in split)
                {
                    if (!string.IsNullOrWhiteSpace(splitstring))
                    {
                        IsAllowed = CheckIfIncluded(splitstring, GetAllowedCharacters());
                        if (IsAllowed)
                        {
                            //DoNothing
                        }
                        else
                        {
                            e.Cancel = true;
                            MacroExecutable.Focus();
                            errorProvider.SetError(MacroExecutable, "\"" + splitstring + "\" is not a valid excecutable");
                            break;
                        }

                    }
                }
                if (IsAllowed)
                {
                    e.Cancel = false;
                    errorProvider.SetError(MacroExecutable, string.Empty);
                }
            }
            
        }

        private void MacroName_Validating(object sender, CancelEventArgs e)
        {
            if (!Configurations.IsNameUnique(MacroName.Text, this))
            {
                e.Cancel = true;
                MacroName.Focus();
                errorProvider.SetError(MacroName, "Excecutable name cannot be the same as another macro");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(MacroName, string.Empty);
            }
        }
        private bool CheckIfIncluded(string s, List<string> slist)
        {
            bool IsIncluded = false;
            foreach (string str in slist)
            {
                if (s == str)
                {
                    IsIncluded = true;
                }
            }
            return IsIncluded;
        }
        private List<string> GetAllowedCharacters()
        {
            return new List<string>{"\t", "\n", "\r", " ", "!", "\"", "#", "$", "&", "\'", "(",
            ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7",
            "8", "9", ":", ";", "<", "=", ">", "?", "@", "[", "\\", "]", "^", "_", "`",
            "a", "b", "c", "d", "e","f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}",
            "accept", "add", "alt", "altleft", "altright", "apps", "backspace",
            "browserback", "browserfavorites", "browserforward", "browserhome",
            "browserrefresh", "browsersearch", "browserstop", "capslock", "clear",
            "convert", "ctrl", "ctrlleft", "ctrlright", "decimal", "del", "delete",
            "divide", "down", "end", "enter", "esc", "escape", "execute", "f1", "f10",
            "f11", "f12", "f13", "f14", "f15", "f16", "f17", "f18", "f19", "f2", "f20",
            "f21", "f22", "f23", "f24", "f3", "f4", "f5", "f6", "f7", "f8", "f9",
            "final", "fn", "hanguel", "hangul", "hanja", "help", "home", "insert", "junja",
            "kana", "kanji", "launchapp1", "launchapp2", "launchmail",
            "launchmediaselect", "left", "modechange", "multiply", "nexttrack",
            "nonconvert", "num0", "num1", "num2", "num3", "num4", "num5", "num6",
            "num7", "num8", "num9", "numlock", "pagedown", "pageup", "pause", "pgdn",
            "pgup", "playpause", "prevtrack", "print", "printscreen", "prntscrn",
            "prtsc", "prtscr", "return", "right", "scrolllock", "select", "separator",
            "shift", "shiftleft", "shiftright", "sleep", "space", "stop", "subtract", "tab",
            "up", "volumedown", "volumemute", "volumeup", "win", "winleft", "winright", "yen",
            "command", "option", "optionleft", "optionright"};

        }
    }
}
