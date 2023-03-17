using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
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
                FirstTime.FirstTimeProg();
                // Timeout => Helps with CPU Usage
                Thread.Sleep(500);

                // Await osu!
                FindPrograms.FindEXE("osu!", "osu!");
                Console.Clear();
                osu = Process.GetProcessesByName("osu!").FirstOrDefault();

                // Set srReader
                _sreader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint(args.FirstOrDefault());
                _sreader.TryRead(GeneralData);
                
            }
        }
    }
}
