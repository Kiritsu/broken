﻿using System;
using System.Diagnostics;
using Broken.Entities;
using Broken.Entities.Memory;
using Broken.Enums;

namespace Broken.CS2D
{
    public class CS2DClient
    {
        /// <summary>
        ///     Process of CS2D tied to this instance.
        /// </summary>
        public Process CS2D { get; }
        
        /// <summary>
        ///     Handle of the <see cref="Process"/> <see cref="CS2D"/>.
        /// </summary>
        public IntPtr Handle => CS2D.Handle;
        
        /// <summary>
        ///     Creates a new <see cref="CS2DClient"/> depending on the given process.
        /// </summary>
        /// <param name="process">Process to bind to this instance.</param>
        public CS2DClient(Process process)
        {
            CS2D = process;
        }
        
        /// <summary>
        ///     Gets the <see cref="LocalPlayer"/> from memory.
        /// </summary>
        public LocalPlayer GetLocalPlayer()
        {
            var localPlayerPointer = MemoryHelpers.ReadInt32(Handle, new IntPtr(Offsets.GameBase + Offsets.LocalPlayerOffset));
            var localPlayer = MemoryHelpers.Read<MemoryLocalPlayer>(Handle, new IntPtr(localPlayerPointer));
            var weapon = MemoryHelpers.Read<MemoryPlayerWeapon>(Handle, new IntPtr(localPlayer.WeaponPointer));
            var money = MemoryHelpers.Read<MemoryMoney>(Handle, new IntPtr(localPlayer.MoneyPointer));
            var health = MemoryHelpers.Read<MemoryHealth>(Handle, new IntPtr(localPlayer.HealthPointer));

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
        public void PatchEncryption()
        {
            var address = new IntPtr(Offsets.GameBase + Offsets.EncryptionOffset);
            var sizeofByte = new UIntPtr(sizeof(byte));
            MemoryHelpers.VirtualProtectEx(Handle, address, sizeofByte, VirtualProtection.PageReadWrite, out var oldProtection);

            // push ebp -> ret 
            MemoryHelpers.WriteByte(Handle, address, 0xC3);
            MemoryHelpers.VirtualProtectEx(Handle, address, sizeofByte, oldProtection, out oldProtection);

            address = new IntPtr(Offsets.GameBase + Offsets.AntiCrashOffset);
            MemoryHelpers.VirtualProtectEx(Handle, address, sizeofByte, VirtualProtection.PageReadWrite, out oldProtection);

            // je 005C1D16 -> jmp 005C1D16
            MemoryHelpers.WriteByte(Handle, address, 0xEB);
            MemoryHelpers.VirtualProtectEx(Handle, address, sizeofByte, oldProtection, out oldProtection);
        }

        /// <summary>
        ///     Fixes the "encrypted" pointers so it allows us to change the value of health and money.
        /// </summary>
        /// <param name="health">Health to set.</param>
        /// <param name="money">Money to set.</param>
        public void FixEncryptedPointers(int health = 100, int money = 16000)
        {
            var localPlayerPointer = MemoryHelpers.ReadInt32(Handle, new IntPtr(Offsets.GameBase + Offsets.LocalPlayerOffset));
            var localPlayer = MemoryHelpers.Read<MemoryLocalPlayer>(Handle, new IntPtr(localPlayerPointer));
            
            MemoryHelpers.WriteInt32(Handle, new IntPtr(localPlayer.HealthPointer + Offsets.ProtectionOffset), 0);
            MemoryHelpers.WriteInt32(Handle, new IntPtr(localPlayer.HealthPointer + Offsets.ValueOffset), health);
            MemoryHelpers.WriteInt32(Handle, new IntPtr(localPlayer.MoneyPointer + Offsets.ProtectionOffset), 0);
            MemoryHelpers.WriteInt32(Handle, new IntPtr(localPlayer.MoneyPointer + Offsets.ValueOffset), money);
        }
    }
}