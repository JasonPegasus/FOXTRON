using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Swed64;

namespace FX_Core
{
    public class Memory
    {
        ////////////////////////////////////// KERNEL IMPORTS //////////////////////////////////////
        ///
        [DllImport("kernel32.dll")] static extern int  VirtualQueryEx   (IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        [DllImport("kernel32.dll")] static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")] static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize);

        ////////////////////////////////////// PROCESS AND ACCESSES //////////////////////////////////////
        ///
        Process _proc;

        public Memory(Process proc) { this._proc = proc; }

        public bool isProcessValid() { return _proc != null && !_proc.HasExited; }

        public Process getProcess() { return _proc; }

        ////////////////////////////////////// SCANNING AND SEARCHING ///////////////////////////////////////

        public Dictionary<IntPtr, float> ScanFloatFiltered(Predicate<float> filter)
        {
            Dictionary<IntPtr, float> results = new();

            IntPtr address = IntPtr.Zero;

            while (true)
            {
                MEMORY_BASIC_INFORMATION m;

                if (VirtualQueryEx(_proc.Handle, address, out m, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) == 0) { break; };

                if (m.State == 0x1000 && (m.Protect & 0x01) == 0)
                {
                    int size = (int)m.RegionSize;

                    byte[] buffer = new byte[size];

                    if (ReadProcessMemory(_proc.Handle, m.BaseAddress, buffer, size, out _))
                    {
                        for (int i = 0; i < size - 4; i++)
                        {
                            if (filter(BitConverter.ToSingle(buffer, i)))
                            { 
                                IntPtr p = (IntPtr) m.BaseAddress.ToInt64() + i;
                                results[p] = ReadFloat(p); 
                            }
                        }
                    }
                }
                address = new IntPtr(m.BaseAddress.ToInt64() + (long)m.RegionSize);
            }
            return results;
        }

        /////////////////////////////////// READ & WRITE //////////////////////////////////////

        public byte[] ReadBytes(IntPtr ptr, int bytes)
        {
            byte[] buffer = new byte[bytes];
            ReadProcessMemory(_proc.Handle, ptr, buffer, bytes, out _);
            return buffer;
        }

        public float ReadFloat(IntPtr address)
        { return BitConverter.ToSingle(ReadBytes(address, 4)); }


        ////////////////////////////////////// OTHER //////////////////////////////////////

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
    }
}
