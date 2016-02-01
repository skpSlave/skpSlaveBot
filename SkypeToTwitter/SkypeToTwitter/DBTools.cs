using SKYPE4COMLib;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SkypeToTwitter
{
    public static class DBTools
    {
        private static string ConnectionString;

        public static void Connect()
        {
            bool DBExist;
            ConnectionString = GetConnectionString(out DBExist);

            if(!DBExist)
            {
                ExecuteCommand(Constants.COMMAND_CREATE_TABLE);
            }
        }

        private static string GetConnectionString(out bool exist)
        {
            exist = true;
            string DBName = ConfigurationManager.ConnectionStrings["DBName"].ConnectionString;
            string DBPath = ConfigurationManager.ConnectionStrings["DBPath"].ConnectionString;
            string FullDBPath = Path.Combine(DBPath, DBName);

            if (!Directory.Exists(DBPath))
            {
                Directory.CreateDirectory(DBPath);
            }

            if (!File.Exists(FullDBPath))
            {
                exist = false;
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

        public static void Insert(ChatMessage message)
        {
            MessageEntity messageEnt = new MessageEntity(message);
            ExecuteCommand(string.Format(Constants.COMMAND_INSERT_MESSAGE, messageEnt.ChatID, messageEnt.Sender, messageEnt.Message, messageEnt.Timestamp));
        }
    }
}