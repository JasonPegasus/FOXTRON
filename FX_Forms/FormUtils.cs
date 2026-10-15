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
        internal static Process? OpenProcessSelectionDialog(Form parent = null, string title = null)
        {
            F_ProcessSelector fSelect = new(title);
            fSelect.Location = (parent ?? fSelect).Location;
            Process? sProc = (fSelect.ShowDialog() == DialogResult.OK) ? ProcessManager.TryGetProcessByID(fSelect.ReturnPID) : null;
            fSelect.Dispose();
            return sProc;
        }
    }
}
