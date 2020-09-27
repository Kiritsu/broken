using System;
using System.Diagnostics;
using System.Linq;
using Broken.Entities;
using Broken.Entities.Memory;
using Broken.Enums;

namespace Broken.CS2D
{
    public class Program
    {
        public static void Main()
        {
            var process = Process.GetProcessesByName("CS2D").First();
            var handle = process.Handle;

            PatchEncryption(handle);
            FixEncryptedPointers(handle, 100, 16000);
            var localPlayer = GetLocalPlayer(handle);
        }

        /// <summary>
        ///     Gets the <see cref="LocalPlayer"/> from memory.
        /// </summary>
        /// <param name="handle">Handle of the game.</param>
        public static LocalPlayer GetLocalPlayer(IntPtr handle)
        {
            var localPlayerPointer = MemoryHelpers.ReadInt32(handle, new IntPtr(Offsets.GameBase + Offsets.LocalPlayerOffset));
            var localPlayer = MemoryHelpers.Read<MemoryLocalPlayer>(handle, new IntPtr(localPlayerPointer));
            var weapon = MemoryHelpers.Read<MemoryPlayerWeapon>(handle, new IntPtr(localPlayer.WeaponPointer));
            var money = MemoryHelpers.Read<MemoryMoney>(handle, new IntPtr(localPlayer.MoneyPointer));
            var health = MemoryHelpers.Read<MemoryHealth>(handle, new IntPtr(localPlayer.HealthPointer));

            return new LocalPlayer
            {
                PlayerId = localPlayer.PlayerId,
                Team = localPlayer.Team,
                PositionX = localPlayer.PositionX,
                PositionY = localPlayer.PositionY,
                MouseRotation = localPlayer.MouseRotation,
                Weapon = new PlayerWeapon
                {
                    WeaponId = weapon.WeaponId,
                    MaxAmmo = weapon.MaxAmmo,
                    CurrentAmmo = weapon.CurrentAmmo,
                    WeaponMode = weapon.WeaponMode
                },
                Health = new PlayerHealth
                {
                    Value = health.Health
                },
                Money = new PlayerMoney
                {
                    Value = money.Money
                }
            };
        }

        /// <summary>
        ///     Patches encryption algorithm. Used for health and money.
        /// </summary>
        /// <param name="handle">Handle of the game.</param>
        public static void PatchEncryption(IntPtr handle)
        {
            var address = new IntPtr(Offsets.GameBase + Offsets.EncryptionOffset);
            var sizeofByte = new UIntPtr(sizeof(byte));
            MemoryHelpers.VirtualProtectEx(handle, address, sizeofByte, VirtualProtection.PageReadWrite, out var oldProtection);

            // push ebp -> ret 
            MemoryHelpers.WriteByte(handle, address, 0xC3);
            MemoryHelpers.VirtualProtectEx(handle, address, sizeofByte, oldProtection, out oldProtection);

            address = new IntPtr(Offsets.GameBase + Offsets.AntiCrashOffset);
            MemoryHelpers.VirtualProtectEx(handle, address, sizeofByte, VirtualProtection.PageReadWrite, out oldProtection);

            // je 005C1D16 -> jmp 005C1D16
            MemoryHelpers.WriteByte(handle, address, 0xEB);
            MemoryHelpers.VirtualProtectEx(handle, address, sizeofByte, oldProtection, out oldProtection);
        }

        /// <summary>
        ///     Fixes the "encrypted" pointers so it allows us to change the value of health and money.
        /// </summary>
        /// <param name="handle">Handle of the game.</param>
        /// <param name="health">Health to set.</param>
        /// <param name="money">Money to set.</param>
        public static void FixEncryptedPointers(IntPtr handle, int health, int money)
        {
            var localPlayerPointer = MemoryHelpers.ReadInt32(handle, new IntPtr(Offsets.GameBase + Offsets.LocalPlayerOffset));
            var localPlayer = MemoryHelpers.Read<MemoryLocalPlayer>(handle, new IntPtr(localPlayerPointer));
            
            MemoryHelpers.WriteInt32(handle, new IntPtr(localPlayer.HealthPointer + Offsets.ProtectionOffset), 0);
            MemoryHelpers.WriteInt32(handle, new IntPtr(localPlayer.HealthPointer + Offsets.ValueOffset), health);
            MemoryHelpers.WriteInt32(handle, new IntPtr(localPlayer.MoneyPointer + Offsets.ProtectionOffset), 0);
            MemoryHelpers.WriteInt32(handle, new IntPtr(localPlayer.MoneyPointer + Offsets.ValueOffset), money);
        }
    }
}