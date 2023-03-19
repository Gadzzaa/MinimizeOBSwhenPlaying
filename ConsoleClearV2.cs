using System;

namespace OsuHG
{
    public class Watermark
    {
        public static void Clear()
        {
            Console.Clear();
            Console.Write("OsuHG developed by gadzzaa | Github: https://github.com/Gadzzaa/OsuHG | Discord: https://discord.gg/TtSQa944Ky\r\n\r\n");
            if (Settings1.Default.firstTime) return;
            Console.WriteLine("Press F8 in order to access settings");
            Console.WriteLine("Keep this command prompt open!\r\n");
            if(!Settings1.Default.minFeature && !Settings1.Default.gammaFeature)
                Console.WriteLine("You haven't enabled any feature! Check settings.\r\n\r\n");
        }
    }
}
