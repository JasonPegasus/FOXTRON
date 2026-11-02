using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using FX_Core;
using FX_Forms.Forms;
using FX_Forms.Styles;

namespace FX_Forms
{
    public partial class F_Main : Form
    {
        static readonly string WindowTitle = Program.PRODUCT_NAME;
        F_Logs fLogs;

        private readonly System.Windows.Forms.Timer updateValuesTimer = new() { Interval = 16 }; // ~60 fps

        public F_Main()
        {
            InitializeComponent();
            FormStyle.SetStyle<KatStyle>(this);
            updateValuesTimer.Start();
            Menu_Attach.Click += (_, _) => SendAttachTo();
            Menu_Logs.Click += (_, _) => SwitchLogs();
            Menu_Settings.Click += (_, _) => new F_Settings(this).ShowDialog();
            BT_Scan.Click += (_, _) => StartScan();
            BT_AddManualAddress.Click += (_, _) => AddVariableManually();
            updateValuesTimer.Tick += (_, _) => UpdateVariableValues();
            DG_Variables.CellContentClick += OnDGClicked;


            Engine.OnAttachChange += OnAttachChange;
            SetupLogs();
            SetupScansCombo();
            SetupPageSizesCombo();
            OnAttachChange(false);
            SwitchLogs();
        }

        void OnDGClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (Engine.AttachedProcess is null) return;

            if (((DataGridView)sender).Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex == 0)
            {
                FloatVariable fv = new FloatVariable(Shared.StringToPtr(DG_Variables.Rows[e.RowIndex].Cells["V_address"].Value.ToString()));
                FormUtils.OpenChangeValueDialog(ref fv, this);
                UpdateVariableValues();
            }
        }

        void UpdateVariableValues()
        {
            if (Engine.AttachedProcess is null) return;

            foreach (DataGridViewRow row in DG_Variables.Rows)
            {
                row.Cells["V_value"].Value = Engine.AttachedProcess.FOUND_VARIABLES[row.Cells["V_name"].Value.ToString()].Value;
            }
        }

        void UpdateVariableList()
        {
            DG_Variables.Rows.Clear();
            if (Engine.AttachedProcess is null) return;

            foreach (var vr in Engine.AttachedProcess.FOUND_VARIABLES)
            {
                DG_Variables.Rows.Add(Shared.PtrStr(vr.Value.Address), vr.Key, vr.Value.Value);
            }
        }

        void AddVariableManually()
        {
            try
            {
                Engine.AttachedProcess?.AddVariableManually(CM_ScanSelection.Text, new FloatVariable(FormUtils.OpenAskAddressDialog(CM_ScanSelection.Text, this)));
                UpdateVariableList();
            }
            catch (Exception ex)
            {
                Exception msg = Shared.FormatError("Error at AddVariableManually", ex);
                Shared.Print(msg);
                FormUtils.ShowMessage(msg.Message, Shared.PrintType.Exception);
            }
        }

        void StartScan()
        {
            try
            {
                SendAnalyzerSettings();
                string? scanName = CM_ScanSelection.SelectedItem?.ToString();
                FloatVariable? possibleFV = Engine.AttachedProcess?.ExecuteScanByName(scanName, (FloatVariable[] fvs) => { return FormUtils.OpenAddressSelectionDialog(fvs, this); });
                if (possibleFV is null) { Shared.Print($"No address was selected...", Shared.PrintType.Error); return; }
                FloatVariable fv = (FloatVariable)possibleFV;
                Shared.Print($"[Selected final: {fv}]", Shared.PrintType.Success);
                UpdateVariableList();
            }
            catch (Exception ex)
            {
                Exception msg = Shared.FormatError("Error at StartScan", ex);
                Shared.Print(msg);
                FormUtils.ShowMessage(msg.Message, Shared.PrintType.Exception);;
            }
        }

        void SendAnalyzerSettings()
        {
            if (Engine.AttachedProcess is null) { throw new NullReferenceException("There was no process attached, failed to get ProcessAnalyzer"); }
            Engine.AttachedProcess.settings.MemoryPageSize = memoryPageSizes[CM_MemoryPageSizes.SelectedIndex];
            Engine.AttachedProcess.settings.GameFPS = ((int)NUD_AverageFPS.Value);
            Engine.AttachedProcess.settings.PauseWhileScanning = CB_PauseScanning.Checked;
        }

        int[] memoryPageSizes = Enumerable.Range(0, 13).Select(i => (0x400 << i)).ToArray();
        void SetupPageSizesCombo()
        {
            foreach (int size in memoryPageSizes) { CM_MemoryPageSizes.Items.Add(size / 1024 + " KB"); }
            CM_MemoryPageSizes.SelectedIndex = 6;
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
            Process? proc = FormUtils.OpenProcessSelectionDialog(this);
            if (proc is null) return;
            Engine.AttachToProcess(proc);
        }

        Color? btColor;
        void OnAttachChange(bool attached)
        {
            BT_Scan.Enabled = attached;
            BT_AddManualAddress.Enabled = attached;
            if (btColor is null) { btColor = BT_Scan.BackColor; }
            //BT_Scan.Cursor = attached ? Cursors.Default : Cursors.No;
            BT_Scan.BackColor = attached ? (Color)btColor : Color.DarkGray;

            this.Text = WindowTitle + $" - ({(attached ? Engine.AttachedProcess.process.ProcessName + ".exe" : "Detached")})";
        }
    }
}
