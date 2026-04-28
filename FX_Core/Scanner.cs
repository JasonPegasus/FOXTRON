using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

        public Scanner(Process process, bool pauseWhileScanning = false)
        { 
            pauseScan = pauseWhileScanning;
            MEM = new Memory(process); 
        }

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

        void Pause(bool p) 
        {
            if (!pauseScan) { return; }
            ProcessManager.SetPauseProcess(Process(), p); 
        }

        public IntPtr FindCamera(int cleans = 20)
        {
            ProcessManager.SetFoxtronHighPriority(true);
            Dictionary<IntPtr, float> pointers = Find360();
            
            Shared.Log($"Found {pointers.Count} 360 values");

            for (int i = 0; i < 5; i++)
            {
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                InputSimulator.MoveMouse(20, 0);
                Thread.Sleep(10);
                Pause(true);
                Shared.Log($"Removed {MEM.FilterValues(ref pointers, v => Between(v, -360, 360))} non-360 values! ({pointers.Count} remaining...)");
                Pause(false);
            }

            for (int i = 0; i < 10; i++)
            {
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                InputSimulator.MoveMouse(20, 0);
                Thread.Sleep(10);
                Pause(true);
                Shared.Log($"Removed {MEM.CompareFilterValues(ref pointers, (a, b) => a != b)} equal values! ({pointers.Count} remaining...)");
                Pause(false);
            }

            Dictionary<nint, float> deltas = new Dictionary<nint, float>();

            for (int i = 0; i < 60; i++)
            {
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                if (Shared.random.Next(2) == 0)
                {
                    InputSimulator.MoveMouse(20, 0);
                    Thread.Sleep(10);
                    Pause(true);
                    Shared.Log($"Removed {MEM.CompareFilterValues(ref pointers, (a, b) =>
                    {
                        float delta = b - a;

                        // wrap fix
                        if (delta > 180) delta -= 360;
                        if (delta < -180) delta += 360;

                        return Math.Abs(delta) > 0 && delta < 50; // threshold inicial
                    })} non-proportional values! ({pointers.Count} remaining...)");
                }
                else
                {
                    Shared.Log($"Removed {MEM.CompareFilterValues(ref pointers, (a, b) => a == b)} non-equal values! ({pointers.Count} remaining...)");
                }
                Pause(false);
            }

            foreach(var ptr in pointers) { Shared.Log($"Ptr: 0x{ptr.Key.ToString("X")} | Value: {ptr.Value}"); }

            ProcessManager.SetFoxtronHighPriority(false);
            return nint.Zero;
        }
    }
}