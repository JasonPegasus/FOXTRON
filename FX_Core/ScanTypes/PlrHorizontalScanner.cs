namespace FX_Core.ScanTypes
{
    [ScanTypeData("Player X")]
    internal class PlrHorizontalScanner : ScanType
    {
        public PlrHorizontalScanner(ProcessAnalyzer analyzedProcess) : base(analyzedProcess)
        {
        }

        public override FloatVariable[] ReturnPossiblePointers()
        {
            throw new NotImplementedException();
        }
    }
}
