using System;
using System.Threading;

namespace OsuHG
{
    public class Write
    {
        public static void WriteString(string x)
        {
            for (var i = 0; i < x.Length; i++)
            {
                Console.Write(x[i]);
                Thread.Sleep(10);
            }
        }
    }
}