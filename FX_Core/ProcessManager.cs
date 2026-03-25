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

        //public static Memory attachTo(Process selectedProc)
        //{
        //    if (selectedProc == null) { return null; }
        //    try
        //    {
        //        return new Memory(selectedProc);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Error when attaching:\r\n" + e.Message);
        //    }
        //}

    }
}
