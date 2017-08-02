using System;
using System.Drawing;
using System.Windows.Forms;

namespace WiremockUI
{
    public class TabPageCustom : TabPage
    {
        public Func<bool> CanClose { get; set; }
    }
}
