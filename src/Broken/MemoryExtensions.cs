using System;

namespace Broken
{
    public unsafe static class MemoryExtensions
    {
        public static string GetString(this int ptr)
        {
            return new Span<char>((char*)(ptr + 0xC), *(int*)(ptr + 0x8)).ToString();
        }
    }
}
