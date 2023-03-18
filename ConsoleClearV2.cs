using System;

namespace OsuHG
{
    public class ConsoleClearV2
    {
        public static void Clear()
        {
            Console.Clear();
            Console.Write("OsuHG developed by gadzzaa | Github: https://github.com/Gadzzaa/OsuHG | Discord: https://discord.gg/TtSQa944Ky\r\n");
            Console.WriteLine();
            Console.WriteLine("Press F8 in order to access settings");
            Console.WriteLine("Keep this command prompt open!");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
