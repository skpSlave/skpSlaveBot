using System;
using System.Collections.Generic;

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

        //launch keys
        public static string SILENT_MODE = "-silent";
        public static bool SILENT_MODE_ENABLED = false;

        public static string LANGUAGE_SWITCHER_MODE = "-disableLangSwitch";
        public static bool LANGUAGE_SWITCHER_MODE_ENABLED = true;

        //Twitter constants
        public static Dictionary<String, String> TWITTER_NICKS = new Dictionary<string, string>()
        {
            { "ddev1l1", "Дед" },
            { "germanov.pavel", "Павло" },
            { "sergey.yak1movich", "Серж" },
            { "gan4in", "Пейс" },
            { "dustazz", "Юрко" },
        };

        public static List<string> IMAGE_EXTS = new List<string>()
        {
            ".jpg", ".jpeg", ".gif", ".png", ".bmp"
        };

        //Other constants
        public static string DEFAULT_ANSWER = "(shake)";
        public static string HELP_DESCRIPTION = String.Format("{1}{0}{2}",
                                                              Environment.NewLine,
                                                              "- [!] в начале сообщения - не обрабатывать сообщение",
                                                              "- Погода: -w, -w [city]");
        public static string UPTIME_MESSAGE = "System UpTime: {0} Day(s) {1}:{2}:{3}";

        public static string ENGLISH_CHARS = "`qwertyuiop[]\asdfghjkl;'zxcvbnm,./~QWERTYUIOP{}|ASDFGHJKL:\"ZXCVBNM<>?!@#$^&";
        public static string RUSSIAN_CHARS = "ёйцукенгшщзхъ\\фывапролджэячсмитьбю.ЁЙЦУКЕНГШЩЗХЪ/ФЫВАПРОЛДЖЭЯЧСМИТЬБЮ,!\"№;:?";

        public static List<string> WordsDict = new List<string>();
    }
}