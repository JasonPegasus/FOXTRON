using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static FX_UnsafeMemory.Memory;

namespace FX_UnsafeMemory
{
    public class Memory
    {
        ////////////////////////////////////// KERNEL IMPORTS //////////////////////////////////////
        ///
        [DllImport("kernel32.dll")] static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        [DllImport("kernel32.dll")] static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")] static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize);

        ////////////////////////////////////// PROCESS AND ACCESSES //////////////////////////////////////
        ///
        Process _proc;

        public Memory(Process proc) { this._proc = proc; }

        public bool isProcessValid() { return _proc != null && !_proc.HasExited; }

        public Process getProcess() { return _proc; }

        ////////////////////////////////////// SCANNING AND SEARCHING ///////////////////////////////////////

        public struct DATA { IntPtr ptr; float value; }

        public unsafe Dictionary<IntPtr, float> ScanFloatFiltered(Predicate<float> filter)
        {
            var results = new Dictionary<IntPtr, float>();

            IntPtr address = IntPtr.Zero;

            const int chunkSize = 0x10000;

            while (true)
            {
                MEMORY_BASIC_INFORMATION m;

                if (VirtualQueryEx(_proc.Handle, address, out m, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) == 0)
                    break;

                if (m.State == 0x1000 && (m.Protect & 0x01) == 0)
                {
                    long baseAddr = m.BaseAddress.ToInt64();
                    long regionSize = (long)m.RegionSize;

                    for (long offset = 0; offset < regionSize; offset += chunkSize)
                    {
                        int toRead = (int)Math.Min(chunkSize, regionSize - offset);

                        byte[] buffer = new byte[toRead];

                        if (ReadProcessMemory(_proc.Handle, (IntPtr)(baseAddr + offset), buffer, toRead, out _))
                        {
                            fixed (byte* ptr = buffer)
                            {
                                for (int i = 0; i < toRead - 4; i += 4)
                                {
                                    float value = *(float*)(ptr + i);

                                    if (filter(value))
                                    {
                                        results.Add((IntPtr)(baseAddr + offset + i), value);
                                    }
                                    else if ((baseAddr + offset + i) == (IntPtr)0x6B832B48)
                                    {
                                        return new Dictionary<IntPtr, float>();
                                    }
                                }
                            }
                        }
                    }
                }

                address = new IntPtr(m.BaseAddress.ToInt64() + (long)m.RegionSize);
            }

            return results;
        }

        public unsafe int FilterValues(ref Dictionary<IntPtr, float> ptrs, Predicate<float> filter, int chunkSize = 0x10000) // 4KB default
        {
            Dictionary<IntPtr, float> result = new Dictionary<IntPtr, float>();
            int ogSize = ptrs.Count;

            byte[] buffer = new byte[chunkSize];
            foreach (var group in ptrs.GroupBy(addr => addr.Key.ToInt64() / chunkSize)) // Agrupar por bloque de memoria
            {
                long baseAddr = group.Key * chunkSize;

                if (!ReadProcessMemory(_proc.Handle, (IntPtr)baseAddr, buffer, chunkSize, out _)) continue;

                fixed (byte* ptr = buffer)
                {
                    foreach (var addr in group)
                    {
                        int offset = (int)(addr.Key.ToInt64() - baseAddr);

                        if (offset < 0 || offset > chunkSize - sizeof(float)) continue; // Seguridad, se fija que no lea cosas de más ya que lee de a 4 bytes

                        // en resumen, el (float*) castea el (ptr + offset) para que sea un puntero, y el * del inicio (*(float*)...)
                        // como que lo convierte para que sea directamente el valor otra vez, ya que *ptr es igual a el valor del pointer
                        float value = *(float*)(ptr + offset);
                        if (filter(value)) 
                        {
                            result[addr.Key] = value;
                        }
                        else if (addr.Key == (IntPtr)0x6B832B48)
                        {
                            return 1111111111;
                        }
                    }
                }
            }
            ptrs = new Dictionary<IntPtr, float>(result);
            return ogSize - ptrs.Count;
        }


        public unsafe int CompareFilterValues(ref Dictionary<IntPtr, float> ptrs, Func<float, float, bool> filter, int chunkSize = 0x1000) // 4KB default
        {
            Dictionary<IntPtr, float> result = new Dictionary<IntPtr, float>();
            int ogSize = ptrs.Count;

            foreach (var group in ptrs.GroupBy(addr => addr.Key.ToInt64() / chunkSize)) // Agrupar por bloque de memoria
            {
                long baseAddr = group.Key * chunkSize;
                byte[] buffer = new byte[chunkSize];

                if (!ReadProcessMemory(_proc.Handle, (IntPtr)baseAddr, buffer, chunkSize, out _)) continue;

                fixed (byte* ptr = buffer)
                {
                    foreach (var addr in group)
                    {
                        int offset = (int)(addr.Key.ToInt64() - baseAddr);

                        if (offset < 0 || offset > chunkSize - sizeof(float)) continue; // Seguridad, se fija que no lea cosas de más ya que lee de a 4 bytes

                        // en resumen, el (float*) castea el (ptr + offset) para que sea un puntero, y el * del inicio (*(float*)...)
                        // como que lo convierte para que sea directamente el valor otra vez, ya que *ptr es igual a el valor del pointer
                        float newValue = *(float*)(ptr + offset);
                        if (filter(addr.Value, newValue)) 
                        {
                            result.Add(addr.Key, newValue);
                        }
                        else if (addr.Key == (IntPtr)0x6B832B48)
                        {
                            return 333333333;
                        }
                    }
                }
            }
            ptrs = new Dictionary<IntPtr, float>(result);
            return ogSize - ptrs.Count;
        }

        public unsafe int GetValuesDeltas(Dictionary<IntPtr, float> ptrs, ref Dictionary<IntPtr, List<float>> history, int chunkSize = 0x1000) // 4KB default
        {
            foreach (var group in ptrs.GroupBy(addr => addr.Key.ToInt64() / chunkSize)) // Agrupar por bloque de memoria
            {
                long baseAddr = group.Key * chunkSize;
                byte[] buffer = new byte[chunkSize];

                if (!ReadProcessMemory(_proc.Handle, (IntPtr) baseAddr, buffer, chunkSize, out _)) continue;

                fixed (byte* ptr = buffer)
                {
                    foreach (var addr in group)
                    {
                        int offset = (int)(addr.Key.ToInt64() - baseAddr);

                        if (offset < 0 || offset > chunkSize - sizeof(float)) continue; // Seguridad, se fija que no lea cosas de más ya que lee de a 4 bytes

                        // fixea el delta por si es mas o menos de 360 para que no se ponga en negativo ni se pase, ni cosas raras lol
                        float delta = *(float*)(ptr + offset) - addr.Value;
                        if (delta > 180) { delta -= 360; }
                        else if (delta < -180) { delta += 360; }

                        // ahora si, si el pointer no existe lo agrega y le pone el delta. si existe solo le pone el delta y ya
                        if (history.ContainsKey(addr.Key)) { history[addr.Key].Add(delta); }
                    }
                }
            }
            return history.Count;
        }

        /////////////////////////////////// READ & WRITE //////////////////////////////////////

        public byte[] ReadBytes(IntPtr ptr, int bytes)
        {
            byte[] buffer = new byte[bytes];
            ReadProcessMemory(_proc.Handle, ptr, buffer, bytes, out _);
            return buffer;
        }

        public unsafe float ReadFloat(IntPtr address)
        {
            byte[] buffer = new byte[4];

            ReadProcessMemory(_proc.Handle, address, buffer, 4, out _);

            fixed (byte* ptr = buffer)
            {
                return *(float*)ptr;
            }
        }


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
