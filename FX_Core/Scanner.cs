using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX_UnsafeMemory;

namespace FX_Core
{
    public class Scanner
    {
        /////////////////////////////// PUBLIC STUFF ///////////////////////////////
        
        Memory MEM;
        bool pauseScan;

        public Scanner(Process process, bool pauseWhileScanning = true)
        { 
            pauseScan = pauseWhileScanning;
            MEM = new Memory(process); 
        }

        public bool isProcessValid() { return MEM != null && MEM.isProcessValid(); }
        
        public Process Process() { return MEM.getProcess(); }

        public Memory Memory() { return MEM; }

        /////////////////////////////// SCANNING METHODS ///////////////////////////////

        bool Between(float num, float min, float max) { return num >= min && num <= max; }

        List<IntPtr> Find360()
        {
            return MEM.ScanFloatFiltered(e => float.Round(e) != 0 && Between(e, -360, 360));
        }


        /////////////////////////////// CURRENT SCAN DATA ///////////////////////////////

        void Pause(bool p) 
        {
            if (!pauseScan) { return; }
            ProcessManager.SetPauseProcess(Process(), p); 
        }

        public IntPtr FindCamera(int cleans = 20)
        {
            List<IntPtr> values = Find360();
            Shared.Log($"Found {values.Count} 360 values");

            for (int i = 0; i < cleans; i++)
            {
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                InputSimulator.MoveMouse(50, 0);
                Thread.Sleep(10);
                Pause(true);
                Shared.Log($"Removed {MEM.FilterValues(ref values, e => Between(e, -360, 360))} values! ({values.Count} remaining)");
                Pause(false);
            }
            return nint.Zero;
        }
    }
}