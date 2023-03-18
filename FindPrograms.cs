﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace OsuHG
{
    public static class FindPrograms
    {
        private static Process y,z;
        public static void FindEXE(string x, string process)
        {
            do {
                Console.Write("Awaiting " + x);
                y = Process.GetProcessesByName(process).FirstOrDefault();
                Thread.Sleep(500);
            } while(y == null);
        }

        public static void FindProgram64or32(string x, string process64, string process32)
        {

            do {
                Console.Write("Awaiting " + x);
                for (var i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    y = Process.GetProcessesByName(process64).FirstOrDefault();
                    z = Process.GetProcessesByName(process32).FirstOrDefault();
                    Thread.Sleep(500);
                }

                ConsoleClearV2.Clear();
            } while (y == null && z == null);
        }
    }
}
