using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Timers;

namespace SkypeToTwitter
{
    class Program
    {
        private static Stopwatch upTime = new Stopwatch();
        private static System.Timers.Timer _writeUpTimeTIMER;

        static void Main(string[] args)
        {
            upTime.Start();

            DBTools.Connect();
            SkypeTools.Connect();

            SetUpTimeTimer(60000 * 10); //10 minutes

            while (Console.ReadLine() != "exit")
            {
            }

            upTime.Stop();

            File.WriteAllText(String.Format("lastUpTime_{0}.log", DateTime.Now.ToString("dd-MM-yyyy")), upTime.Elapsed.ToString());
        }

        private static void SetUpTimeTimer(int interval)
        {
            _writeUpTimeTIMER = new System.Timers.Timer(interval);
            _writeUpTimeTIMER.Elapsed += writeUpTimeToConsole;
            _writeUpTimeTIMER.AutoReset = true;
            _writeUpTimeTIMER.Enabled = true;
        }

        private static void writeUpTimeToConsole(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Up time: {0}", upTime.Elapsed);
        }
    }
}