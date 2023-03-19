using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace OsuHG
{
    public static class FindPrograms
    {
        private static Process y,z;
        public static Process FindEXE(string x, string process)
        {
            while(y == null)
            {
                Console.WriteLine("BE AWARE: Settings unavailable\r\n");
                Console.Write("Awaiting " + x);
                for (var i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    y = Process.GetProcessesByName(process).FirstOrDefault();
                    Thread.Sleep(500);
                }
                ConsoleClearV2.Clear();
            }
            ConsoleClearV2.Clear();
            return y;
        }

        public static void FindProgram64or32(string x, string process64, string process32)
        {

            while (y == null && z == null) 
            {
                Console.WriteLine("Settings unavailable");
                Console.Write("Awaiting " + x);
                for (var i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    y = Process.GetProcessesByName(process64).FirstOrDefault();
                    z = Process.GetProcessesByName(process32).FirstOrDefault();
                    Thread.Sleep(500);
                }
                ConsoleClearV2.Clear();
            }
            ConsoleClearV2.Clear();
        }
    }
}
