using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using OsuMemoryDataProvider;
using OsuMemoryDataProvider.OsuMemoryModels.Direct;

namespace OsuHG
{
    public class MinimizeProgram
    {
        private static readonly GeneralData GeneralData = Program.GeneralData;

        private static OsuMemoryStatus _lastStatus;

        private static Process obs32,obs64;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void MinimProgram()
        {
            // Await OBS
            if(obs32 == null && obs64 == null)
                FindPrograms.FindProgram64or32("OBS", "obs64", "obs32");

            obs32 = Process.GetProcessesByName("obs32").FirstOrDefault();
            obs64 = Process.GetProcessesByName("obs64").FirstOrDefault();

            // Execute program
            if (obs32 != null) Ver32bit();
            else Ver64bit();

            _lastStatus = GeneralData.OsuStatus;
        }
        private static void Ver32bit()
        {
            if (GeneralData.OsuStatus != OsuMemoryStatus.Playing && _lastStatus == OsuMemoryStatus.Playing) ShowWindow(obs32.MainWindowHandle, 4);
            else ShowWindow(obs32.MainWindowHandle, 2);
        }

        private static void Ver64bit()
        {
            if (GeneralData.OsuStatus != OsuMemoryStatus.Playing && _lastStatus == OsuMemoryStatus.Playing) ShowWindow(obs64.MainWindowHandle, 4);
            else ShowWindow(obs64.MainWindowHandle, 2);
        }
    }
}
