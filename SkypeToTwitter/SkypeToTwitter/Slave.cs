using SKYPE4COMLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkypeToTwitter
{
    public static class Slave
    {
        public static bool HandleMessage(ChatMessage message, out string answer)
        {
            answer = String.Empty;
            bool insertToBase = true;

            string trimMessage = message.Body.Trim();

            if (trimMessage.StartsWith("!"))
            {
                return false;
            }
            //show help description
            else if (trimMessage == "-h")
            {
                answer = Constants.HELP_DESCRIPTION;
                insertToBase = false;
            }
            //show current uptime
            else if (trimMessage.StartsWith("-uptime"))
            {
                answer = String.Format(Constants.UPTIME_MESSAGE, ConsoleCommandsHandler.upTime.Elapsed.Days,
                                                                 ConsoleCommandsHandler.upTime.Elapsed.Hours.ToString("00"),
                                                                 ConsoleCommandsHandler.upTime.Elapsed.Minutes.ToString("00"),
                                                                 ConsoleCommandsHandler.upTime.Elapsed.Seconds.ToString("00"));
                insertToBase = false;
            }
            //show current weather
            else if (trimMessage.StartsWith("-w"))
            {
                string[] param = trimMessage.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                answer = param.Length > 1 ? Weather.GetWeather(param[1]) : Weather.GetWeather();
                insertToBase = false;
            }

            //language switcher
            if (Constants.LANGUAGE_SWITCHER_MODE_ENABLED && Constants.WordsDict.Count > 0 &&
                NeedToSwitchLanguage(trimMessage.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList()) &&
                !trimMessage.Contains("http") && !trimMessage.Contains("www") && trimMessage != "-w" && trimMessage != "-h")
            {
                answer = trimMessage.ConvertEngToRus();
            }

            if (!insertToBase && String.IsNullOrEmpty(answer))
                answer = Constants.DEFAULT_ANSWER;

            return insertToBase;
        }

        private static bool NeedToSwitchLanguage(List<string> parsedMessage)
        {
            int wordsCount = parsedMessage.Count;
            int containsCount = 0;
            int matchCount = 0;

            foreach (string word in parsedMessage)
            {
                if (Constants.WordsDict.Where(x => word.IndexOf(x, StringComparison.OrdinalIgnoreCase) > 0).Count() > 0)
                {
                    containsCount += 1;
                }

                if (Constants.WordsDict.Where(x => word == x || word == x + "&" || word == x + "?" || word == x + "/").Count() > 0)
                {
                    matchCount += 1;
                }
            }

            return (matchCount * 100) / wordsCount > 0 ? (containsCount * 100) / wordsCount > 90 : false;
        }
    }
}