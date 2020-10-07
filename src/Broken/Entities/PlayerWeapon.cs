using System.Runtime.InteropServices;
using Broken.Enums;

namespace Broken.Entities
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerWeapon
    {
        [FieldOffset(0xC)] 
        public Weapon WeaponId;
        
        [FieldOffset(0x14)] 
        public int MaxAmmo;
        
        [FieldOffset(0x18)] 
        public int CurrentAmmo;

        [FieldOffset(0x1C)]
        public int WeaponMode;
    }
}