using System;
using System.Threading;


namespace OsuHG
{
    public class FirstTime
    {
        public static void FirstTimeProg()
        {
            Write.WriteString("Hello! It looks like its your first time running OsuHG!");
            Console.WriteLine();
            Write.WriteString("Before we are getting started, make sure these settings are correct.");
            Console.WriteLine();
            Write.WriteString("(Press 'F8' to change the settings)");
            Console.WriteLine();
            Console.WriteLine();
            Write.WriteString("Use minimizing feature?: ");
            Console.Write("<AddSetting>");
            Console.WriteLine();
            //If yes
            Write.WriteString("Program executable name('without .exe'): ");
            Console.WriteLine("<AddSetting>");
            Console.WriteLine();
            Write.WriteString("Use gamma feature?: ");
            Console.Write("<AddSetting>");
            Console.WriteLine();
            //If yes
            Write.WriteString("Lowest usable gamma: ");
            Console.Write("<AddSetting>");
            Console.WriteLine();
            Write.WriteString("Highest usable gamma: ");
            Console.Write("<AddSetting>");
            Console.WriteLine();
            Console.WriteLine();
            Write.WriteString("Closing in 5");
            Console.SetCursorPosition(Console.CursorLeft-12, Console.CursorTop);
            Thread.Sleep(1000);
            Console.Write("Closing in 4");
            Console.SetCursorPosition(Console.CursorLeft-12, Console.CursorTop);
            Thread.Sleep(1000);
            Console.Write("Closing in 3");            
            Console.SetCursorPosition(Console.CursorLeft-12, Console.CursorTop);
            Thread.Sleep(1000);
            Console.Write("Closing in 2");            
            Console.SetCursorPosition(Console.CursorLeft-12, Console.CursorTop);
            Thread.Sleep(1000);
            Console.Write("Closing in 1");  
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}
