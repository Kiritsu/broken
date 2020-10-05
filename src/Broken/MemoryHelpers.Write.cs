using System;
using System.Runtime.InteropServices;
using System.Threading;
using Broken.Enums;
using Broken.Exceptions;

namespace Broken
{
    public static partial class MemoryHelpers
    {
        /// <summary>
        ///     Writes a <see cref="Char"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WriteChar(IntPtr hProcess, IntPtr lpBaseAddress, char value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="Byte"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WriteByte(IntPtr hProcess, IntPtr lpBaseAddress, byte value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="Int16"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WriteInt16(IntPtr hProcess, IntPtr lpBaseAddress, short value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="UInt16"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WriteUInt16(IntPtr hProcess, IntPtr lpBaseAddress, ushort value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="Int32"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WriteInt32(IntPtr hProcess, IntPtr lpBaseAddress, int value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="UInt32"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WriteUInt32(IntPtr hProcess, IntPtr lpBaseAddress, uint value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="Int64"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WriteInt64(IntPtr hProcess, IntPtr lpBaseAddress, long value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="UInt64"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WriteUInt64(IntPtr hProcess, IntPtr lpBaseAddress, ulong value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="IntPtr"/> to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static void WritePointer(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr value)
        {
            Write(hProcess, lpBaseAddress, value);
        }

        /// <summary>
        ///     Writes a <see cref="T"/> value to memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <typeparam name="T">Type or struct representing the element to read from the memory.</typeparam>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static unsafe void Write<T>(IntPtr hProcess, IntPtr lpBaseAddress, T value) where T : unmanaged
        {
            var size = sizeof(T);
            var sizePtr = new IntPtr(size);

            if (!WriteProcessMemory(hProcess, lpBaseAddress, &value, sizePtr, out var bytesWritten)
                || bytesWritten != sizePtr)
            {
                throw new MemoryWriteException(hProcess, lpBaseAddress)
                {
                    Size = sizePtr,
                    Value = value
                };
            }
        }

        /// <summary>
        ///     Safely writes a <see cref="T"/> value to memory at the specified address. 
        /// </summary>
        /// <param name="hProcess">Process handle to write to.</param>
        /// <param name="lpBaseAddress">Address to write to.</param>
        /// <param name="value">Value to write.</param>
        /// <typeparam name="T">Type or struct representing the element to read from the memory.</typeparam>
        /// <exception cref="MemoryWriteException">Thrown when writing memory failed.</exception>
        public static unsafe void SafeWrite<T>(IntPtr hProcess, IntPtr lpBaseAddress, T value) where T : unmanaged
        {
            var size = sizeof(T);
            var sizePtr = new UIntPtr((uint)size);

            VirtualProtectEx(hProcess, lpBaseAddress, sizePtr, VirtualProtection.PageExecuteReadWrite, out var oldProtection);
            Write(hProcess, lpBaseAddress, value);
            VirtualProtectEx(hProcess, lpBaseAddress, sizePtr, oldProtection, out _);
        }

        /// <summary>
        ///     Changes the protection of the given address in memory.
        /// </summary>
        /// <param name="hProcess">Process handle to change protection to.</param>
        /// <param name="lpBaseAddress">Address to change protection to.</param>
        /// <param name="dwSize">Size of the region to change protection to.</param>
        /// <param name="protection">New protection to apply.</param>
        /// <param name="oldProtection">Old protection to that given address in memory.</param>
        public static void VirtualProtectEx(IntPtr hProcess, IntPtr lpBaseAddress, UIntPtr dwSize,
            VirtualProtection protection, out VirtualProtection oldProtection)
        {
            VirtualProtectEx(hProcess, lpBaseAddress, dwSize, (uint)protection, out var oldProtectionUint);
            oldProtection = (VirtualProtection) oldProtectionUint;
        }

        [DllImport("kernel32.dll")]
        private static extern unsafe bool WriteProcessMemory(
            IntPtr hProcess, IntPtr lpBaseAddress, void* lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesWritten);
        
        [DllImport("kernel32.dll")]
        private static extern unsafe bool VirtualProtectEx(
            IntPtr hProcess, IntPtr lpBaseAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
    }
}