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

        private static readonly GeneralData GeneralData = new OsuBaseAddresses().GeneralData;

        private static Process obs32, obs64, osu;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [STAThread]
        private static void Main(string[] args)
        {
            while (true)
            {
                // Timeout => Helps with CPU Usage
                Thread.Sleep(500);

                // Await osu!
                FindPrograms.FindEXE("osu!", "osu!");
                Console.Clear();
                osu = Process.GetProcessesByName("osu!").FirstOrDefault();

                // Await OBS
                FindPrograms.FindProgram64or32("OBS", "obs64", "obs32");
                Console.Clear();
                obs32 = Process.GetProcessesByName("obs32").FirstOrDefault();
                obs64 = Process.GetProcessesByName("obs64").FirstOrDefault();
                
                // Cooldown to load every program
                Thread.Sleep(2500);

                // Set srReader
                _sreader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint(args.FirstOrDefault());
                _sreader.TryRead(GeneralData);

                // Execute program
                if (obs32 != null) Ver32bit();
                else Ver64bit();
            }
        }

        private static void Ver32bit()
        {
            if (GeneralData.OsuStatus != OsuMemoryStatus.Playing) ShowWindow(obs32.MainWindowHandle, 4);
            else ShowWindow(obs32.MainWindowHandle, 2);
        }

        private static void Ver64bit()
        {
            if (GeneralData.OsuStatus != OsuMemoryStatus.Playing) ShowWindow(obs64.MainWindowHandle, 4);
            else ShowWindow(obs64.MainWindowHandle, 2);
        }
    }
}
