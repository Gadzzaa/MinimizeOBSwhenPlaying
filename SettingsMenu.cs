using System;
using System.Threading.Tasks;

namespace OsuHG
{
    public class SettingsMenu
    {
        private static int x;

        public static string[] Settings =
        {
            "1. Use minimizing feature? (True/False): ", "minFeature",
            "2. Program executable name('without .exe'): ", "execName",
            "3. Use gamma feature? (True/False): ", "gammaFeature",
            "4. Lowest usable gamma (min. 0): ", "lowGamma",
            "5. Highest usable gamma (max. 2): ", "highGamma"
        };

        public static void Menu()
        {
            for (var i = 0; i < Settings.Length; i++)
                if (i % 2 != 0)
                {
                    Console.Write(Settings1.Default[Settings[i]].ToString());
                    Console.WriteLine();
                }
                else
                {
                    Write.WriteString(Settings[i]);
                }

            Console.WriteLine();
            Write.WriteString("Type '6' for main menu.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Write.WriteString("Select which setting you wish to change: ");
            x = Int32.Parse(Console.ReadLine());
            int y = 2 * (x - 1);
            ConsoleClearV2.Clear();
            Write.WriteString(Settings[y]);
            switch (y) 
            {
                case 0:
                    Settings1.Default.minFeature = TrueFalse(Console.ReadLine());
                    break;
                case 2:
                    Settings1.Default.execName = Console.ReadLine();
                    break;
                case 4:
                    Settings1.Default.gammaFeature = TrueFalse(Console.ReadLine());
                    break;
                case 6:
                    Settings1.Default.lowGamma = int.Parse(Console.ReadLine());
                    break;
                case 8:
                    Settings1.Default.highGamma = int.Parse(Console.ReadLine());
                    break;
                case 10:
                    break;
                default:
                    OutOfRange();
                    break;

            }
            ConsoleClearV2.Clear();
            Settings1.Default.Save();
        }

        public static void OutOfRange()
        {
            Write.WriteString("Out of range!");
            Console.SetCursorPosition(Console.CursorLeft - 13, Console.CursorTop);
            Console.SetCursorPosition(Console.CursorLeft - 999999999, Console.CursorTop - 1);
            ConsoleClearV2.Clear();
        }

        public static bool TrueFalse(string value)
        {
            if (value.ToLower() == "True".ToLower())
                return true;
            if (value.ToLower() == "False".ToLower())
                return false;
            ConsoleClearV2.Clear();
            Write.WriteString("Out of range!");
            return false;
        }
    }
}
