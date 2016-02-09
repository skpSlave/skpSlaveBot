using System.Threading;
using System.Diagnostics;
using System;
using System.IO;

namespace SkypeToTwitter
{
    class Program
    {
        private static Stopwatch upTime = new Stopwatch();

        static void Main(string[] args)
        {
            upTime.Start();

            DBTools.Connect();
            SkypeTools.Connect();

            //while (true)
            //{
            //    Thread.Sleep(1000);
            //}

            while (Console.ReadLine() != "exit")
            {
            }

            upTime.Stop();

            File.WriteAllText("lastUpTime.log", upTime.Elapsed.ToString());
        }
    }
}