using System;
using System.IO;

namespace SkypeToTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg == Constants.SILENT_MODE)
                    Constants.SILENT_MODE_ENABLED = true;

                if (arg == Constants.LANGUAGE_SWITCHER_MODE)
                    Constants.LANGUAGE_SWITCHER_MODE_ENABLED = false;
            }

            ConsoleCommandsHandler.Initialize();

            DBTools.Connect();
            SkypeTools.Connect(Constants.SILENT_MODE_ENABLED);
            LoadWordDict();

            while (!ConsoleCommandsHandler.ExitCommand()) { }
        }

        private static void LoadWordDict()
        {
            string[] lines = null;

            if (File.Exists(@"DICT\words.num"))
            {
                lines = File.ReadAllLines(@"DICT\words.num");

                int linesCount = lines.Length;
                int treshold = lines.Length;

                if (linesCount >= treshold)
                {
                    for (int i = 0; i < treshold; i++)
                    {
                        Constants.WordsDict.Add(lines[i].ConvertRusToEng());
                    }
                }
            }
        }
    }
}