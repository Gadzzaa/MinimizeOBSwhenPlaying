using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace OsuHG
{
    public class FirstTime
    {
        public static bool firstTime = false;
        public static List<string> BeforeF8 = new List<string>()
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
        public static void FirstTimeProg()
        {
            if (firstTime) return;
            foreach (var x in BeforeF8)
            {
                Write.WriteString(x);
                Console.WriteLine();
            }
            Console.WriteLine();
            int z = 0;
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.F8))
            {
                if (z % 2 != 0)
                {
                    Console.Write(Settings1.Default[AfterF8[z]].ToString());
                    Console.WriteLine();
                }
                else 
                    Write.WriteString(AfterF8[z]);
                z++;
                if (z == AfterF8.Length - 1)
                {
                    Console.WriteLine();
                    for (int i = 5; i > 0; i--)
                    {
                        Write.WriteString("Closing in " + i);
                        Console.SetCursorPosition(Console.CursorLeft-12, Console.CursorTop);
                        Thread.Sleep(1000);
                    }
                    ConsoleClearV2.Clear();
                    break;
                }
            }
            //TODO: Here comes code to replace settings
        }
    }
}
