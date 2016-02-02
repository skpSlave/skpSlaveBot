using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace SkypeToTwitter
{
    static class Weather
    {
        public static string GetWeather()
        {
            return GetWeather("Kharkiv");
        }

        public static string GetWeather(string cityName)
        {
            StringBuilder result = new StringBuilder();

            WeatherEnity weather = WeatherParser.GetCurrent(cityName);

            result.AppendLine("Погода в: ");
            result.Append(weather.CityName);
            result.Append(", ");
            result.Append(weather.CountryName);
            result.Append(Environment.NewLine);
            result.AppendLine("Температура: ");
            result.Append(weather.Temperature);

            return result.ToString();
        }
    }

    public class WeatherEnity
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string Temperature { get; set; }
    }

    public static class WeatherParser
    {
        private const string UrlCurrent = "http://api.openweathermap.org/data/2.5/weather?mode=xml&appid=81b505700bab55832dd6c64938f58487&units=metric";

        public static WeatherEnity GetCurrent(string cityName)
        {
            WeatherEnity wn = new WeatherEnity();

            string apiResult = CallRestMethod(UrlCurrent, cityName);

            if (String.IsNullOrEmpty(apiResult) || apiResult.Contains("Error"))
                return wn;

            XDocument doc = XDocument.Parse(apiResult);

            if (doc.Root != null)
            {
                IEnumerable<XElement> childList = doc.Root.Elements().ToList();

                XElement cityNode = childList.FirstOrDefault(e => e.Name == "city");
                if (cityNode != null)
                {
                    wn.CityName = cityNode.Attribute("name").Value;

                    XElement countryNode = cityNode.Elements().FirstOrDefault(el => el.Name == "country");

                    if (countryNode != null && !String.IsNullOrEmpty(countryNode.Value))
                    {
                        wn.CountryName = countryNode.Value;
                    }
                }

                XElement tempNode = childList.FirstOrDefault(e => e.Name == "temperature");
                if (tempNode != null && !String.IsNullOrEmpty(tempNode.Attribute("value").Value))
                {
                    wn.Temperature = tempNode.Attribute("value").Value;
                }
            }

            return wn;
        }

        private static string CallRestMethod(string url, string city)
        {
            url += "&q=" + city;

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webresponse;
            try
            {
                webresponse = (HttpWebResponse) webrequest.GetResponse();
            }
            catch
            {
                return String.Empty;
            }
            Encoding enc = Encoding.GetEncoding("utf-8");
            Stream resp = webresponse.GetResponseStream();

            if (resp == null)
                return String.Empty;

            StreamReader responseStream = new StreamReader(resp, enc);
            var result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }
    }
}

