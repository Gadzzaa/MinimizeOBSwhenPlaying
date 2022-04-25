using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuMemoryDataProvider;
using OsuMemoryDataProvider.OsuMemoryModels;
using OsuMemoryDataProvider.OsuMemoryModels.Direct;
using Formatting = System.Xml.Formatting;

namespace MinimizeAppSomething
{
    static class Program
    {        
        private int _readDelay = 33;
        
        private CancellationTokenSource cts = new CancellationTokenSource();
        private StructuredOsuMemoryReader _sreader;

        [STAThread]
        static void Main(string[] args)
        {
            _sreader = StructuredOsuMemoryReader.Instance.GetInstanceForWindowTitleHint((string) args.FirstOrDefault());
            _sreader.InvalidRead += SreaderOnInvalidRead;
        }

        public async void OsuData()
        {
            await Task.Run(async () =>
            {
                _sreader.WithTimes = true;
                var baseAddresses = new OsuBaseAddresses();
                while (true)
                {
                    if (cts.IsCancellationRequested)
                        return;

                    if (!_sreader.CanRead)
                    {
                        Console.WriteLine("osu! process not found");
                        await Task.Delay(_readDelay);
                        continue;
                    }
                    _sreader.TryRead(baseAddresses.GeneralData);
                    Console.WriteLine("CURRENT STATUS: " + baseAddresses.GeneralData.OsuStatus);
                }
            }, cts.Token);
        }
        private void SreaderOnInvalidRead(object sender, (object readObject, string propPath) e)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now:T} Error reading {e.propPath}{Environment.NewLine}");
            }
            catch (ObjectDisposedException)
            {

            }        
        }
        private T ReadProperty<T>(object readObj, string propName, T defaultValue = default) where T : struct
        {
            if (_sreader.TryReadProperty(readObj, propName, out var readResult))
                return (T)readResult;

            return defaultValue;
        }
        private T ReadClassProperty<T>(object readObj, string propName, T defaultValue = default) where T : class
        {
            if (_sreader.TryReadProperty(readObj, propName, out var readResult))
                return (T)readResult;

            return defaultValue;
        }
        private int ReadInt(object readObj, string propName)
            => ReadProperty<int>(readObj, propName, -5);
        private short ReadShort(object readObj, string propName)
            => ReadProperty<short>(readObj, propName, -5);

        private float ReadFloat(object readObj, string propName)
            => ReadProperty<float>(readObj, propName, -5f);

        private string ReadString(object readObj, string propName)
            => ReadClassProperty<string>(readObj, propName, "INVALID READ");
    }
}