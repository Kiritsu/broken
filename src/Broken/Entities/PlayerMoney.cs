using System.Runtime.InteropServices;

namespace Broken.Entities
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerMoney
    {
        [FieldOffset(0x8)] 
        public int EncryptedValue;

        [FieldOffset(0xC)] 
        public int Protection;
        
        public int Money => EncryptedValue + Protection;
    }
}