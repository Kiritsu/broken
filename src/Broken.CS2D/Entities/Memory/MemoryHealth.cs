using System.Runtime.InteropServices;

namespace Broken.CS2D.Entities.Memory
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MemoryHealth
    {
        [FieldOffset(0x8)] 
        public int Health;

        [FieldOffset(0xC)] 
        public int Protection;

        public int RealHealth => Health + Protection;
    }
}