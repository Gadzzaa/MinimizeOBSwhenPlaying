using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace OsuHG
{
    public static class FindPrograms
    {
        public static void FindEXE(string x, string process)
        {
            var y = Process.GetProcessesByName(process).FirstOrDefault();
            while (y == null)
            {
                Console.Write("Awaiting " + x);
                y = Process.GetProcessesByName(process).FirstOrDefault();
                Thread.Sleep(500);
                Console.Write(".");
                y = Process.GetProcessesByName(process).FirstOrDefault();
                Thread.Sleep(500);
                Console.Write(".");
                y = Process.GetProcessesByName(process).FirstOrDefault();
                Thread.Sleep(500);
                Console.Write(".");
                y = Process.GetProcessesByName(process).FirstOrDefault();
                Thread.Sleep(500);
                ConsoleClearV2.Clear();
            }
        }

        public static void FindProgram64or32(string x, string process64, string process32)
        {
            var y = Process.GetProcessesByName(process64).FirstOrDefault();
            var z = Process.GetProcessesByName(process32).FirstOrDefault();
            while (y == null && z == null)
            {
                Console.Write("Awaiting " + x);
                y = Process.GetProcessesByName(process64).FirstOrDefault();
                z = Process.GetProcessesByName(process32).FirstOrDefault();
                Thread.Sleep(500);
                Console.Write(".");
                y = Process.GetProcessesByName(process64).FirstOrDefault();
                z = Process.GetProcessesByName(process32).FirstOrDefault();
                Thread.Sleep(500);
                Console.Write(".");
                y = Process.GetProcessesByName(process64).FirstOrDefault();
                z = Process.GetProcessesByName(process32).FirstOrDefault();
                Thread.Sleep(500);
                Console.Write(".");
                y = Process.GetProcessesByName(process64).FirstOrDefault();
                z = Process.GetProcessesByName(process32).FirstOrDefault();
                Thread.Sleep(500);
                ConsoleClearV2.Clear();
            }
        }
    }
}
