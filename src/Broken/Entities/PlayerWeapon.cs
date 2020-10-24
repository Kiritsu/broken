using System.Runtime.InteropServices;
using Broken.Enums;

namespace Broken.Entities
{
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerWeapon
    {
        /// <summary>
        ///     Gets the id of the weapon.
        /// </summary>
        [FieldOffset(0xC)] 
        public Weapon WeaponId;
        
        /// <summary>
        ///     Gets the max available amount of ammo.
        /// </summary>
        [FieldOffset(0x14)] 
        public int MaxAmmo;
        
        /// <summary>
        ///     Gets the current loaded amount of ammo.
        /// </summary>
        [FieldOffset(0x18)] 
        public int CurrentAmmo;

        /// <summary>
        ///     Gets the mode of the weapon: scope, silencer, etc.
        /// </summary>
        [FieldOffset(0x1C)]
        public int WeaponMode;
    }
}