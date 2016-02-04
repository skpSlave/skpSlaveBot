﻿using SKYPE4COMLib;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkypeToTwitter
{
    static class SkypeTools
    {
        private static Skype skype = new Skype();

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
                    skype.Attach(7, false);
                    Console.WriteLine("skype attached");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("top lvl exception : " + ex.ToString());
                }
            });
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
                    SendMessage(answer, pMessage.Chat);
                }
            }
            else if (status == TChatMessageStatus.cmsSent)
            {
                Console.WriteLine("[{0}] Bot [{1}]", DateTime.Now.ToString("hh:mm:ss"), pMessage.Body.Replace(Environment.NewLine, " |"));

            }
        }

        public static object _lock = new object();
        public static void SendMessage(string message, Chat toChat)
        {
            lock (_lock)
            {
                toChat.SendMessage(message);
            }
        }
    }
}