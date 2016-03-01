using System;
using System.Text;

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

        public static string ConvertEngToRus(this string input)
        {
            var result = new StringBuilder(input.Length);
            int index;
            foreach (var symbol in input)
                result.Append((index = Constants.ENGLISH_CHARS.IndexOf(symbol)) != -1 ? Constants.RUSSIAN_CHARS[index] : symbol);
            return result.ToString();
        }
    }
}