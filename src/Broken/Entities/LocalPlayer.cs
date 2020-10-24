using System.Numerics;
using System.Runtime.InteropServices;
using Broken.Enums;

namespace Broken.Entities
{
    /// <summary>
    ///     Represents any kind of player in a CS2D game.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct LocalPlayer
    {
        /// <summary>
        ///     Gets the id of the user in the game.
        /// </summary>
        [FieldOffset(0x8)]
        public int Id;

        /// <summary>
        ///     Gets the address to the player's name.
        /// </summary>
        [FieldOffset(0xC)]
        public int NamePtr;

        /// <summary>
        ///     Gets the player's name.
        /// </summary>
        public string Name => NamePtr.GetString();
        
        /// <summary>
        ///     Gets the player's team.
        /// </summary>
        [FieldOffset(0x1C4)] 
        public PlayerTeam Team;
        
        /// <summary>
        ///     Gets the player's position.
        /// </summary>
        [FieldOffset(0x1D4)] 
        public Vector2 Position;
       
        /// <summary>
        ///     Gets the player's mouse rotation.
        /// </summary>
        [FieldOffset(0x1DC)] 
        public float MouseRotation;
        
        /// <summary>
        ///     Gets the address to the player's health.
        /// </summary>
        [FieldOffset(0x1F4)] 
        public int HealthPointer;

        /// <summary>
        ///     Gets the player's health.
        /// </summary>
        public EncryptedStruct* Health => (EncryptedStruct*)HealthPointer;

        /// <summary>
        ///     Gets the address to the player's money.
        /// </summary>
        [FieldOffset(0x200)]
        public int MoneyPointer;

        /// <summary>
        ///     Gets the player's money.
        /// </summary>
        public EncryptedStruct* Money => (EncryptedStruct*)MoneyPointer;

        /// <summary>
        ///     Gets the address to the player's current weapon.
        /// </summary>
        [FieldOffset(0x24C)]
        public int WeaponPointer;

        /// <summary>
        ///     Gets the player's current weapon.
        /// </summary>
        public PlayerWeapon* Weapon => (PlayerWeapon*)WeaponPointer;

        /// <summary>
        ///     Gets whether player's lamp is enabled.
        /// </summary>
        [FieldOffset(0x264)]
        public bool LampEnabled;
    }
}