using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeToTwitter
{
    public static class Constants
    {
        public static string COMMAND_CREATE_TABLE = @"CREATE TABLE [Chat_{0}]
                                                        (
                                                          [ID] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                                          [ChatID] text NOT NULL,
                                                          [Sender] text NOT NULL,
                                                          [Message] text,
                                                          [Timestamp] text,
                                                          [TwitterID] text
                                                        );";

        public static string COMMAND_INSERT_MESSAGE = @"INSERT INTO [{0}] (ChatID, Sender, Message, Timestamp) VALUES ('{1}', '{2}', '{3}', '{4}')";
    }
}
