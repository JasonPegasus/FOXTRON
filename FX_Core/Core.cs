using System.Diagnostics;

namespace FX_Core
{
    public static class Core
    {
        //////////////////////////// ATTACHING ////////////////////////////
        public static Scanner scanner { get; private set; }

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