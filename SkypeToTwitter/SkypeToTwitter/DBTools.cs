using SKYPE4COMLib;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System;
using System.Collections.Generic;

namespace SkypeToTwitter
{
    //[Table]
    //public class ChatTable
    //{
    //    [PrimaryKey]
    //    public Int32 ID { get; set; }
    //    [Field]
    //    public String ChatID { get; set; }
    //    [Field]
    //    public String Sender { get; set; }
    //    [Field]
    //    public String Message { get; set; }
    //    [Field]
    //    public String Timestamp { get; set; }
    //    [Field]
    //    public String TwitterID { get; set; }
    //}

    public static class DBTools
    {
        private static string ConnectionString;
        private static List<string> _tables;

        public static void Connect()
        {
            _tables = new List<string>();
            ConnectionString = GetConnectionString();
        }

        private static string GetConnectionString()
        {
            string DBName = ConfigurationManager.ConnectionStrings["DBName"].ConnectionString;
            string DBPath = ConfigurationManager.ConnectionStrings["DBPath"].ConnectionString;
            string FullDBPath = Path.Combine(DBPath, DBName);

            if (!Directory.Exists(DBPath))
            {
                Directory.CreateDirectory(DBPath);
            }

            if (!File.Exists(FullDBPath))
            {
                SQLiteConnection.CreateFile(FullDBPath);
            }

            return FullDBPath;
        }

        private static void ExecuteCommand(string command)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.ConnectionString = "Data Source = " + ConnectionString;

                connection.Open();

                using (SQLiteCommand com = new SQLiteCommand(connection))
                {
                    com.CommandText = command;

                    com.CommandType = CommandType.Text;

                    com.ExecuteNonQuery();
                }
            }
        }

        public static void InsertMessage(MessageEntity message)
        {
            CreateTable(message.ChatID);

            ExecuteCommand(string.Format(Constants.COMMAND_INSERT_MESSAGE, message.ChatID, message.Sender, message.Message.Replace("'", "_"), message.Timestamp));
        }

        public static void CreateTable(String chatName)
        {
            if (!_tables.Contains(chatName))
            {
                _tables.Add(chatName);
                ExecuteCommand(String.Format(Constants.COMMAND_CREATE_TABLE, chatName));
            }
        }
    }
}