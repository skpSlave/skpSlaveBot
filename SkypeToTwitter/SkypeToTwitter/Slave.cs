using SKYPE4COMLib;
using System;

namespace SkypeToTwitter
{
    public static class Slave
    {
        public static bool HandleMessage(ChatMessage message, out string answer)
        {
            answer = String.Empty;
            bool insertToBase = true;

            string trimMessage = message.Body.Trim();

            if(trimMessage.StartsWith("!"))
            {
                return false;
            }
            //show help description
            else if (trimMessage == "-h")
            {
                answer = Constants.HELP_DESCRIPTION;
                insertToBase = false;
            }
            //show current weather
            else if (trimMessage.StartsWith("-w"))
            {
                string[] param = trimMessage.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                answer = param.Length > 1 ? Weather.GetWeather(param[1]) : Weather.GetWeather();
                insertToBase = false;
            }

            if (!insertToBase && String.IsNullOrEmpty(answer))
                answer = Constants.DEFAULT_ANSWER;

            return insertToBase;
        }
    }
}