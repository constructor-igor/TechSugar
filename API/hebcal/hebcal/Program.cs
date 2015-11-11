using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace hebcal
{
    /*
     * https://www.hebcal.com/home/category/developers
     * https://www.hebcal.com/home/195/jewish-calendar-rest-api
     * */
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder uriBuilder = new StringBuilder();
            uriBuilder
                .Append(@"http://www.hebcal.com/hebcal/")
                .Append("?v=1")
                .Append("&cfg=json")
                .Append("&maj=on")
                .Append("&min=on")
                .Append("&mod=on")
                .Append("&nx=on")
                .Append("&year=now")
                .Append("&month=11")
                .Append("&ss=on")
                .Append("&mf=on")
                .Append("&c=on")
                .Append("&geo=none")
                .Append("&geonameid=3448439")
                .Append("&m=50")
                .Append("&s=on");
            Uri baseUri = new Uri(uriBuilder.ToString());
            HttpWebRequest req = WebRequest.Create(baseUri) as HttpWebRequest;
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Stream responseStream = resp.GetResponseStream();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HebCalendar));
            HebCalendar hebCalendar = (HebCalendar)serializer.ReadObject(responseStream);

            Console.WriteLine("hebCalendar: {0}", hebCalendar);
        }
    }

    public class HebCalendarLocation
    {
        public string geonameid;
        public double latitude;
        public string tzid;
        public string geo;
        public string title;
        public string city;
        public double longitude;
    }

    public class HebCalendarLeyning
    {
        public string torah;
        public string haftarah;
    }
    public class HebCalendarItems
    {
        public string category;
        public string title;
        public string date;
        public string hebrew;
        public string link;
        public bool yomtov;
        public HebCalendarLeyning leyning;
    }
    public class HebCalendar
    {
        public string link;
        public string title;
        public string date;
        public HebCalendarLocation location;
        public HebCalendarItems[] items;

        #region Overrides of Object
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine(String.Format("link: {0}", link))
                .AppendLine(String.Format("title: {0}", title))
                .AppendLine(String.Format("date: {0}", date));
            return builder.ToString();
        }
        #endregion
    }
}
