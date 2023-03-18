using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OsuMemoryDataProvider;
using OsuMemoryDataProvider.OsuMemoryModels;
using OsuMemoryDataProvider.OsuMemoryModels.Direct;

namespace OsuHG
{
    internal static class Program
    {
        private static StructuredOsuMemoryReader _sreader;

        public static readonly GeneralData GeneralData = new OsuBaseAddresses().GeneralData;

        private static Process osu;

        [STAThread]
        private static void Main(string[] args)
        {
            while (true)
            {
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.F8))
                {
                    ConsoleClearV2.Clear();
                    if (Settings1.Default.firstTime) FirstTime.FirstTimeProg();
                    // Timeout => Helps with CPU Usage
                    Thread.Sleep(500);
                    
                    // Await osu!
                    FindPrograms.FindEXE("osu!", "osu!");
                    ConsoleClearV2.Clear();
                    osu = Process.GetProcessesByName("osu!").FirstOrDefault();

                    //Minimize program
                    if (Settings1.Default.minFeature)
                        MinimizeProgram.MinimProgram();


                    // Set srReader
                    _sreader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint(args.FirstOrDefault());
                    _sreader.TryRead(GeneralData);
                }

                ConsoleClearV2.Clear();
                SettingsMenu.Menu();
            }
        }
    }
}
