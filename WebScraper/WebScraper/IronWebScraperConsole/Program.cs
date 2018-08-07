using IronWebScraper;
using System;
using System.Linq;

/*
 * https://ironsoftware.com/csharp/webscraper/
 * 
 * */

namespace IronWebScraperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new BlogScraper();
            scraper.Start();
        }
    }
    public class BlogScraper : WebScraper
    {
        public override void Init()
        {
            this.LoggingLevel = WebScraper.LogLevel.All;
            this.Request("https://modiinapp.com/en/category/115/restaurants-in-modiin", Parse);
        }

        public override void Parse(Response response)
        {
            var attributes = response.Css("div")
                                        .Where(item => item.Attributes.ContainsKey("class"))
                                        .Where(item => item.Attributes["class"].Split(' ').Contains("page-item"))
                                        .ToList();
            foreach (var attribute in attributes)
            {
                var titleNode = attribute.ChildNodes[1].ChildNodes.First(node => node.Attributes.ContainsKey("title"));
                string title = titleNode.TextContentClean;
                Console.WriteLine(title);
            }

/*
            foreach (Dictionary<string, string> attribute in attributes)
            {
                foreach (string key in attribute.Keys)
                {
                    Console.WriteLine("[{0}]: {1}", key, attribute[key]);
                }
            }
*/
//response.Document.GetElementsByTagName("page-item")
            foreach (var title_link in response.Css("h2.entry-title a"))
            {
                string strTitle = title_link.TextContentClean;
                Scrape(new ScrapedData() { { "Title", strTitle } });
            }

            if (response.CssExists("div.prev-post > a[href]"))
            {
                var next_page = response.Css("div.prev-post > a[href]")[0].Attributes["href"];
                this.Request(next_page, Parse);
            }
        }
    }
}
