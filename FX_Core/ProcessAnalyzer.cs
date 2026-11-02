using System.Diagnostics;
using System.Reflection;
using FX_UnsafeMemory;
using static FX_Core.Shared;

namespace FX_Core
{
    public sealed class ProcessAnalyzer
    {
        public Process process;
        public Memory memory;
        public AnalyzerSettings settings;

        internal ProcessAnalyzer(Process process)
        {
            this.process = process;
            this.memory = new Memory(process);
        }

        public Dictionary<string, FloatVariable> FOUND_VARIABLES { get; private set; } = new();

        public FloatVariable AddVariableManually(string scanName, FloatVariable fv) { FOUND_VARIABLES[scanName] = fv; return fv; }

        public FloatVariable? ExecuteScanByName(string name, Func<FloatVariable[], FloatVariable> selectionFunction, Action<int>? updateProgressMethod = null)
        {
            Type sType = ScanType.ScansList[name];
            if (sType is null) return null;

            MethodInfo? executeScanMethod = this.GetType().GetMethod(nameof(ExecuteScan), BindingFlags.Instance | BindingFlags.NonPublic);
            if (executeScanMethod is null) return null;

            FloatVariable fv = selectionFunction.Invoke((FloatVariable[])executeScanMethod.MakeGenericMethod(sType).Invoke(this, new object[] { updateProgressMethod }));
            // no se puede llamar a ExecuteScan de manera directa ya que sType es una variable y no un tipo puro, entonces hay que usar reflection para llamarlo
            // además, el updateProgressMethod se pasa en un array de objetos porque el generic method no sabe que argumentos tiene ExecuteScan, asi q pasa array de args

            FOUND_VARIABLES[name] = fv;
            return fv;
    }

        internal FloatVariable[] ExecuteScan<T>(Action<int>? updateProgressMethod = null) where T : ScanType
        {
            ScanType scanType = ((ScanType)Activator.CreateInstance(typeof(T), this));
            if (updateProgressMethod is not null) { scanType.OnProgress += updateProgressMethod; }

            int memsize = settings.MemoryPageSize;
            memory.MEMORY_PAGE_SIZE = memsize;
            DateTime startTime = DateTime.Now;
            Shared.Print($"Starting Scan Execution of: {typeof(T)}\n" +
                $"   (Memory Page Size: {memsize / 1024}KB ({memsize} / 0x{memsize.ToString("X")}))", PrintType.Info);

            FloatVariable[] result = scanType.ReturnPossiblePointers();

            Shared.Print($"Finished Scan execution of: {typeof(T)}, elapsed {DateTime.Now.Subtract(startTime).TotalSeconds}s", PrintType.Success);
            return result;
        }

        public bool isValid()
        { return ProcessManager.isProcessValid(process); }
    }

    public struct AnalyzerSettings
    {
        public AnalyzerSettings() {}

        public int MemoryPageSize = 0x1000;
        public int GameFPS = 60;
        public bool PauseWhileScanning = false;
    }
}
