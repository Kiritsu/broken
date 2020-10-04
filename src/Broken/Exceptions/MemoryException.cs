using System;

namespace Broken.Exceptions
{
    public abstract class MemoryException : Exception
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
            : this("A memory exception happened. See the child exception for more information.", 
                processHandle, baseAddress)
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