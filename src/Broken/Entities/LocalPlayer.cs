using System.Runtime.InteropServices;
using Broken.Enums;

namespace Broken.Entities
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct LocalPlayer
    {
        [FieldOffset(0x8)]
        public int PlayerId;
        
        [FieldOffset(0x1C4)] 
        public PlayerTeam Team;
        
        [FieldOffset(0x1D4)] 
        public float PositionX;
        
        [FieldOffset(0x1D8)] 
        public float PositionY;
        
        [FieldOffset(0x1DC)] 
        public float MouseRotation;
        
        [FieldOffset(0x1F4)] 
        public int HealthPointer;
        public PlayerHealth Health => *(PlayerHealth*)HealthPointer;

        [FieldOffset(0x200)]
        public int MoneyPointer;
        public PlayerMoney Money => *(PlayerMoney*)MoneyPointer;

        [FieldOffset(0x24C)]
        public int WeaponPointer;
        public PlayerWeapon Weapon => *(PlayerWeapon*)WeaponPointer;
    }
}