using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
            return MEM.ScanFloatFiltered(e => (e != 0 && e != 1 && Between(e, -360, 360)));
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
            Shared.Log($"Found {pointers.Count} values in the initial 360º range");

            //FilterBy360(pointers, 1);
            FilterByEqual(pointers, 100);
            FilterByProportion(pointers, 20);

            //foreach(var ptr in pointers) { Shared.Log($"Ptr: 0x{ptr.Key.ToString("X")} | Value: {ptr.Value}"); }

            ProcessManager.SetFoxtronHighPriority(false);
            return IntPtr.Zero;
        }


        void FilterByProportion(Dictionary<IntPtr, float> pointers, int iterations)
        {
            Dictionary<IntPtr, List<float>> history = pointers.Keys.ToDictionary(k => k, k => new List<float>());
            Shared.Log("hist count: " + history.Count);
            for (int i = 0; i < iterations; i++)
            {
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                InputSimulator.MoveMouse(20, 0);
                Thread.Sleep(10);

                MEM.GetValuesDeltas(pointers, ref history);
                Shared.Log($"Got values ({i})");
            }
            Shared.Log("Now starting the ranking");
            List<IntPtr> pointerRank = history.Select(kv =>
            {
                List<float> deltas = kv.Value;
                float avg = deltas.Average();
                float variance = deltas.Sum(d => (d - avg) * (d - avg)) / deltas.Count;
                float stddev = MathF.Sqrt(variance);
                return new
                {
                    Ptr = kv.Key,
                    Avg = avg,
                    StdDev = stddev,
                    Score = avg / (1f + stddev)
                };
            }).OrderByDescending(x => x.Score).Select(x => x.Ptr).ToList();

            Shared.Log("Finished ranking, printing last 10");

            for (int i = 0; i < Math.Min(10, pointerRank.Count); i++)
            { Shared.Log($"Ptr: 0x{pointerRank[i].ToString("X")}"); }

            Shared.Log("it should be in "+ pointerRank.IndexOf((IntPtr)0x6B832B48));
        }

        void FilterByEqual(Dictionary<IntPtr, float> pointers, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                bool doMove = Shared.random.Next(1) == 0;
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                InputSimulator.MoveMouse(doMove ? 50 : 0, 0);
                Thread.Sleep(doMove ? 10 : 100);
                Pause(true);
                Shared.Log($"Removed {MEM.CompareFilterValues(ref pointers, (a, b) => (Between(a, -360, 360) && (doMove) ? a != b : a == b))} equal values! ({pointers.Count} remaining...)");
                Pause(false);
            }
        }

        void FilterBy360(Dictionary<IntPtr, float> pointers, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                InputSimulator.MoveMouse(20, 0);
                Thread.Sleep(10);
                Pause(true);
                Shared.Log($"Removed {MEM.FilterValues(ref pointers, v => Between(v, -360, 360))} non-360 values! ({pointers.Count} remaining...)");
                Pause(false);
            }
        }
    }
}