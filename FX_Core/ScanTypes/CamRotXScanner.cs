using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX_Core.ScanTypes
{
    [ScanName("Camera Yaw Rotation")]
    internal class CamRotXScanner : ScanType
    {
        public CamRotXScanner(ProcessAnalyzer analyzedProcess) : base(analyzedProcess) { }

        Dictionary<nint, float> Find360()
        {
            return MEM.ScanFloatFiltered(e => (e != 0 && e != 1 && Shared.isBetween(e, -360, 360)));
        }

        void FilterBy360(ref Dictionary<nint, float> pointers, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                ProcessManager.SetForegroundWindow(aProc.process.MainWindowHandle);
                Thread.Sleep(10);
                InputManager.MoveMouse(20, 0);
                Thread.Sleep(10);
                Pause(true);
                Shared.Print($"Removed {MEM.FilterValues(ref pointers, v => Shared.isBetween(v, -360, 360))} non-360 values! ({pointers.Count} remaining...) ({i}/{iterations})");
                Pause(false);
            }
        }

        void FilterByEqual(ref Dictionary<nint, float> pointers, int iterations)
        {
            int baseProg = scanProgress;
            int endProg = baseProg + 30;
            for (int i = 0; i < iterations; i++)
            {
                bool doMove = Shared.random.Next(2) == 0;
                ProcessManager.SetForegroundWindow(aProc.process.MainWindowHandle);
                Thread.Sleep(10);
                if (doMove) { InputManager.MoveMouseRepeat(5, 0, 5, 10); }
                Thread.Sleep(doMove ? 20 : 10);
                Pause(true);
                Shared.Print($"Removed {MEM.CompareFilterValues(ref pointers, (a, b) => (Shared.isBetween(b, -360, 360) && ((doMove) ? a != b : a == b)))} equal values! [doMove: {doMove}] ({pointers.Count} remaining...) ({i}/{iterations})");
                Pause(false);
                //scanProgress = (int)Math.Round(Single.Lerp(baseProg, endProg, i/iterations)); // COMMENTED OUT THANKS TO LACK OF ASYNC UPDATES...
            }
        }

        void FilterByProportion(ref Dictionary<nint, float> pointers, int iterations) // LEFT UNUSED, TOO UNSTABLE (false positives and mainly negatives)
        {
            Dictionary<nint, List<float>> history = pointers.Keys.ToDictionary(k => k, k => new List<float>());
            Shared.Print("hist count: " + history.Count);
            for (int i = 0; i < iterations; i++)
            {
                ProcessManager.SetForegroundWindow(aProc.process.MainWindowHandle);
                Thread.Sleep(10);
                InputManager.MoveMouseRepeat(1, 0, 50, 10);
                Thread.Sleep(10);

                MEM.GetValuesDeltas(pointers, ref history);
                Shared.Print($"Got values ({i})");
            }
            Shared.Print("Now starting the ranking");
            List<nint> pointerRank = history.Select(kv =>
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

            Shared.Print("Finished ranking, printing last 10");

            for (int i = 0; i < Math.Min(10, pointerRank.Count); i++)
            { Shared.Print($"Ptr: 0x{pointerRank[i].ToString("X")}"); }

            Shared.Print("it should be in " + pointerRank.IndexOf(MEM.temp_CamAddress));
        }

        void FilterByWrite(ref Dictionary<nint, float> pointers)
        {
            foreach (nint ptr in pointers.Keys)
            {
                float ogValue = MEM.ReadFloat(ptr);
                InputManager.MoveMouseRepeat(5, 0, 5, 10);
                Thread.Sleep(50);

                if (MEM.ReadFloat(ptr) == ogValue)
                {
                    Shared.Print($"{Shared.PtrStr(ptr)} got reset or did not change");
                    pointers.Remove(ptr);
                    continue;
                }
                MEM.WriteFloat(ptr, ogValue);
                Thread.Sleep(50);

                if (MEM.ReadFloat(ptr) == ogValue)
                {
                    Shared.Print($"{Shared.PtrStr(ptr)} WAS FINE!!!");
                    continue;
                }


                Shared.Print($"{Shared.PtrStr(ptr)} discarded");
                pointers.Remove(ptr);
            }
        }

        void FilterByCopies(ref Dictionary<nint, float> pointers)
        {
            List<nint> possibles = new();
            foreach (nint ptr in pointers.Keys)
            {
                Pause(true);
                Dictionary<nint, float> ogPtrs = pointers.ToDictionary(e => e.Key, e => MEM.ReadFloat(e.Key));
                Shared.Print($"{Shared.PtrStr(ptr)} (pre)  = {MEM.ReadFloat(ptr)}");
                MEM.DoFloat(ptr, e => e * 1.1f);
                Shared.Print($"{Shared.PtrStr(ptr)} (post) = {MEM.ReadFloat(ptr)}");
                Pause(false);
                Thread.Sleep(100);

                bool doBreak = false;
                foreach (var og in ogPtrs)
                {
                    if (og.Key != ptr && og.Value != MEM.ReadFloat(og.Key))
                    {
                        Shared.Print($"{Shared.PtrStr(ptr)} IS A TOTAL PARENT!!");
                        possibles.Add(ptr);
                        doBreak = true;
                        break;
                    }
                }
                MEM.WriteFloat(ptr, ogPtrs[ptr]);
                if (doBreak) { continue; }

                Shared.Print($"{Shared.PtrStr(ptr)} did not change the rest");

                Thread.Sleep(100);
            }
            pointers = possibles.ToDictionary(e => e, e => MEM.ReadFloat(e));
        }

       

        public override FloatPointer[] ReturnPossiblePointers()
        {
            int cleans = 200;
            ProcessManager.SetFoxtronPriority(true);
            scanProgress = 5;
            Dictionary<nint, float> pointers = Find360();
            scanProgress = 30;

            Shared.Print($"Found {pointers.Count} values in the initial 360º range");

            FilterByEqual(ref pointers, cleans);
            scanProgress = 60;
            FilterByWrite(ref pointers);
            scanProgress = 80;
            FilterByCopies(ref pointers);

            foreach (var group in pointers.GroupBy(g => g.Value))
            {
                foreach (var ptr in group)
                {
                    Shared.Print($"POSSIBLE: [0x{ptr.Key.ToString("X")} | Value: {ptr.Value}]");
                }
            }
            Shared.Print("Final Count: " + pointers.Count);

            return FloatPointer.FromDictionary(pointers);
        }
    }
}
