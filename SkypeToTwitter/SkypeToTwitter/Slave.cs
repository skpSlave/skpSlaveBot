using SKYPE4COMLib;
using System;
using System.Collections.Generic;
using System.Linq;

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
        };

        public static bool HandleMessage(ChatMessage message, out string answer)
        {
            answer = String.Empty;
            string trimMessage = message.Body.Trim();

            if (trimMessage.StartsWith("-h"))
            {
                answer = "-w погода в Харькове" + Environment.NewLine + "-w city погода в city";
                return false;
            }
            else if (trimMessage.StartsWith("-w"))
            {
                string[] param = trimMessage.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                answer = param.Length > 1 ? Weather.GetWeather(param[1]) : Weather.GetWeather();
                return false;
            }
            else if (trimMessage.StartsWith("-s"))
            {
                answer = answers[r.Next(0, answers.Count - 1)];
                return false;
            }

            return true;
        }
    }
}