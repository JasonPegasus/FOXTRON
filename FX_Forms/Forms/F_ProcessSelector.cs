using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FX_Core;

namespace FX_Forms.Forms
{
    public sealed partial class F_ProcessSelector : Form
    {
        public int ReturnPID { get; private set; }

        public F_ProcessSelector(string windowTitle)
        {
            InitializeComponent();
            this.Text = (windowTitle is null) ? "Process Selection" : windowTitle;
            FillProcessList();
            BT_Cancel.Click += (_, _) => CancelSelect();
            BT_Select.Click += (_, _) => FinishSelect();
            DG_ProcessList.CellDoubleClick += (_, _) => FinishSelect();
        }

        void FillProcessList()
        {
            DG_ProcessList.Rows.Clear();
            Process[] pList = ProcessManager.getUserProcesses();
            foreach (Process proc in pList) 
            {
                if (proc.HasExited) continue;

                string? filePath = null;
                try { filePath = proc.MainModule.FileName; } catch(Exception ex) { }
                DG_ProcessList.Rows.Add(filePath is not null ? Icon.ExtractAssociatedIcon(filePath) : null, proc.Id, proc.ProcessName, filePath);
            }
        }

        void FinishSelect()
        {
            if (DG_ProcessList.SelectedRows[0] is null) return;

            int pid = (int)DG_ProcessList.SelectedRows[0].Cells[1].Value;

            ReturnPID = pid;
            DialogResult = DialogResult.OK;
        }

        void CancelSelect() { DialogResult = DialogResult.Cancel; }
    }
}
