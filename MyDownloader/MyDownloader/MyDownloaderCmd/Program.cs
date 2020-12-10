using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

/*
 *
 * https://www.ecanarys.com/Blogs/ArticleID/77/How-to-download-YouTube-Videos-using-NET
 */

namespace MyDownloaderCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Main] started");
            DownLoader downLoader = new DownLoader("https://www.youtube.com/watch?v=J4rMSmNGwJ8");
            downLoader.Download();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Console.WriteLine("[Main] finished");
        }
    }

    public class FormatItem
    {
        public string Uri;
        public string MimeType;
        public string Quality;
        public string ITag;
        public string URL;
    }
    public class DownLoader
    {
        private readonly string m_videoUri;

        public DownLoader(string videoUri)
        {
            m_videoUri = videoUri;
        }

        public void Download()
        {
            Uri videoUri = new Uri(m_videoUri);
            string videoID = HttpUtility.ParseQueryString(videoUri.Query).Get("v");
            string videoInfoUrl = "https://www.youtube.com/get_video_info?video_id=" + videoID;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(videoInfoUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    string videoInfo = HttpUtility.HtmlDecode(reader.ReadToEnd());
                    NameValueCollection videoParams = HttpUtility.ParseQueryString(videoInfo);
                    foreach (string videoParam in videoParams)
                    {
                        Console.WriteLine($"{videoParam}: {videoParams[videoParam]}");
                    }

                    if (videoParams.Get("url_encoded_fmt_stream_map") != null)
                    {
                        string[] videoURLs = videoParams["url_encoded_fmt_stream_map"].Split(',');
                        foreach (string videoURL in videoURLs)
                        {
                            Console.WriteLine($"videoURL: {videoURL}");
                        }
                    }
                    else
                    {
                        string jsonPlayerResponse = videoParams.Get("player_response");
                        dynamic responseContent = JsonConvert.DeserializeObject(jsonPlayerResponse);
                        string title = responseContent.videoDetails.title.ToString();
                        string shortDescription = responseContent.videoDetails.shortDescription.ToString();
                        dynamic formats = responseContent.streamingData.formats;    
                        dynamic adaptiveFormats = responseContent.streamingData.adaptiveFormats;
                        List<object> allFormats = new List<object>(formats);
                        allFormats.AddRange(adaptiveFormats);
                        List<FormatItem> formatItems = allFormats
                            .Cast<dynamic>()
                            .Select(formatItemContent =>
                            {
                                string uri = formatItemContent["url"];
                                string mimeType = formatItemContent["mimeType"];
                                string quality = formatItemContent["quality"];
                                string itag = formatItemContent["itag"];
                                FormatItem formatItem = new FormatItem() {Uri = uri, MimeType = mimeType, Quality = quality, ITag = itag};
                                return formatItem;
                            })
                            .ToList();
                        Console.WriteLine($"formatItems: {formatItems.Count}");
                        formatItems.ForEach(formatItem =>
                        {
                            string sURL = HttpUtility.HtmlDecode(formatItem.Uri);
                            sURL += "&type=" + formatItem.MimeType;
                            sURL += "&title=" + HttpUtility.HtmlDecode(title);
                            formatItem.URL = sURL;
                            Console.WriteLine($"type: {formatItem.MimeType}, title: {title}");
                        });

                        FormatItem item = formatItems.First();
                        string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        string videoFormat = item.MimeType.Split(';')[0].Split('/')[1];
                        string sFilePath = string.Format(Path.Combine(downloadPath, $"Downloads\\{title}.{videoFormat}"));
                        WebClient webClient = new WebClient();
                        webClient.DownloadFileCompleted += Completed;
                        webClient.DownloadFileAsync(new Uri(item.URL), sFilePath);
                    }
                }
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("download finished");
        }
    }
}
