using System;


namespace Store.GetUserData
{
    public static class PrintData
    {
        public static void Print(string message)
        {
            Console.WriteLine(message);
        }

        public static void PrintLine(string message)
        {
            Console.Write(message);
        }

        public static void PrintClear()
        {
            Console.Clear();
        }
        public static void PrintWithChangeColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Print(message);
            Console.ResetColor();
        }
    }
}
