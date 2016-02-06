using SKYPE4COMLib;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SkypeToTwitter
{
    static class SkypeTools
    {
        private static Skype skype = new Skype();
        private static System.Timers.Timer _checkMissedMessagesTimer;

        public static void Connect()
        {
            Task.Run(delegate
            {
                try
                {
                    if (!skype.Client.IsRunning)
                    {
                        skype.Client.Start(true, true);
                    }

                    skype.MessageStatus += OnMessageReceived;
                    skype.Attach(7, true);

                    RemoveUnreadedMessages();

                    Console.WriteLine("skype attached");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("top lvl exception : " + ex.ToString());
                }
            });

            SetTimerForMissedMessages(10000);
        }

        private static void RemoveUnreadedMessages()
        {
            int c = skype.MissedMessages.Count;

            for (int i = 1; i <= c; i++)
            {
                Console.Clear();
                Console.WriteLine("Removing unreaded messages: {0} from {1}", i, c);
                try { skype.MissedMessages[i].Seen = true; }
                catch { }
            }
        }

        private static void OnMessageReceived(ChatMessage pMessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsReceived)
            {
                Console.WriteLine("[{0}] [{1}] [{2}]", DateTime.Now.ToString("hh:mm:ss"), pMessage.Sender.FullName, pMessage.Body.Replace(Environment.NewLine, " |"));

                string answer = string.Empty;
                if (Slave.HandleMessage(pMessage, out answer))
                {
                    DBTools.InsertMessage(pMessage);
                }
                else
                {
                    SendMessage(answer, pMessage);
                }
            }
            else if (status == TChatMessageStatus.cmsSent)
            {
                Console.WriteLine("[{0}] Bot [{1}]", DateTime.Now.ToString("hh:mm:ss"), pMessage.Body.Replace(Environment.NewLine, " |"));

            }

            pMessage.Seen = true;
        }

        public static object _lock = new object();
        public static void SendMessage(string message, ChatMessage pMessage)
        {
            lock (_lock)
            {
                try
                {
                    pMessage.Chat.SendMessage(message);
                }
                catch
                {
                    try
                    {
                        skype.get_Chat(pMessage.ChatName).SendMessage(message);
                    }
                    catch
                    {
                        skype.SendMessage(pMessage.ChatName, message);
                    }
                }
            }
        }

        private static void CheckMissedMessages(Object source, ElapsedEventArgs e)
        {
            _checkMissedMessagesTimer.Enabled = false;
            int c = skype.MissedMessages.Count;

            if (c > 0)
            {
                Console.WriteLine("-----------------------------[{0}]-----------------------------", c);

                for (int i = 1; i <= c; i++)
                {
                    try
                    {
                        OnMessageReceived(skype.MissedMessages[i], TChatMessageStatus.cmsReceived);
                    }
                    catch { }
                }

                Console.WriteLine("Missed messages checked");
            }

            _checkMissedMessagesTimer.Enabled = true;
        }

        private static void SetTimerForMissedMessages(int interval)
        {
            _checkMissedMessagesTimer = new System.Timers.Timer(interval);
            _checkMissedMessagesTimer.Elapsed += CheckMissedMessages;
            _checkMissedMessagesTimer.AutoReset = true;
            _checkMissedMessagesTimer.Enabled = true;
        }
    }
}