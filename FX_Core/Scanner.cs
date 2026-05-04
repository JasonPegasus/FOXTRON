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
                InputManager.MoveMouse(20, 0);
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
                if (doMove) { InputManager.MoveMouseRepeat(5, 0, 5, 10); }
                Thread.Sleep(doMove ? 20 : 10);
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
                InputManager.MoveMouseRepeat(1, 0, 50, 10);
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

        void FilterByWrite(ref Dictionary<IntPtr, float> pointers)
        {
            foreach (IntPtr ptr in pointers.Keys)
            {
                float ogValue = MEM.ReadFloat(ptr);
                InputManager.MoveMouseRepeat(5, 0, 5, 10);
                Thread.Sleep(50);

                if (MEM.ReadFloat(ptr) == ogValue)
                {
                    Shared.Log($"{Shared.PtrStr(ptr)} got reset or did not change");
                    pointers.Remove(ptr);
                    continue;
                }
                MEM.WriteFloat(ptr, ogValue);
                Thread.Sleep(50);

                if (MEM.ReadFloat(ptr) == ogValue) 
                { 
                    Shared.Log($"{Shared.PtrStr(ptr)} WAS FINE!!!");
                    continue;
                }


                Shared.Log($"{Shared.PtrStr(ptr)} discarded");
                pointers.Remove(ptr);
            }
        }
        
        void FilterByCopies(ref Dictionary<IntPtr, float> pointers)
        {
            List<IntPtr> possibles = new();
            foreach (IntPtr ptr in pointers.Keys)
            {
                Pause(true);
                Dictionary<IntPtr, float> ogPtrs = pointers.ToDictionary(e => e.Key, e => MEM.ReadFloat(e.Key));
                Shared.Log($"{Shared.PtrStr(ptr)} (pre)  = {MEM.ReadFloat(ptr)}");
                MEM.DoFloat(ptr, e => e * 1.1f);
                Shared.Log($"{Shared.PtrStr(ptr)} (post) = {MEM.ReadFloat(ptr)}");
                Pause(false);
                Thread.Sleep(100);

                bool doBreak = false;
                foreach (var og in ogPtrs)
                {
                    if (og.Key != ptr && og.Value != MEM.ReadFloat(og.Key))
                    {
                        Shared.Log($"{Shared.PtrStr(ptr)} IS A TOTAL PARENT!!");
                        possibles.Add(ptr);
                        doBreak = true;
                        break;
                    }
                }
                MEM.WriteFloat(ptr, ogPtrs[ptr]);
                if (doBreak) { continue; }

                Shared.Log($"{Shared.PtrStr(ptr)} did not change the rest");

                Thread.Sleep(100);
            }
            pointers = possibles.ToDictionary(e => e, e => MEM.ReadFloat(e));
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
            FilterByWrite(ref pointers);
            FilterByCopies(ref pointers);

            foreach (var group in pointers.GroupBy(g => g.Value))
            {
                foreach (var ptr in group) 
                { 
                    Shared.Log($"POSSIBLE: [0x{ptr.Key.ToString("X")} | Value: {ptr.Value}]"); 
                }
            }
            Shared.Log("Final Count: " + pointers.Count);


            IntPtr fp = pointers.Keys.First(e => true);
            float unit = MEM.ReadFloat(fp)*0.01f;
            while (true)
            {
                Thread.Sleep(100);
                MEM.DoFloat(fp, e => e+unit);
            }

            ProcessManager.SetFoxtronHighPriority(false);
            return IntPtr.Zero;
        }
    }
}