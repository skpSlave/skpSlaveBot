using System.Threading;

namespace SkypeToTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            DBTools.Connect();
            SkypeTools.Connect();

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}