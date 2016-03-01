using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SkypeToTwitter
{
    static class ConsoleCommandsHandler
    {
        public static Stopwatch upTime = new Stopwatch();

        public static void Initialize()
        {
            upTime.Start();

            //Start listeners
            ShowUpTime();
        }

        public static bool ExitCommand()
        {
            if (Console.ReadLine().ToLower() == "exit")
            {
                upTime.Stop();
                File.WriteAllText(String.Format("lastUpTime_{0}.log", DateTime.Now.ToString("dd-MM-yyyy")), upTime.Elapsed.ToString());
                return true;
            }

            return false;
        }
        
        private static void ShowUpTime()
        {
            Task.Run(delegate
            {
                while (true)
                {
                    if (Console.ReadLine().ToLower() == "uptime")
                    {
                        Extentions.ConsoleWriteLine(String.Format("Up time: {0}", upTime.Elapsed), ConsoleColor.Cyan);
                    }
                }
            });
        }
    }
}