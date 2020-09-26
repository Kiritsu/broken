using System;
using System.Runtime.InteropServices;
using Broken.Exceptions;

namespace Broken
{
    public static partial class MemoryHelpers
    {
        /// <summary>
        ///     Reads a <see cref="Char"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static char ReadChar(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<char>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="Byte"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static byte ReadByte(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<byte>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="Int16"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static short ReadInt16(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<short>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="UInt16"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static ushort ReadUInt16(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<ushort>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="Int32"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static int ReadInt32(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<int>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="UInt32"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static uint ReadUInt32(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<uint>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="Int64"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static long ReadInt64(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<long>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="UInt64"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static ulong ReadUInt64(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<ulong>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="IntPtr"/> from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static IntPtr ReadPointer(IntPtr hProcess, IntPtr lpBaseAddress)
        {
            return Read<IntPtr>(hProcess, lpBaseAddress);
        }

        /// <summary>
        ///     Reads a <see cref="String"/> from the memory from the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <param name="maxLength">
        ///     Maximum length of the string. Will be truncated to the first \0 found.
        /// </param>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static string ReadString(IntPtr hProcess, IntPtr lpBaseAddress, int maxLength)
        {
            var buffer = ReadChain<char>(hProcess, lpBaseAddress, maxLength);
            return buffer.Slice(0, buffer.IndexOf('\0')).ToString();
        }

        /// <summary>
        ///     Reads <see cref="amount"/> <see cref="T"/> values from the memory from the specified address. 
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <param name="amount">Amount of elements to read.</param>
        /// <typeparam name="T">Type or struct representing the elements to read from the memory.</typeparam>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static unsafe ReadOnlySpan<T> ReadChain<T>(IntPtr hProcess, IntPtr lpBaseAddress, int amount)
            where T : unmanaged
        {
            var values = stackalloc T[amount];
            var size = sizeof(T);
            var sizePtr = new IntPtr(size * amount);

            if (!ReadProcessMemory(hProcess, lpBaseAddress, &values, sizePtr, out var read)
                || read.ToInt32() != size)
            {
                throw new MemoryReadException(hProcess, lpBaseAddress)
                {
                    Type = typeof(T)
                };
            }

            return new Span<T>(values, amount);
        }

        /// <summary>
        ///     Reads a <see cref="T"/> value from the memory at the specified address.
        /// </summary>
        /// <param name="hProcess">Process handle to read from.</param>
        /// <param name="lpBaseAddress">Address to read from.</param>
        /// <typeparam name="T">Type or struct representing the elements to read from the memory.</typeparam>
        /// <exception cref="MemoryReadException">Thrown when reading memory failed.</exception>
        public static unsafe T Read<T>(IntPtr hProcess, IntPtr lpBaseAddress) where T : unmanaged
        {
            var value = default(T);
            var size = sizeof(T);
            var sizePtr = new IntPtr(size);

            if (!ReadProcessMemory(hProcess, lpBaseAddress, &value, sizePtr, out var read)
                || read.ToInt32() != size)
            {
                throw new MemoryReadException(hProcess, lpBaseAddress)
                {
                    Type = typeof(T)
                };
            }

            return value;
        }

        [DllImport("kernel32.dll")]
        private static extern unsafe bool ReadProcessMemory(
            IntPtr hProcess, IntPtr lpBaseAddress, void* lpBuffer, IntPtr dwSize, out IntPtr lpNumberOfBytesRead);
    }
}