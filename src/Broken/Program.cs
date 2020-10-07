using System;
using System.Threading;

namespace Broken
{
    public class Program
    {
        public unsafe static void Main(string[] args)
        {
            Console.WriteLine("Hello world from .NET Assembly!");

            var client = new CS2DClient();
            while (true)
            {
                var localPlayer = client.LocalPlayer;
                Console.WriteLine($"X:{localPlayer->Position.X};Y:{localPlayer->Position.Y}");
                Thread.Sleep(10);
            }
        }
    }
}