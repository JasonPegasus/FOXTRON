using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FX_UnsafeMemory;

namespace FX_Core.ScanTypes
{
    [ScanTypeData("Camera Yaw Rotation")]
    internal class CamRotXScanner : ScanType
    {
        public CamRotXScanner(ProcessAnalyzer analyzedProcess) : base(analyzedProcess) { }

        List<TPointer<float>> Find360()
        {
            return MEM.ScanFloatFiltered(e => (e != 0 && e != 1 && Shared.isBetween(e, -360, 360)));
        }

        // Los filtros modifican la lista directamente (in-place), por eso ya no hace falta "ref"
        void FilterBy360(List<TPointer<float>> pointers, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                ProcessManager.SetForegroundWindow(aProc.process.MainWindowHandle);
                Thread.Sleep(10);
                InputManager.MoveMouse(20, 0);
                Thread.Sleep(10);
                Pause(true);
                Shared.Print($"Removed {MEM.FilterValues(pointers, v => Shared.isBetween(v, -360, 360))} non-360 values! ({pointers.Count} remaining...) ({i}/{iterations})");
                Pause(false);
            }
        }

        void FilterByEqual(List<TPointer<float>> pointers, int iterations)
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
                Shared.Print($"Removed {MEM.CompareFilterValues(pointers, (a, b) => (Shared.isBetween(b, -360, 360) && ((doMove) ? a != b : a == b)))} equal values! [doMove: {doMove}] ({pointers.Count} remaining...) ({i}/{iterations})");
                Pause(false);
                //scanProgress = (int)Math.Round(Single.Lerp(baseProg, endProg, i/iterations)); // COMMENTED OUT THANKS TO LACK OF ASYNC UPDATES...
            }
        }

        void FilterByProportion(List<TPointer<float>> pointers, int iterations) // LEFT UNUSED, TOO UNSTABLE (false positives and mainly negatives)
        {
            Dictionary<nint, List<float>> history = pointers.ToDictionary(p => p.Address, p => new List<float>());
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

        void FilterByWrite(List<TPointer<float>> pointers)
        {
            int write = 0; // Posicion donde se guarda el proximo sobreviviente (compactacion in-place)

            for (int read = 0; read < pointers.Count; read++)
            {
                nint ptr = pointers[read].Address;

                float ogValue = MEM.ReadFloat(ptr);
                InputManager.MoveMouseRepeat(5, 0, 5, 10);
                Thread.Sleep(50);

                if (MEM.ReadFloat(ptr) == ogValue)
                {
                    Shared.Print($"{Shared.PtrStr(ptr)} got reset or did not change");
                    continue; // descartado
                }
                MEM.WriteFloat(ptr, ogValue);
                Thread.Sleep(50);

                if (MEM.ReadFloat(ptr) == ogValue)
                {
                    Shared.Print($"{Shared.PtrStr(ptr)} WAS FINE!!!");
                    pointers[write++] = new TPointer<float>(ptr, ogValue);
                    continue;
                }

                Shared.Print($"{Shared.PtrStr(ptr)} discarded");
            }

            pointers.RemoveRange(write, pointers.Count - write);
        }

        void FilterByCopies(List<TPointer<float>> pointers)
        {
            List<TPointer<float>> possibles = new();
            float[] ogValues = new float[pointers.Count]; // Reutilizado en cada iteracion

            for (int i = 0; i < pointers.Count; i++)
            {
                nint ptr = pointers[i].Address;

                Pause(true);
                for (int j = 0; j < pointers.Count; j++) { ogValues[j] = MEM.ReadFloat(pointers[j].Address); } // Foto de todos los valores antes de modificar
                Shared.Print($"{Shared.PtrStr(ptr)} (pre)  = {MEM.ReadFloat(ptr)}");
                MEM.DoFloat(ptr, e => e * 1.1f);
                Shared.Print($"{Shared.PtrStr(ptr)} (post) = {MEM.ReadFloat(ptr)}");
                Pause(false);
                Thread.Sleep(100);

                bool doBreak = false;
                for (int j = 0; j < pointers.Count; j++)
                {
                    if (j != i && ogValues[j] != MEM.ReadFloat(pointers[j].Address))
                    {
                        Shared.Print($"{Shared.PtrStr(ptr)} IS A TOTAL PARENT!!");
                        possibles.Add(pointers[i]);
                        doBreak = true;
                        break;
                    }
                }
                MEM.WriteFloat(ptr, ogValues[i]);
                if (doBreak) { continue; }

                Shared.Print($"{Shared.PtrStr(ptr)} did not change the rest");

                Thread.Sleep(100);
            }

            // Reemplaza el contenido de la lista original por los candidatos, con su valor actual
            pointers.Clear();
            foreach (var p in possibles)
            {
                pointers.Add(new TPointer<float>(p.Address, MEM.ReadFloat(p.Address)));
            }
        }



        public override FloatVariable[] ReturnPossiblePointers()
        {
            int cleans = 200;
            ProcessManager.SetFoxtronPriority(true);
            scanProgress = 5;
            List<TPointer<float>> pointers = Find360();
            scanProgress = 30;

            Shared.Print($"Found {pointers.Count} values in the initial 360º range");

            FilterByEqual(pointers, cleans);
            scanProgress = 60;
            FilterByWrite(pointers);
            scanProgress = 80;
            FilterByCopies(pointers);

            foreach (var group in pointers.GroupBy(g => g.Value))
            {
                foreach (var ptr in group)
                {
                    Shared.Print($"POSSIBLE: [0x{ptr.Address.ToString("X")} | Value: {ptr.Value}]");
                }
            }
            Shared.Print("Final Count: " + pointers.Count);

            pointers.TrimExcess();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // La lista final es chica, asi que este diccionario temporal solo sirve de puente con FloatVariable.FromDictionary
            return FloatVariable.FromDictionary(pointers.ToDictionary(p => p.Address, p => p.Value));
        }
    }
}
