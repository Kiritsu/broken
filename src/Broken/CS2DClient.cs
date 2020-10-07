using System;
using System.Diagnostics;
using Broken.CS2D.Entities.Memory;

namespace Broken.CS2D
{
    public unsafe class CS2DClient
    {
        /// <summary>
        ///     Gets the process of CS2D tied to this instance.
        /// </summary>
        public Process CS2D { get; }
        
        /// <summary>
        ///     Gets the handle of the <see cref="Process"/>.
        /// </summary>
        public IntPtr Handle => CS2D.Handle;

        /// <summary>
        ///     Gets the local player.
        /// </summary>
        public LocalPlayer LocalPlayer => *(LocalPlayer*)(Offsets.GameBase + Offsets.LocalPlayerOffset);

        /// <summary>
        ///     Creates a new <see cref="CS2DClient"/> depending on the given process.
        /// </summary>
        /// <param name="process">Process to bind to this instance.</param>
        public CS2DClient()
        {
            CS2D = Process.GetCurrentProcess();
        }

        /// <summary>
        ///     Patches encryption algorithm. Used for health and money. Facultative.
        /// </summary>
        public void PatchEncryption()
        {
            // push ebp -> ret 
            *(int*)(Offsets.GameBase + Offsets.EncryptionOffset) = 0xC3;
            // je 005C1D16 -> jmp 005C1D16
            *(int*)(Offsets.GameBase + Offsets.AntiCrashOffset) = 0xEB;
        }

        /// <summary>
        ///     Fixes the "encrypted" pointers so it allows us to change the value of health and money. Facultative.
        /// </summary>
        /// <param name="health">Health to set.</param>
        /// <param name="money">Money to set.</param>
        public void FixEncryptedPointers(int health = 100, int money = 16000)
        {
            var localPlayer = *(LocalPlayer*)(Offsets.GameBase + Offsets.LocalPlayerOffset);

            *(int*)(localPlayer.HealthPointer + Offsets.ProtectionOffset) = 0;
            *(int*)(localPlayer.HealthPointer + Offsets.ValueOffset) = health;
            *(int*)(localPlayer.MoneyPointer + Offsets.ProtectionOffset) = 0;
            *(int*)(localPlayer.MoneyPointer + Offsets.ValueOffset) = money;
        }

        /// <summary>
        ///     Enables or disables the flash hack.
        /// </summary>
        /// <param name="enable">Whether to enable or disable the flash hack.</param>
        public void ToggleFlashHack(bool enable)
        {
            *(int*)(Offsets.GameBase + Offsets.FlashHackOffset) = enable ? 0x0 : 0x1;
        }
    }
}