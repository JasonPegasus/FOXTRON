using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using FX_Core;
using FX_Forms.Forms;
using FX_Forms.Styles;

namespace FX_Forms
{
    public partial class F_Main : Form
    {
        static readonly string WindowTitle = Program.PRODUCT_NAME;
        F_Logs fLogs;

        public F_Main()
        {
            InitializeComponent();
            FormStyle.SetStyle<KatStyle>(this);
            Menu_Attach.Click += (_, _) => SendAttachTo();
            Menu_Logs.Click += (_, _) => SwitchLogs();
            Menu_Settings.Click += (_, _) => new F_Settings(this).ShowDialog();
            BT_Scan.Click += (_, _) => StartScan();
            Engine.OnAttachChange += OnAttachChange;
            SetupLogs();
            SetupScansCombo();
            SetupChunkSizesCombo();
            OnAttachChange(false);
            SwitchLogs();
        }

        async void StartScan()
        {
            try
            {
                Engine.AttachedProcess.MemoryChunkSize = memoryChunkSizes[CM_MemoryChunkSizes.SelectedIndex];
                Shared.Print("size: " + memoryChunkSizes[CM_MemoryChunkSizes.SelectedIndex].ToString("x"), Shared.PrintType.Warn);
                Engine.AttachedProcess?.ExecuteScanByName(CM_ScanSelection.SelectedItem?.ToString() ?? "");
            }
            catch (Exception ex)
            {
                Exception msg = Shared.FormatError("Error at StartScan", ex);
                Shared.Print(msg);
                MessageBox.Show(msg.Message);
            }
        }

        int[] memoryChunkSizes = Enumerable.Range(0, 13).Select(i => (0x400 << i)).ToArray();
        void SetupChunkSizesCombo()
        {
            foreach (int size in memoryChunkSizes) { CM_MemoryChunkSizes.Items.Add(size / 1024 + " KB"); }
            CM_MemoryChunkSizes.SelectedIndex = 6;
        }

        //void UpdateProgressBar(int progress) { PB_ScanProgress.Value = progress; }

        void SetupScansCombo()
        {
            if (ScanType.ScansList.Keys.Count <= 0) return;
            foreach (string scan in ScanType.ScansList.Keys)
            { CM_ScanSelection.Items.Add(scan); }
            CM_ScanSelection.SelectedIndex = 0;
        }

        void SwitchLogs()
        {
            if (fLogs.Visible) { fLogs.Hide(); return; }
            fLogs.Show();
        }

        void SetupLogs()
        {
            fLogs = new(this);
            fLogs.Show();
            fLogs.Opacity = 0;
            fLogs.Hide();
            fLogs.Opacity = 1;
        }

        void SendAttachTo()
        {
            Process? proc = FormUtils.OpenProcessSelectionDialog();
            if (proc is null) return;
            Engine.AttachToProcess(proc);
        }

        Color? btColor;
        void OnAttachChange(bool attached)
        {
            BT_Scan.Enabled = attached;
            if (btColor is null) { btColor = BT_Scan.BackColor; }
            //BT_Scan.Cursor = attached ? Cursors.Default : Cursors.No;
            BT_Scan.BackColor = attached ? (Color)btColor : Color.DarkGray;

            this.Text = WindowTitle + $" - ({(attached ? Engine.AttachedProcess.process.ProcessName + ".exe" : "Detached")})";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
