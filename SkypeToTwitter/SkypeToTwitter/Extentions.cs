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

        public static string ConvertRusToEng(this string input)
        {
            return ConvertLanguage(input, false);
        }

        public static string ConvertEngToRus(this string input)
        {
            return ConvertLanguage(input, true);
        }

        private static string ConvertLanguage(string input, bool engToRus)
        {
            var result = new StringBuilder(input.Length);
            int index;

            foreach (var symbol in input)
            {
                if (engToRus)
                {
                    result.Append((index = Constants.ENGLISH_CHARS.IndexOf(symbol)) != -1 ? Constants.RUSSIAN_CHARS[index] : symbol);
                }
                else
                {
                    result.Append((index = Constants.RUSSIAN_CHARS.IndexOf(symbol)) != -1 ? Constants.ENGLISH_CHARS[index] : symbol);
                }
            }

            return result.ToString();
        }
    }
}