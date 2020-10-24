using System.Runtime.InteropServices;

namespace Broken.Entities
{
    [StructLayout(LayoutKind.Explicit)]
    public struct EncryptedStruct
    {
        /// <summary>
        ///     Gets the encrypted value of the structure.
        /// </summary>
        [FieldOffset(0x8)]
        public int EncryptedValue;

        /// <summary>
        ///     Gets the protection value of the structure.
        /// </summary>
        [FieldOffset(0xC)]
        public int Protection;

        /// <summary>
        ///     Gets the real value of the structure.
        /// </summary>
        public int Value => EncryptedValue + Protection;
    }
}
