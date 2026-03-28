using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core
{
    public class Scanner
    {
        /////////////////////////////// PUBLIC STUFF ///////////////////////////////
        
        Memory MEM;

        public Scanner(Process process) { MEM = new Memory(process); }

        public bool isProcessValid() { return MEM != null && MEM.isProcessValid(); }
        
        public Process Process() { return MEM.getProcess(); }

        public Memory Memory() { return MEM; }

        /////////////////////////////// SCANNING METHODS ///////////////////////////////

        bool Between(float num, float min, float max) { return num >= min && num <= max; }

        Dictionary<IntPtr, float> Find360()
        {
            return MEM.ScanFloatFiltered(e => float.Round(e) != 0 && Between(e, -360, 360));
        }


        /////////////////////////////// CURRENT SCAN DATA ///////////////////////////////


        public IntPtr FindCamera(int cleans = 20)
        {
            Dictionary<IntPtr, float> values = Find360();
            Shared.Log($"Found {values.Count} 360 values");

            ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
            for (int i = 0; i < cleans; i++)
            {
                InputSimulator.MoveMouse(10, 0);
                ProcessManager.SetPauseProcess(Process(), true);
                Task.Delay(10).Wait();
                Shared.Log($"Removed {Shared.FilterPointerDictionary(ref values, e => !Between(e, -360, 360))} values!");
                ProcessManager.SetPauseProcess(Process(), false);
            }
            return nint.Zero;
        }

    }

    class NewScan()
    {

    }
}