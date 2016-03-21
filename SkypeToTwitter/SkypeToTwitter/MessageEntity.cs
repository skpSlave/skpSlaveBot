using SKYPE4COMLib;
using System;
using System.Collections.Generic;

namespace SkypeToTwitter
{
    public class MessageEntity
    {
        public Int32 ID;
        public string ChatID;
        public string Sender;
        public string Message;
        public string TwitterMessage;
        public DateTime Timestamp;
        public string TwitterID;

        public string TwitterNick;

        public bool Secure = false;

        public MessageEntity(ChatMessage message)
        {
            ChatID = message.Chat.Name;
            Sender = message.Sender.Handle;
            Message = message.Body;
            Timestamp = message.Timestamp;

            //Set twitter nick and secure flag
            if (Constants.TWITTER_NICKS.TryGetValue(Sender, out TwitterNick))
            {
                Secure = true;
            }

            PrepareMessage();
        }

        private void PrepareMessage()
        {
            if(Message.Contains("<<<"))
            {
                TwitterMessage = Message.Split(new[] { "<<<" }, StringSplitOptions.None)[1];
            }
            else
            {
                TwitterMessage = Message;
            }
        }
    }
}