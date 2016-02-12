using System;

namespace SkypeToTwitter
{
    public static class Extentions
    {
        public static void ConsoleWriteLine(String value, ConsoleColor color)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = prevColor;
        }
    }
}