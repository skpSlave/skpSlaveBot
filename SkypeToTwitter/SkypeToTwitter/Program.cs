namespace SkypeToTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleCommandsHandler.Initialize();

            DBTools.Connect();
            SkypeTools.Connect();

            while (!ConsoleCommandsHandler.ExitCommand()) { }
        }
    }
}