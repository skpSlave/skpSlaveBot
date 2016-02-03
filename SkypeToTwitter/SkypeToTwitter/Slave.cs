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

            if (trimMessage == "-h")
            {
                answer = "-w - погода в Харькове" + Environment.NewLine + "-w [city] - погода в [city]";
                insertToBase = false;
            }
            if (trimMessage.StartsWith("-w"))
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
                    insertToBase = false;
                }
            }

            if (String.IsNullOrEmpty(answer))
                answer = "(shake)";

            return insertToBase;
        }
    }
}