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
        };

        public static bool HandleMessage(ChatMessage message, out string answer)
        {
            answer = String.Empty;
            string trimMessage = message.Body.Trim();
            if (trimMessage.StartsWith("-"))
            {
                if (trimMessage.StartsWith("-h"))
                {
                    answer = @"Тут будет список команд.";
                }
                else if (trimMessage.StartsWith("-s"))
                {
                    answer = answers[r.Next(0, answers.Count - 1)];
                }
                else
                {
                    answer = @"Даже не знаю, что сказать...";
                }

                return false;
            }

            return true;
        }
    }
}