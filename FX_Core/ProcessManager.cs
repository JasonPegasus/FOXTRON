using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swed64;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Linq;


namespace FX_Core
{
    public static class ProcessManager
    {
        public static Process[] getUserProcesses()
        { return Process.GetProcesses().Where(isUserProcess).ToArray(); }


        public static bool isUserProcess(Process process)
        { return !(process.SessionId == 0); }
        

        public static Swed proc;
        static IntPtr moduleBase;

        public static Process attachTo(Process selectedProc)
        {
            if (selectedProc == null) { return null; }
            try
            {
                proc = new Swed(selectedProc.ProcessName);
                moduleBase = proc.GetModuleBase(selectedProc.MainModule.ModuleName);
                return selectedProc;
            }
            catch (Exception e)
            {
                proc = null;
                moduleBase = IntPtr.Zero;
                throw new Exception("Error when attaching:\r\n" + e.Message);
            }
        }

        public static bool detach()
        {
            moduleBase = IntPtr.Zero;
            if (proc == null) { return false; }
            proc = null; return true;
        }

    }
}
