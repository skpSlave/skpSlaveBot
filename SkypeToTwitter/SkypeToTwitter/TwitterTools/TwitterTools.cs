using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TweetSharp;
using System.Linq;

namespace SkypeToTwitter
{
    public static class TwitterTools
    {
        private static string _consumerKey = String.Empty;
        private static string _consumerSecret = String.Empty;
        private static string _accessToken = String.Empty;
        private static string _accessTokenSecret = String.Empty;

        private static TwitterService InitializeTwitterService()
        {
            ReadSettigs();

            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);

            return service;
        }

        private static void ReadSettigs()
        {
            var lines = File.ReadAllLines("Settings/TwitterAuth.txt").ToList();

            _consumerKey = lines.Where(x => x.Contains("_consumerKey")).FirstOrDefault().Split(new[] { " = " }, StringSplitOptions.None)[1];
            _consumerSecret = lines.Where(x => x.Contains("_consumerSecret")).FirstOrDefault().Split(new[] { " = " }, StringSplitOptions.None)[1];
            _accessToken = lines.Where(x => x.Contains("_accessToken")).FirstOrDefault().Split(new[] { " = " }, StringSplitOptions.None)[1];
            _accessTokenSecret = lines.Where(x => x.Contains("_accessTokenSecret")).FirstOrDefault().Split(new[] { " = " }, StringSplitOptions.None)[1];
        }

        public static void SendMessage(MessageEntity message)
        {
            TwitterService service = InitializeTwitterService();

            //check if message contains image url
            //if (Constants.IMAGE_EXTS.Where(x => message.Message.IndexOf(x, StringComparison.OrdinalIgnoreCase) > -1).Count() > 0)
            //{
            //    status = SendImageMessage(service, message);
            //}
            //else
            //{
            //    status = SendTextMessage(service, message);
            //}

            List<TwitterStatus> statuses = SendTextMessage(service, message);
        }

        private static List<TwitterStatus> SendTextMessage(TwitterService service, MessageEntity message)
        {
            List<TwitterStatus> statuses = new List<TwitterStatus>();

            String hashTag = String.Format("{0}{1}", "#LiveSkypeChat", Environment.NewLine); //TODO: module - defining current top hashtag

            List<string> splittedTwitterMessage = SplitMessage(message.TwitterMessage, hashTag);

            int messgaeIndex = 0;

            foreach (string messagePart in splittedTwitterMessage)
            {
                String twitterMessage = String.Empty;
                messgaeIndex++;

                if (splittedTwitterMessage.Count > 1)
                {
                    String counter = String.Format("{0}/{1}{2}", messgaeIndex, splittedTwitterMessage.Count, Environment.NewLine);
                    twitterMessage = String.Format("{0}{1}{2}: {3}", hashTag, counter, message.TwitterNick, messagePart);
                }
                else
                {
                    twitterMessage = String.Format("{0}{1}: {2}", hashTag, message.TwitterNick, messagePart);
                }

                TwitterStatus status = service.SendTweet(new SendTweetOptions { Status = twitterMessage });
                statuses.Add(status);
            }

            return statuses;
        }

        private static TwitterStatus SendImageMessage(TwitterService service, MessageEntity message)
        {
            TwitterStatus status = null;

            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(message.Message);
                WebResponse myResp = myReq.GetResponse();

                using (Stream stream = myResp.GetResponseStream())
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024];
                        count = stream.Read(buf, 0, 1024);
                        ms.Write(buf, 0, count);
                    } while (stream.CanRead && count > 0);

                    status = service.SendTweetWithMedia(new SendTweetWithMediaOptions()
                    {
                        Status = message.Message,
                        DisplayCoordinates = false,
                        Images = new Dictionary<string, Stream>()
                                              {
                                                  {message.TwitterNick,  ms}
                                              }
                    });
                }
            }
            catch (Exception ex)
            {
                Extentions.ConsoleWriteLine(ex.Message, ConsoleColor.Red);
            }

            return status;
        }

        private static List<string> SplitMessage(string message, string hashTag)
        {
            List<string> parts = new List<string>();

            if (message.Length > 130 - hashTag.Length)
            {
                int maxMessageLength = 130 - (hashTag.Length + (5 + Environment.NewLine.Length));

                bool lastMessage = false;

                do
                {
                    string part = String.Empty;

                    if (message.Length > maxMessageLength)
                    {
                        part = message.Substring(0, maxMessageLength);
                        message = message.Remove(0, maxMessageLength);
                    }
                    else
                    {
                        part = message;
                        lastMessage = true;
                    }

                    parts.Add(part);
                }
                while (lastMessage != true);
            }
            else
            {
                parts.Add(message);
            }

            return parts;
        }
    }
}