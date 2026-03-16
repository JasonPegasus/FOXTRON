using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Swed64;

namespace FX_Core
{
    public static class Memory
    {
        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(
        IntPtr hProcess,
        IntPtr lpAddress,
        out MEMORY_BASIC_INFORMATION lpBuffer,
        uint dwLength);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int dwSize,
        out IntPtr lpNumberOfBytesRead);

        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }


        public static List<IntPtr> ScanFloatRange(Process process, float min, float max)
        {
            List<IntPtr> results = new();

            IntPtr address = IntPtr.Zero;

            while (true)
            {
                MEMORY_BASIC_INFORMATION m;

                int result = VirtualQueryEx(
                    process.Handle,
                    address,
                    out m,
                    (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION)));

                if (result == 0)
                    break;

                bool isReadable =
                    m.State == 0x1000 &&          // MEM_COMMIT
                    (m.Protect & 0x01) == 0;      // not PAGE_NOACCESS

                if (isReadable)
                {
                    int size = (int)m.RegionSize;

                    byte[] buffer = new byte[size];

                    if (ReadProcessMemory(
                        process.Handle,
                        m.BaseAddress,
                        buffer,
                        size,
                        out _))
                    {
                        for (int i = 0; i < size - 4; i++)
                        {
                            float value = BitConverter.ToSingle(buffer, i);

                            if (value >= min && value <= max)
                            {
                                long found =
                                    m.BaseAddress.ToInt64() + i;

                                results.Add((IntPtr)found);
                            }
                        }
                    }
                }

                address =
                    new IntPtr(
                        m.BaseAddress.ToInt64() +
                        (long)m.RegionSize);
            }

            return results;
        }
    }
}
