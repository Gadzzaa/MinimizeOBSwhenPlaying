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

        public static OsuBaseAddresses BaseAddresses = new OsuBaseAddresses();

        public static readonly GeneralData GeneralData = BaseAddresses.GeneralData;
        
        public static OsuMemoryStatus _lastStatus;

        private static Process osu;

        public static bool Clear = false;

        [STAThread]
        private static void Main(string[] args)
        {
            while (true)
            {
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.F8))
                {
                    if(!Clear) ConsoleClearV2.Clear();
                    Clear = true;
                    if (Settings1.Default.firstTime) FirstTime.FirstTimeProg();
                    // Timeout => Helps with CPU Usage
                    Thread.Sleep(500);
                    
                    osu = Process.GetProcessesByName("osu!").FirstOrDefault();

                    // Await osu!
                    if (osu == null)
                    {
                        osu = FindPrograms.FindEXE("osu!", "osu!");
                        Clear = false;
                    }

                    // Minimize program
                    if (Settings1.Default.minFeature)
                    {
                        MinimizeProgram.MinimProgram();
                        _lastStatus = GeneralData.OsuStatus;
                        Clear = false;
                    }
                    
                    // Gamma Program
                    if (Settings1.Default.gammaFeature)
                        GammaProgram.Gamma();

                    // Set srReader
                    _sreader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint(args.FirstOrDefault());
                    _sreader.TryRead(BaseAddresses);
                }

                SettingsMenu.Menu();
                Clear = false; 
            }
        }
    }
}
