﻿using System.Runtime.InteropServices;

namespace Broken.Entities.Memory
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MemoryMoney
    {
        [FieldOffset(0x8)] 
        public int Money;

        [FieldOffset(0xC)] 
        public int Protection;
    }
}