using System;

namespace Broken.Exceptions
{
    public class MemoryException : Exception
    {
        /// <summary>
        ///     Gets the handle of the process that caused this <see cref="MemoryException"/>.
        /// </summary>
        public IntPtr ProcessHandle { get; }
        
        /// <summary>
        ///     Gets the address context that caused this <see cref="MemoryException"/>.
        /// </summary>
        public IntPtr BaseAddress { get; }
        
        public MemoryException(IntPtr processHandle, IntPtr baseAddress) 
            : this("Something wrong happened.", processHandle, baseAddress)
        {
        }
        
        public MemoryException(string exception, IntPtr processHandle, IntPtr baseAddress) 
            : base(exception)
        {
            ProcessHandle = processHandle;
            BaseAddress = baseAddress;
        }
    }
}