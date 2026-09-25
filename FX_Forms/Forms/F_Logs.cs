using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FX_Core;

namespace FX_Forms.Forms
{
    public partial class F_Logs : Form
    {
        public F_Logs()
        {
            InitializeComponent();
            Shared.OnPrint += (string str, Shared.PrintType type) => PrintToConsole(str, type);
        }


        void PrintToConsole(string msg, Shared.PrintType pType, Font? font = null)
        { PrintToConsole(msg, Shared.PrintTypeColors[pType]); }
        void PrintToConsole(string msg, Color color, Font? font = null)
        {
            RichTextBox c = RTB_Console;
            c.SelectionStart = c.Text.Length;
            c.SelectionLength = 0;
            c.SelectionColor = color;
            if (font is not null) { c.SelectionFont = font; }
            c.AppendText(msg + "\n");
            if (CH_AutoScroll.Checked) { c.ScrollToCaret(); }
        }
    }
}
