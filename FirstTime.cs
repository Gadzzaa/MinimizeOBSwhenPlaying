using System;
using System.Collections.Generic;
using System.Threading;

namespace OsuHG
{
    public class FirstTime
    {
        public static bool firstTime = false;

        public static List<string> BeforeF8 = new List<string>
        {
            "Hello! It looks like its your first time running OsuHG!",
            "Before we are getting started, make sure these settings are correct.",
            "(Press 'F8' to change the settings)"
        };

        public static string[] AfterF8 =
        {
            "Use minimizing feature?: ", "minFeature",
            "Program executable name('without .exe'): ", "execName",
            "Use gamma feature?: ", "gammaFeature",
            "Lowest usable gamma: ", "lowGamma",
            "Highest usable gamma: ", "highGamma"
        };

        private static bool y = true;

        public static void FirstTimeProg()
        {
            if (firstTime) return;
            foreach (var x in BeforeF8)
            {
                Write.WriteString(x);
                Console.WriteLine();
            }

            Console.WriteLine();
            var z = 0;
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.F8))
            {
                if (z < 10)
                {
                    if (z % 2 != 0)
                    {
                        if (z != 3)
                        {
                            Console.Write(Settings1.Default[AfterF8[z]].ToString());
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Write("UNAVAILABLE, ONLY OBS SUPPORTED");
                            Console.WriteLine();
                        }
                    }
                    else Write.WriteString(AfterF8[z]);
                }

                if (z == AfterF8.Length)
                {
                    Console.WriteLine();
                    for (var i = 5; i > 0; i--)
                    {
                        Write.WriteString("Closing in " + i);
                        Console.SetCursorPosition(Console.CursorLeft - 12, Console.CursorTop);
                        Thread.Sleep(1000);
                    }

                    ConsoleClearV2.Clear();
                    y = false;
                    Settings1.Default.firstTime = false;
                    break;
                }
                z++;
            }

            if (y)
            {
                ConsoleClearV2.Clear();
                SettingsMenu.Menu();
                Settings1.Default.firstTime = false;
            }

            Settings1.Default.Save();
        }
    }
}
