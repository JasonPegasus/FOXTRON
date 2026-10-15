using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static FX_UnsafeMemory.Memory;

namespace FX_UnsafeMemory
{
    public class Memory
    {
        #region -- IMPORTS --

        [DllImport("kernel32.dll")] static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("kernel32.dll")] static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")] static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize);

        [DllImport("kernel32.dll")] static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")] static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize);

        #endregion

        #region -- PROCESS & ACCESSES --

        Process _proc;

        public Memory(Process process) { _proc = process; }

        public bool isProcessValid() { return _proc != null && !_proc.HasExited; }

        public int MEMORY_PAGE_SIZE = 0x10000;

        #endregion

        #region -- SCANNING & SEARCHING --

        public IntPtr temp_CamAddress;

        public struct DATA { IntPtr ptr; float value; }

        public unsafe List<TPointer<float>> ScanFloatFiltered(Predicate<float> filter)
        {
            List<TPointer<float>> results = new();

            IntPtr address = IntPtr.Zero;
            byte[] buffer = new byte[MEMORY_PAGE_SIZE];        

            while (true)
            {
                MEMORY_BASIC_INFORMATION m;
                if (VirtualQueryEx(_proc.Handle, address, out m, (uint)sizeof(MEMORY_BASIC_INFORMATION)) == 0)
                    break;

                const uint MEM_COMMIT = 0x1000, PAGE_NOACCESS = 0x01, PAGE_GUARD = 0x100;
                if (m.State == MEM_COMMIT && (m.Protect & (PAGE_NOACCESS | PAGE_GUARD)) == 0 && m.Protect != 0)
                {
                    nint baseAddr = m.BaseAddress;
                    nint regionSize = (nint)m.RegionSize;

                    nint next = baseAddr + regionSize;
                    if (next <= address.ToInt64()) break;
                    address = new IntPtr(next);

                    for (nint offset = 0; offset < regionSize; offset += MEMORY_PAGE_SIZE)
                    {
                        int toRead = (int)Math.Min(MEMORY_PAGE_SIZE, regionSize - offset);

                        if (ReadProcessMemory(_proc.Handle, (IntPtr)(baseAddr + offset), buffer, toRead, out _))
                        {
                            fixed (byte* ptr = buffer)
                            {
                                for (int i = 0; i <= toRead - 4; i += 4)
                                {
                                    float value = *(float*)(ptr + i);

                                    if (filter(value))
                                    {
                                        results.Add(new TPointer<float>(baseAddr + offset + i, value));
                                    }
                                }
                            }
                        }
                    }
                }

                address = new IntPtr(m.BaseAddress.ToInt64() + (nint)m.RegionSize);
            }

            return results;
        }

        private unsafe bool TryReadFloat(nint address, byte[] tmp, out float value)
        {
            value = 0;
            if (!ReadProcessMemory(_proc.Handle, (IntPtr)address, tmp, sizeof(float), out _)) return false;
            fixed (byte* p = tmp) { value = *(float*)p; }
            return true;
        }

        public unsafe int FilterValues(List<TPointer<float>> ptrs, Predicate<float> filter)
        {
            int ogSize = ptrs.Count;
            Span<TPointer<float>> span = CollectionsMarshal.AsSpan(ptrs);           
            int write = 0;                    

            byte[] buffer = ArrayPool<byte>.Shared.Rent(MEMORY_PAGE_SIZE);
            byte[] tmp = new byte[sizeof(float)];

            try      
            {
                nint currentPage = -1;
                bool pageOk = false;          

                fixed (byte* bufferPtr = buffer)
                {
                    for (int read = 0; read < span.Length; read++)
                    {
                        TPointer<float> p = span[read];
                        nint page = p.Address / MEMORY_PAGE_SIZE;

                        if (page != currentPage)            
                        {
                            currentPage = page;
                            pageOk = ReadProcessMemory(_proc.Handle, (IntPtr)(page * MEMORY_PAGE_SIZE), buffer, MEMORY_PAGE_SIZE, out _);
                        }

                        float value;
                        if (pageOk)
                        {
                            int offset = (int)(p.Address - page * MEMORY_PAGE_SIZE);
                            if (offset < 0 || offset > MEMORY_PAGE_SIZE - sizeof(float)) continue;       
                            value = *(float*)(bufferPtr + offset);
                        }
                        else if (!TryReadFloat(p.Address, tmp, out value))
                        {
                            continue;         
                        }

                        if (filter(value))
                        {
                            span[write++] = new TPointer<float>(p.Address, value);
                        }
                    }
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }

            ptrs.RemoveRange(write, ptrs.Count - write);       
            return ogSize - ptrs.Count;
        }


        public unsafe int CompareFilterValues(List<TPointer<float>> ptrs, Func<float, float, bool> filter)
        {
            int ogSize = ptrs.Count;
            Span<TPointer<float>> span = CollectionsMarshal.AsSpan(ptrs);
            int write = 0;

            byte[] buffer = ArrayPool<byte>.Shared.Rent(MEMORY_PAGE_SIZE);
            byte[] tmp = new byte[sizeof(float)];

            try
            {
                nint currentPage = -1;
                bool pageOk = false;

                fixed (byte* bufferPtr = buffer)
                {
                    for (int read = 0; read < span.Length; read++)
                    {
                        TPointer<float> p = span[read];
                        nint page = p.Address / MEMORY_PAGE_SIZE;

                        if (page != currentPage)
                        {
                            currentPage = page;
                            pageOk = ReadProcessMemory(_proc.Handle, (IntPtr)(page * MEMORY_PAGE_SIZE), buffer, MEMORY_PAGE_SIZE, out _);
                        }

                        float newValue;
                        if (pageOk)
                        {
                            int offset = (int)(p.Address - page * MEMORY_PAGE_SIZE);
                            if (offset < 0 || offset > MEMORY_PAGE_SIZE - sizeof(float)) continue;
                            newValue = *(float*)(bufferPtr + offset);
                        }
                        else if (!TryReadFloat(p.Address, tmp, out newValue))
                        {
                            continue;
                        }

                        if (filter(newValue, p.Value))      
                        {
                            span[write++] = new TPointer<float>(p.Address, newValue);
                        }
                    }
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }

            ptrs.RemoveRange(write, ptrs.Count - write);
            return ogSize - ptrs.Count;
        }

        public unsafe int GetValuesDeltas(List<TPointer<float>> ptrs, ref Dictionary<IntPtr, List<float>> history)
        {
            byte[] buffer = new byte[MEMORY_PAGE_SIZE];

            foreach (var group in ptrs.GroupBy(p => p.Address / MEMORY_PAGE_SIZE))      
            {
                nint baseAddr = group.Key * MEMORY_PAGE_SIZE;

                if (!ReadProcessMemory(_proc.Handle, (IntPtr)baseAddr, buffer, MEMORY_PAGE_SIZE, out _)) continue;

                fixed (byte* ptr = buffer)
                {
                    foreach (var p in group)
                    {
                        int offset = (int)(p.Address - baseAddr);

                        if (offset < 0 || offset > MEMORY_PAGE_SIZE - sizeof(float)) continue;

                        float delta = *(float*)(ptr + offset) - p.Value;
                        if (delta > 180) { delta -= 360; }
                        else if (delta < -180) { delta += 360; }

                        if (history.ContainsKey(p.Address)) { history[p.Address].Add(delta); }
                    }
                }
            }
            return history.Count;
        }

        #endregion

        #region -- READ & WRITE --

        public unsafe float ReadFloat(IntPtr address)
        {
            byte[] buffer = new byte[4];

            ReadProcessMemory(_proc.Handle, address, buffer, 4, out _);

            fixed (byte* ptr = buffer)
            { return *(float*)ptr; }
        }

        public unsafe bool WriteFloat(IntPtr address, float value)
        {
            byte[] buffer = new byte[4];

            fixed (byte* ptr = buffer)
            { *(float*)ptr = value; }

            return WriteProcessMemory(_proc.Handle, address, buffer, 4, out _);
        }

        public unsafe float DoFloat(IntPtr address, Func<float, float> f)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(_proc.Handle, address, buffer, 4, out _);

            float done;
            fixed (byte* ptr = buffer)
            {
                done = f(*(float*)ptr);
                *(float*)ptr = done;
            }

            WriteProcessMemory(_proc.Handle, address, buffer, 4, out _);
            return done;
        }

        public unsafe T ReadGeneric<T>(IntPtr address)
        {
            int size = sizeof(T);
            byte[] buffer = new byte[size];

            ReadProcessMemory(_proc.Handle, address, buffer, size, out _);

            fixed (byte* ptr = buffer)
            { return *(T*)ptr; }
        }

        #endregion

        #region -- OTHER --

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

        #endregion
    }
}
