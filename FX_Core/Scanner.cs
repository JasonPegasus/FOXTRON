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

        // UNUSED //
        void FilterBy360(ref Dictionary<IntPtr, float> pointers, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                InputSimulator.MoveMouse(20, 0);
                Thread.Sleep(10);
                Pause(true);
                Shared.Log($"Removed {MEM.FilterValues(ref pointers, v => Between(v, -360, 360))} non-360 values! ({pointers.Count} remaining...) ({i}/{iterations})");
                Pause(false);
            }
        }

        void FilterByEqual(ref Dictionary<IntPtr, float> pointers, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                bool doMove = Shared.random.Next(2) == 0;
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                if (doMove) { InputSimulator.MoveMouseRepeat(1, 0, 20, 10); }
                Thread.Sleep(doMove ? 50 : 10);
                Pause(true);
                Shared.Log($"Removed {MEM.CompareFilterValues(ref pointers, (a, b) => (Between(b, -360, 360) && ((doMove) ? a != b : a == b)))} equal values! [doMove: {doMove}] ({pointers.Count} remaining...) ({i}/{iterations})");
                Pause(false);
            }
        }

        void FilterByProportion(ref Dictionary<IntPtr, float> pointers, int iterations)
        {
            Dictionary<IntPtr, List<float>> history = pointers.Keys.ToDictionary(k => k, k => new List<float>());
            Shared.Log("hist count: " + history.Count);
            for (int i = 0; i < iterations; i++)
            {
                ProcessManager.SetForegroundWindow(Process().MainWindowHandle);
                Thread.Sleep(10);
                InputSimulator.MoveMouseRepeat(1, 0, 50, 10);
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

            Shared.Log("it should be in " + pointerRank.IndexOf(MEM.temp_CamAddress));
        }

        public List<IntPtr> GetFinalPointerByWrite(Dictionary<IntPtr, float> pointers)
        {
            List<IntPtr> possiblePtrs = new List<IntPtr>();
            foreach (var group in pointers.GroupBy(e => e.Value))
            {
                foreach (var ptr in group)
                {
                    IntPtr addr = ptr.Key;
                    float ogValue = MEM.ReadFloat(addr);
                    float newValue = MEM.DoFloat(addr, v => v + 50);
                    Console.WriteLine($"0x{addr.ToString("X")}: {ogValue} -> {newValue}");
                    Thread.Sleep(500);

                    bool doContinue = false;
                    foreach (var subptr in group)
                    {
                        if (MEM.ReadFloat(subptr.Key) != newValue)
                        {
                            Console.WriteLine($"0x{subptr.Key.ToString("X")} not equal | 0x{ptr.Key.ToString("X")} was not parent");
                            doContinue = true;
                            break;
                        }
                    }
                    if (doContinue) continue;

                    Console.WriteLine($"0x{ptr.Key.ToString("X")} IS A PARENT!1!1");
                    if (!possiblePtrs.Contains(ptr.Key)) { possiblePtrs.Add(ptr.Key); }
                    Thread.Sleep(2000);
                }
            }
            return possiblePtrs;
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

            FilterByEqual(ref pointers, 100);
            FilterByProportion(ref pointers, 2);
            List<IntPtr> finalPointers = GetFinalPointerByWrite(pointers);

            foreach (IntPtr pointer in finalPointers) { Shared.Log($"possible?: 0x{pointer.ToString("X")}"); }
            Shared.Log("Final Count: " + finalPointers.Count);

            //Shared.Log($"The final pointer is: 0x{finalPointer.ToString("X")}");

            ProcessManager.SetFoxtronHighPriority(false);
            return IntPtr.Zero;
        }
    }
}