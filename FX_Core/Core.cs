namespace FX_Core
{
    public static class Core
    {
        public static void tryFindAll()
        {
            var results = Memory.ScanFloatRange(p, 0f, 360f);

            Console.WriteLine($"Found {results.Count} addresses");

            foreach (var addr in results)
            {
                Console.WriteLine($"0x{addr.ToInt64():X}");
            }
        }
    }
}