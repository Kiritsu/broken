﻿using System.Runtime.InteropServices;
using Broken.CS2D.Enums;

namespace Broken.CS2D.Entities.Memory
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MemoryPlayerWeapon
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