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

        int _memChunkSize;
        public int MemoryChunkSize { get { return _memChunkSize; } set { _memChunkSize = value; memory.MEMORY_CHUNK_SIZE = value; } }

        internal ProcessAnalyzer(Process process)
        {
            this.process = process;
            this.memory = new Memory(process);
        }


        public FloatPointer[]? ExecuteScanByName(string name, Action<int>? updateProgressMethod = null)
        {
            Type sType = ScanType.ScansList[name];
            if (sType is null) return null;

            MethodInfo? executeScanMethod = this.GetType().GetMethod(nameof(ExecuteScan), BindingFlags.Instance | BindingFlags.NonPublic);
            if (executeScanMethod is null) return null;
            
            return (FloatPointer[])executeScanMethod.MakeGenericMethod(sType).Invoke(this, new object[] { updateProgressMethod });
        }

        internal FloatPointer[] ExecuteScan<T>(Action<int>? updateProgressMethod = null) where T : ScanType
        {
            ScanType scanType = ((ScanType)Activator.CreateInstance(typeof(T), this));
            if (updateProgressMethod is not null) { scanType.OnProgress += updateProgressMethod; }
            return scanType.ReturnPossiblePointers();
        }

        public bool isValid()
        { return ProcessManager.isProcessValid(process); }
    }
}
