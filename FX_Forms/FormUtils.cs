using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX_Core;
using FX_Forms.Forms;

namespace FX_Forms
{
    internal class FormUtils
    {
        internal static Process? OpenProcessSelectionDialog(string title = null)
        {
            F_ProcessSelector fSelect = new(title);
            Process? sProc = (fSelect.ShowDialog() == DialogResult.OK) ? ProcessManager.TryGetProcessByID(fSelect.ReturnPID) : null;
            fSelect.Dispose();
            return sProc;
        }
    }
}
