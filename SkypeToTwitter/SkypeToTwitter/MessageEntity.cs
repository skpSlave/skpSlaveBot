using SKYPE4COMLib;
using System;

namespace SkypeToTwitter
{
    public class MessageEntity
    {
        public Int32 ID;
        public string ChatID;
        public string Sender;
        public string Message;
        public DateTime Timestamp;
        public string TwitterID;

        public MessageEntity(ChatMessage message)
        {
            ChatID = message.Chat.Name;
            Sender = message.Sender.Handle;
            Message = message.Body;
            Timestamp = message.Timestamp;
        }
    }
}
