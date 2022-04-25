using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using OsuMemoryDataProvider;
using OsuMemoryDataProvider.OsuMemoryModels;

namespace MinimizeAppSomething
{
    internal static class Program
    {        
        private static int _readDelay = 33;
        
        private static readonly CancellationTokenSource Cts = new CancellationTokenSource();
        private static StructuredOsuMemoryReader _sreader;
        private static readonly OsuBaseAddresses BaseAddresses = new OsuBaseAddresses();
        private static OsuMemoryStatus _lastStatus;

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        [System.Runtime.InteropServices.DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")] public static extern int SetForegroundWindow(IntPtr hwnd);

        [STAThread]
        static async Task Main(string[] args)
        {
            _sreader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint(args.FirstOrDefault());
            _sreader.InvalidRead += SreaderOnInvalidRead; 
            await OsuData();                    
        }

        private static async Task OsuData()
        {
            await Task.Run(async () =>
            {
                _sreader.WithTimes = true;
                while (true)
                {
                    if (Cts.IsCancellationRequested)
                        return;

                    if (!_sreader.CanRead)
                    {
                        Console.WriteLine("osu! process not found");
                        await Task.Delay(_readDelay);
                        continue;
                    }
                    _sreader.TryRead(BaseAddresses.GeneralData);
                    if (BaseAddresses.GeneralData.OsuStatus == OsuMemoryStatus.Playing && _lastStatus != OsuMemoryStatus.Playing)
                        MinimizeProgram();
                    else if(BaseAddresses.GeneralData.OsuStatus != OsuMemoryStatus.Playing && _lastStatus == OsuMemoryStatus.Playing)
                        MaximizeProgram();
                    _lastStatus = BaseAddresses.GeneralData.OsuStatus;
                }
            }, Cts.Token);
        }

        private static void MinimizeProgram()
        {
            var p = System.Diagnostics.Process.GetProcessesByName("obs64").FirstOrDefault();
            var p2 = Process.GetProcessesByName("osu!").FirstOrDefault();
            if(p!=null)
            {
                ShowWindow(p.MainWindowHandle, (int)ShowWindowEnum.ShowMinimized);
            }
        }

        private static void MaximizeProgram()
        {
            var p = System.Diagnostics.Process.GetProcessesByName("obs64").FirstOrDefault();
            var p2 = Process.GetProcessesByName("osu!").FirstOrDefault();

            if(p!=null)
            {
                ShowWindow(p.MainWindowHandle, (int)ShowWindowEnum.ShowNormalNoActivate);
            }
        }
        private static void SreaderOnInvalidRead(object sender, (object readObject, string propPath) e)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now:T} Error reading {e.propPath}{Environment.NewLine}");
            }
            catch (ObjectDisposedException)
            {

            }        
        }
    }
}