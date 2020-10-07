using System.Runtime.InteropServices;

namespace Broken.CS2D.Entities.Memory
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerHealth
    {
        [FieldOffset(0x8)] 
        public int EncryptedValue;

        [FieldOffset(0xC)] 
        public int Protection;

        public int Health => EncryptedValue + Protection;
    }
}