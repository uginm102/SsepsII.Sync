using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSEPS_II.Synchronisation
{
    public class ControlsManager
    {
        public static CheckBox NewCheckBox(string name, string label)
        {
            return new CheckBox() { Name = name, Text = label };
        }

        public static RadioButton NewRadioButton(string name, string label,int top=10,int left=10)
        {
            return new RadioButton() { Name = name, Text = label, Top=top,Left=left,Width=350 };
        }
    }
}
