using System;
using System.Threading;

namespace Broken
{
    public class Program
    {
        public unsafe static void Main(string[] args)
        { 
            var client = new CS2DClient();
            while (true)
            {
                var players = client.GetPlayers();
                foreach (var player in players)
                {
                    Console.WriteLine($"Player ID#{player.PlayerId} {player.PlayerName}");
                }
                /*var localPlayer = client.LocalPlayer;
                if (localPlayer != null)
                {
                    var timeLeft = client.TimeLeft;
                    Console.WriteLine($"Player name: {localPlayer->PlayerName}");
                    Console.WriteLine($"Time left: {timeLeft.Minutes}:{timeLeft.Seconds}");
                    Console.WriteLine($"X:{localPlayer->Position.X};Y:{localPlayer->Position.Y}");
                    Console.WriteLine($"{localPlayer->Health->Value} HP");
                    Console.WriteLine($"{localPlayer->Money->Value} $");
                    Console.WriteLine($"{localPlayer->Weapon->WeaponId} - {localPlayer->Weapon->CurrentAmmo}|{localPlayer->Weapon->MaxAmmo} ({localPlayer->Weapon->WeaponMode})");
                }*/
                Thread.Sleep(10);
            }
        }
    }
}