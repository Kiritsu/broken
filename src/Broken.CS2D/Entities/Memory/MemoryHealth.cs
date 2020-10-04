using System.Runtime.InteropServices;

namespace Broken.Entities.Memory
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MemoryHealth
    {
        [FieldOffset(0x8)] 
        public int Health;

        [FieldOffset(0xC)] 
        public int Protection;
    }
}