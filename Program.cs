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
        private static OsuMemoryStatus _lastStatus;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [STAThread]
        private static void Main(string[] args)
        {
            while (true) {
                _sreader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint(args.FirstOrDefault());
                _sreader.WithTimes = true;
                _sreader.TryRead(BaseAddresses.GeneralData);
                if (BaseAddresses.GeneralData.OsuStatus == OsuMemoryStatus.Playing && _lastStatus != OsuMemoryStatus.Playing)
                {
                    var p = Process.GetProcessesByName("obs64").FirstOrDefault();
                    if (p != null) ShowWindow(p.MainWindowHandle, 2);
                }
                else if (BaseAddresses.GeneralData.OsuStatus != OsuMemoryStatus.Playing && _lastStatus == OsuMemoryStatus.Playing)
                {
                    var p = Process.GetProcessesByName("obs64").FirstOrDefault();
                    if (p != null) ShowWindow(p.MainWindowHandle, 4);
                }
                _lastStatus = BaseAddresses.GeneralData.OsuStatus;
                /*
                 Hide = 0,
                ShowNormal = 1,
                ShowMinimized = 2,
                ShowMaximized = 3,
                Maximize = 3,
                ShowNormalNoActivate = 4,
                Show = 5,
                Minimize = 6,
                ShowMinNoActivate = 7,
                ShowNoActivate = 8,
                Restore = 9,
                ShowDefault = 10,
                ForceMinimized = 11
                 */
            }
        }
    }
}