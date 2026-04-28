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
using System.Runtime.InteropServices;


namespace FX_Core
{
    public static class ProcessManager
    {
        [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("ntdll.dll")] static extern int NtSuspendProcess(IntPtr processHandle);
        [DllImport("ntdll.dll")] static extern int NtResumeProcess(IntPtr processHandle);

        public static void SetPauseProcess(Process process, bool Pause)
        {
            if (Pause) 
            { NtSuspendProcess(process.Handle); return; }
            NtResumeProcess(process.Handle);
        }

        public static void SetFoxtronHighPriority(bool high)
        { Process.GetCurrentProcess().PriorityClass = (high ? ProcessPriorityClass.High : ProcessPriorityClass.Normal); }

        public static Process[] getUserProcesses()
        { return Process.GetProcesses().Where(isUserProcess).ToArray(); }

        public static bool isUserProcess(Process process)
        { return !(process.SessionId == 0); }

    }
}
