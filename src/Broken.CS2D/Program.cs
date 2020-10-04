using System.Diagnostics;
using System.Linq;

namespace Broken.CS2D
{
    public class Program
    {
        public static void Main()
        {
            var process = Process.GetProcessesByName("CS2D").First();
            var client = new CS2DClient(process);
            client.PatchEncryption();
            client.FixEncryptedPointers();
            var localPlayer = client.GetLocalPlayer();
        }
    }
}