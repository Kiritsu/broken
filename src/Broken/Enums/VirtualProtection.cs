namespace Broken.Enums
{
    public enum VirtualProtection
    {
        PageNoAccess = 0x01,
        PageReadOnly = 0x02,
        PageReadWrite = 0x04,
        PageWriteCopy = 0x08,
        PageExecute = 0x10,
        PageExecuteRead = 0x20,
        PageExecuteReadWrite = 0x40,
        PageExecuteWriteCopy = 0x80,
        PageGuard = 0x100,
        PageNoCache = 0x200,
        PageWriteCombine = 0x400
    }
}