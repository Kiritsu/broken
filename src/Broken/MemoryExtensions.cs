using System;

namespace Broken
{
    public unsafe static class MemoryExtensions
    {
        /// <summary>
        ///     Gets a string from its address.
        /// </summary>
        /// <param name="ptr">Address of the string.</param>
        /// <remarks>
        ///     Value at +0x8 gives the length of the string.
        ///     Value at +0xC is the first character of the string.
        /// </remarks>
        public static string GetString(this int ptr)
        {
            return new Span<char>((char*)(ptr + 0xC), *(int*)(ptr + 0x8)).ToString();
        }
    }
}
