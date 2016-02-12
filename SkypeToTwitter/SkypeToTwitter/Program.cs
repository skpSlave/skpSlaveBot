namespace SkypeToTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleCommandsHandler.Initialize();

            DBTools.Connect();
            SkypeTools.Connect(args.Length > 0 && args[0] == "-silent");

            while (!ConsoleCommandsHandler.ExitCommand()) { }
        }
    }
}