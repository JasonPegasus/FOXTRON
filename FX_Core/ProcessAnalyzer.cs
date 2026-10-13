using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FX_UnsafeMemory;
using static FX_Core.Shared;

namespace FX_Core
{
    public sealed class ProcessAnalyzer
    {
        public Process process;
        public Memory memory;

        internal ProcessAnalyzer(Process process)
        {
            this.process = process;
            this.memory = new Memory(process);
        }

        public Func<int> MemoryChunkSizeGetter;

        public FloatVariable[]? ExecuteScanByName(string name, Action<int>? updateProgressMethod = null)
        {
            Type sType = ScanType.ScansList[name];
            if (sType is null) return null;

            MethodInfo? executeScanMethod = this.GetType().GetMethod(nameof(ExecuteScan), BindingFlags.Instance | BindingFlags.NonPublic);
            if (executeScanMethod is null) return null;
            
            return (FloatVariable[])executeScanMethod.MakeGenericMethod(sType).Invoke(this, new object[] { updateProgressMethod });
        }

        internal FloatVariable[] ExecuteScan<T>(Action<int>? updateProgressMethod = null) where T : ScanType
        {
            ScanType scanType = ((ScanType)Activator.CreateInstance(typeof(T), this));
            if (updateProgressMethod is not null) { scanType.OnProgress += updateProgressMethod; }

            int memsize = MemoryChunkSizeGetter.Invoke();
            memory.MEMORY_CHUNK_SIZE = memsize;
            Shared.Print($"Starting Scan Execution of: {typeof(T)}\n" +
                $"   (Memory Chunk Size: {memsize / 1024}KB ({memsize} / 0x{memsize.ToString("X")}))", PrintType.Info);
            return scanType.ReturnPossiblePointers();
        }

        public bool isValid()
        { return ProcessManager.isProcessValid(process); }
    }
}
