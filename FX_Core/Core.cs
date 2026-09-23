using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using FX_Core.Scanners;

namespace FX_Core
{
    public class Core
    {
        //////////////////////////// GLOBAL USAGE ////////////////////////////

        private readonly IUserInteractions userInteraction;

        public Core(IUserInteractions interaction)
        {
            userInteraction = interaction;
        }

        //////////////////////////// OBJECT FINDING ////////////////////////////

        public void MakePlayer()
        {
            DObjectManager.ScanPlayerData();
        }

        //////////////////////////// ATTACHING ////////////////////////////

        internal AttachedProcess CURRENT_PROCESS { get; private set; }

        public Process Attach(string processName) { return Attach(Process.GetProcessesByName(processName)[0]); }
        public  Process Attach(Process process)
        {
            if (isAttached() || process is null)
            { throw new Exception("- Error on Core.Attach():\r\n" + "Process was already attached!... Or was null..."); }

            try
            {
                CURRENT_PROCESS = new(process);
                return process;
            }
            catch (Exception e) { throw new Exception("- Error on Core.Attach():\r\n" + e.Message); }
        }

        public bool Detach()
        {
            if (CURRENT_PROCESS is null) { return false; }
            CURRENT_PROCESS = null; return true;
        }

        public void UpdateAttach()
        {
            if (CURRENT_PROCESS is not null && !CURRENT_PROCESS.isProcessValid()) 
            { Detach(); }
        }

        public bool isAttached()
        {
            UpdateAttach();
            return CURRENT_PROCESS is not null;
        }
    }
}