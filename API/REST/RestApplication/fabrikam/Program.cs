using System;
using System.IO;
using System.Net;

/*
 * 
 * References:
 * https://msdn.microsoft.com/en-us/library/cc197953(v=vs.95).aspx
 * 
 * */

namespace fabrikam
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri serviceUri = new Uri("http://fabrikam.com/service/getUser");
            using (WebClient downloader = new WebClient())
            {
                downloader.OpenReadCompleted += new OpenReadCompletedEventHandler(downloader_OpenReadCompleted);
                downloader.OpenReadAsync(serviceUri);
            }
            Console.ReadLine();
        }
        static void downloader_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                using (Stream responseStream = e.Result)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseContent = streamReader.ReadToEnd();
                        Console.WriteLine("response: {0}", responseContent);
                    }
                }
            }
        }
    }
}
