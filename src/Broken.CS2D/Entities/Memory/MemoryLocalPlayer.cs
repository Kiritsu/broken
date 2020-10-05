using System.Runtime.InteropServices;
using Broken.CS2D.Enums;

namespace Broken.CS2D.Entities.Memory
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MemoryLocalPlayer
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

        [FieldOffset(0x200)] 
        public int MoneyPointer;
        
        [FieldOffset(0x24C)] 
        public int WeaponPointer;
    }
}