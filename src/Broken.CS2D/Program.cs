using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Broken.CS2D
{
    public class Program
    {
        public static void Main()
        {
            var process = Process.GetProcessesByName("CS2D").First();
            var client = new CS2DClient(process);
            client.ToggleFlashHack(true);
        }
    }
}