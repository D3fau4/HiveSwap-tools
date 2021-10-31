using System;

namespace DONTTOUCHTHECHILD
{
    internal class cmd
    {
        public enum LogType
        {
            Error = ConsoleColor.Red,
            Warning = ConsoleColor.Yellow,
            Information = ConsoleColor.Green,
            Debug = ConsoleColor.Magenta
        }

        public static void print(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void print(int message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
