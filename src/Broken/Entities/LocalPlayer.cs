using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Broken.Enums;

namespace Broken.Entities
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct LocalPlayer
    {
        [FieldOffset(0x8)]
        public int PlayerId;

        [FieldOffset(0xC)]
        public int PlayerNamePtr;
        public string PlayerName => PlayerNamePtr.GetString();
        
        [FieldOffset(0x1C4)] 
        public PlayerTeam Team;
        
        [FieldOffset(0x1D4)] 
        public Vector2 Position;
       
        [FieldOffset(0x1DC)] 
        public float MouseRotation;
        
        [FieldOffset(0x1F4)] 
        public int HealthPointer;
        public EncryptedStruct* Health => (EncryptedStruct*)HealthPointer;

        [FieldOffset(0x200)]
        public int MoneyPointer;
        public EncryptedStruct* Money => (EncryptedStruct*)MoneyPointer;

        [FieldOffset(0x24C)]
        public int WeaponPointer;
        public PlayerWeapon* Weapon => (PlayerWeapon*)WeaponPointer;

        [FieldOffset(0x264)]
        public bool LampEnabled;
    }
}