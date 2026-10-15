

        public unsafe int FilterValues(Dictionary<IntPtr, float> ptrs, Predicate<float> filter)
        {
            int ogSize = ptrs.Count;
            var toRemove = new List<IntPtr>();

            byte[] buffer = ArrayPool<byte>.Shared.Rent(MEMORY_PAGE_SIZE); // crea un array a partir de uno que
                                                                           // ya existia en el heap, para evitar
                                                                           // llenar la memoria
            try // Por si explota el unsafe
            {
                nint currentPage = -1; 
                bool pageOk = false; // Estos dos, setean en que pagina de memoria está, y si esta OK o esta corrupta

                var keys = ptrs.Keys.OrderBy(k => k.ToInt64()).ToArray(); // Esto ordena los pointers
                                                                          // por su direccion, asi estan
                                                                          // en orden a la memoria

                fixed (byte* bufferPtr = buffer) 
                {
                    foreach (var key in keys)
                    {
                        nint addr = key; 
                        nint page = addr / MEMORY_PAGE_SIZE; // Estos dos, setean el address y la página de la misma

                        if (page != currentPage) // Si el puntero actual ya está en otra página, lee la pagina nueva.
                        {
                            currentPage = page;
                            pageOk = ReadProcessMemory(_proc.Handle, (IntPtr)(page * MEMORY_PAGE_SIZE), buffer, MEMORY_PAGE_SIZE, out _);
                        }

                        if (!pageOk) { toRemove.Add(key); continue; } // Si no pudo leer la page con ReadProcessMemory, la marca como mala y la skipea

                        int offset = (int)(addr - page * MEMORY_PAGE_SIZE); // Esto te dá el offset del pointer dentro de cada pagina.
                                                                            // Osea, offset = pointer - (nº de pagina * tamaño de pagina),
                                                                            // asi basicamente te da un numero del 0 al 4096 si la pagina es de 4 KB

                        if (offset < 0 || offset > MEMORY_PAGE_SIZE - sizeof(float)) { toRemove.Add(key); continue; } // Simple seguridad para que el
                                                                                                                      // ptr no se salga de la pagina
                        
                        float value = *(float*)(bufferPtr + offset); // Valor = puntero a ptr + offset, y obtener valor float
                        if (filter(value)) ptrs[key] = value;
                        else toRemove.Add(key);
                    }
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer); // Le dice al programa que devuelva el array al array pool para usar luego
            }

            foreach (var k in toRemove) ptrs.Remove(k);
            return ogSize - ptrs.Count;
        }


        public unsafe int CompareFilterValues(Dictionary<IntPtr, float> ptrs, Func<float, float, bool> filter)
        {
            int ogSize = ptrs.Count;
            var toRemove = new List<IntPtr>();
            byte[] buffer = ArrayPool<byte>.Shared.Rent(MEMORY_PAGE_SIZE);
            try
            {
                nint currentPage = -1;
                bool pageOk = false;
                var keys = ptrs.Keys.OrderBy(k => k.ToInt64()).ToArray();
                fixed (byte* bufferPtr = buffer)
                {
                    foreach (var key in keys)
                    {
                        nint addr = key;
                        nint page = addr / MEMORY_PAGE_SIZE;
                        if (page != currentPage)
                        {
                            currentPage = page;
                            pageOk = ReadProcessMemory(_proc.Handle, (IntPtr)(page * MEMORY_PAGE_SIZE), buffer, MEMORY_PAGE_SIZE, out _);
                        }
                        if (!pageOk) { toRemove.Add(key); continue; }
                        int offset = (int)(addr - page * MEMORY_PAGE_SIZE);
                        if (offset < 0 || offset > MEMORY_PAGE_SIZE - sizeof(float)) { toRemove.Add(key); continue; }
                        float newValue = *(float*)(bufferPtr + offset);
                        if (filter(newValue, ptrs[key])) ptrs[key] = newValue;
                        else toRemove.Add(key);
                    }
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
            foreach (var k in toRemove) ptrs.Remove(k);
            return ogSize - ptrs.Count;
        }