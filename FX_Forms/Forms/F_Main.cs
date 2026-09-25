using System;
using System.Diagnostics;
using FX_Core;
using FX_Forms.Forms;

namespace FX_Forms
{
    public partial class F_Main : Form
    {
        static readonly string WindowTitle = Program.PRODUCT_NAME;
        F_Logs fLogs = new();

        public F_Main()
        {
            InitializeComponent();
            this.Text = WindowTitle + "- (Detached)";
            Menu_Attach.Click += (_, _) => SendAttachTo();
            Menu_Logs.Click += (_, _) => SwitchLogs();
            Engine.OnAttachChange += OnAttachChange;
            SetupLogs();
        }

        void SwitchLogs()
        {
            if (fLogs.Visible) { fLogs.Hide(); return; }
            fLogs.Show();
        }

        void SetupLogs()
        {
            fLogs.Show();
            fLogs.Opacity = 0;
            fLogs.Hide();
            fLogs.Opacity = 1;
        }

        void SendAttachTo()
        {
            using (Process? proc = FormUtils.OpenProcessSelectionDialog())
            {
                if (proc is null) return;
                Engine.AttachToProcess(proc);
            }
        }

        void OnAttachChange(bool attached)
        {
            this.Text = WindowTitle + $" - ({(attached ? Engine.AttachedProcess.process.ProcessName + ".exe" : "Detached")})";
        }

    }
}
