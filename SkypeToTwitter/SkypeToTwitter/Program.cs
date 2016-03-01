namespace SkypeToTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleCommandsHandler.Initialize();

            DBTools.Connect();
            SkypeTools.Connect(args.Length > 0 && args[0] == Constants.SILENT_MODE);

            while (!ConsoleCommandsHandler.ExitCommand()) { }
        }
    }
}