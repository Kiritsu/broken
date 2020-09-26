using System;

namespace Broken.Exceptions
{
    public class MemoryWriteException : MemoryException
    {
        /// <summary>
        ///     Gets the value that was trying to be written to the memory.
        /// </summary>
        public object? Value { get; init; }
        
        /// <summary>
        ///     Gets the size of the value.
        /// </summary>
        public IntPtr Size { get; init; }
        
        public MemoryWriteException(IntPtr processHandle, IntPtr baseAddress) 
            : this("An error occured when trying to write memory.", processHandle, baseAddress)
        {
        }
        
        public MemoryWriteException(string exception, IntPtr processHandle, IntPtr baseAddress) 
            : base(exception, processHandle, baseAddress)
        {
        }
    }
}