using System;

namespace SkypeToTwitter
{
    public static class Constants
    {
        //DB command constants
        public static string COMMAND_CREATE_TABLE = @"CREATE TABLE [Chat_{0}]
                                                        (
                                                          [ID] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                          [ChatID] text NOT NULL,
                                                          [Sender] text NOT NULL,
                                                          [Message] text,
                                                          [Timestamp] text,
                                                          [TwitterID] text
                                                        );";

        public static string COMMAND_INSERT_MESSAGE = @"INSERT INTO [Chat_{0}] (ChatID, Sender, Message, Timestamp)
                                                       VALUES ('{0}', '{1}', '{2}', '{3}')";
       
        //Other constants
        public static string DEFAULT_ANSWER = "(shake)";
        public static string HELP_DESCRIPTION = String.Format("{1}{0}{2}",
                                                              Environment.NewLine,
                                                              "- [!] в начале сообщения - не обрабатывать сообщение",
                                                              "- Погода: -w, -w [city]");
    }
}