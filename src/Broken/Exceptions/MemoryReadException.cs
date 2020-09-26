using System;

namespace Broken.Exceptions
{
    public class MemoryReadException : MemoryException
    {
        /// <summary>
        ///     Gets the type that was trying to be read.
        /// </summary>
        public Type? Type { get; init; }

        public MemoryReadException(IntPtr processHandle, IntPtr baseAddress) 
            : this("An error occured when trying to read memory.", processHandle, baseAddress)
        {
        }
        
        public MemoryReadException(string exception, IntPtr processHandle, IntPtr baseAddress) 
            : base(exception, processHandle, baseAddress)
        {
        }
    }
}