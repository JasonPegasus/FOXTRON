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
using FX_Forms.Styles;

namespace FX_Forms.Forms
{
    public partial class F_Logs : Form
    {
        Form parent;
        public F_Logs(Form parent)
        {
            InitializeComponent();
            FormStyle.SetStyle<KatStyle>(this);
            Shared.OnPrint += (string str, Shared.PrintType type) => PrintToConsole(str, type);
            this.parent = parent;
            this.parent.Move += (_, _) => UpdatePosition();
            this.parent.SizeChanged += (_, _) => UpdatePosition();
            this.Move += (_, _) => UpdatePosition();
            this.SizeChanged += (_, _) => UpdatePosition();
            this.CH_FollowMain.Click += (_, _) => UpdatePosition();
            UpdatePosition();
            this.FormClosing += OnClosing;
        }

        void OnClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        void UpdatePosition()
        {
            if (CH_FollowMain.Checked)
            {
                this.Location = new Point(parent.Location.X + parent.Size.Width, parent.Location.Y);
            }
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
