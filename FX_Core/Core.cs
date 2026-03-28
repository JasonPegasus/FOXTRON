using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace FX_Core
{
    public static class Core
    {
        //////////////////////////// GLOBAL USAGE ////////////////////////////

        //////////////////////////// ATTACHING ////////////////////////////
        public static Scanner scanner { get; private set; }

        public static Scanner Attach(string processName) { return Attach(Process.GetProcessesByName(processName)[0]); }

        public static Scanner Attach(Process process)
        {
            if (isAttached() || process == null)
            { throw new Exception("- Error on Core.Attach():\r\n" + "Process was already attached!"); }

            try
            {
                scanner = new Scanner(process);
                return scanner;
            }
            catch (Exception e)
            { throw new Exception("- Error on Core.Attach():\r\n" + e.Message); }
        }

        public static bool Detach()
        {
            if (scanner == null) { return false; }
            scanner = null; return true;
        }

        public static void UpdateAttach()
        {
            if (scanner != null && !scanner.isProcessValid()) 
            { Detach(); }
        }

        public static bool isAttached()
        {
            UpdateAttach();
            return scanner != null;
        }
    }
}