using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using OsuMemoryDataProvider;
using OsuMemoryDataProvider.OsuMemoryModels;

namespace MinimizeAppSomething
{
    internal static class Program
    {
        private static StructuredOsuMemoryReader _sreader;
        private static readonly OsuBaseAddresses BaseAddresses = new OsuBaseAddresses();
        private static Boolean playing = false;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [STAThread]
        private static void Main(string[] args)
        {
            Console.WriteLine("Open OBS and osu! and keep this running to automatically hide OBS while playing catJAM");
            while (true) {
                _sreader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint(args.FirstOrDefault());
                _sreader.WithTimes = true;

                while(Process.GetProcessesByName("osu!").Length == 0 && !_sreader.CanRead || Process.GetProcessesByName("obs64").Length == 0) continue;

                _sreader.TryRead(BaseAddresses.GeneralData);

                if (BaseAddresses.GeneralData.OsuStatus == OsuMemoryStatus.Playing && playing) ShowWindow(Process.GetProcessesByName("obs64")[0].MainWindowHandle, 2);
                else if (BaseAddresses.GeneralData.OsuStatus != OsuMemoryStatus.Playing && !playing) ShowWindow(Process.GetProcessesByName("obs64")[0].MainWindowHandle, 4);

                playing = !playing;
            }
        }
    }
}