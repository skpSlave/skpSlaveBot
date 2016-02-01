using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeToTwitter
{
    public static class Constants
    {
        public static string COMMAND_CREATE_TABLE = @"CREATE TABLE [Messages]
                                                        (
                                                          [ID] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                          [ChatID] text NOT NULL,
                                                          [Sender] text NOT NULL,
                                                          [Message] text,
                                                          [Timestamp] text,
                                                          [TwitterID] text
                                                        );";

        public static string COMMAND_INSERT_MESSAGE = @"INSERT INTO Messages (ChatID, Sender, Message, Timestamp)
                                                       VALUES ('{0}', '{1}', '{2}', '{3}')";
    }
}
