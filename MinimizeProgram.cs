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

        private static Process exec;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void MinimProgram()
        {
            exec = Process.GetProcessesByName(Settings1.Default.execName).FirstOrDefault();
            
            // Await Program
            if(exec == null)
                exec = FindPrograms.FindEXE("Program Executable (" + Settings1.Default.execName + ")", Settings1.Default.execName);
            ConsoleClearV2.Clear();

            // Execute program
            if (GeneralData.OsuStatus != OsuMemoryStatus.Playing && _lastStatus == OsuMemoryStatus.Playing) ShowWindow(exec.MainWindowHandle, 4);
            else ShowWindow(exec.MainWindowHandle, 2);
            _lastStatus = GeneralData.OsuStatus;
        }
    }
}
