using SKYPE4COMLib;
using System;
using System.Collections.Generic;

namespace SkypeToTwitter
{
    public static class Slave
    {
        private static Random r = new Random();
        private static List<string> answers = new List<string>()
        {
            @"Ололо пыщь пыщь я вам скажу",
            @"Да иди ты!",
            @"Зачем?",
            @"5$",
            @"Нет, спасибо",
            @"."
        };

        public static bool HandleMessage(ChatMessage message, out string answer)
        {
            answer = String.Empty;
            bool insertToBase = true;

            string trimMessage = message.Body.Trim();

            if (trimMessage == "-h")
            {
                answer = "-w - погода в Харькове" + Environment.NewLine + "-w [city] - погода в [city]";
                insertToBase = false;
            }
                else if (trimMessage.StartsWith("-s"))
            {
                string[] param = trimMessage.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                answer = param.Length > 1 ? Weather.GetWeather(param[1]) : Weather.GetWeather();
                insertToBase = false;
            }
            if (trimMessage.StartsWith("-n"))
            {
                string[] param = trimMessage.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (param.Length > 1)
                {
                    string secondParam = param[1];
                    string secondParamLowered = secondParam.ToLower();

                    if (!secondParamLowered.Contains("раст") && !secondParam.Contains("rast") &&
                        !secondParam.Contains("паш") && !secondParam.Contains("pash"))
                    {
                        answer = "Иди ка ты нахер, " + secondParam + "!";
                        insertToBase = false;
                    }
                    else
                    {
                        insertToBase = false;
                    }
                }
                else
                {
                    answer = "Нахер все это!";
                    return false;
                }

            if (String.IsNullOrEmpty(answer))
                answer = "(shake)";

            return insertToBase;
        }
    }
}