using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

/*
 * http://html-agility-pack.net/
 * 
 * */

namespace HtmlAgilityPackConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://modiinapp.com/en/category/115/restaurants-in-modiin";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var nodes = doc.DocumentNode
                .Descendants("div")
                .Where(x => x.Attributes.Contains("class"))
                .Where(x => x.Attributes["class"].Value.Contains("page-item"))
                .ToList();

            List<string> titles = new List<string>();

            foreach (var node in nodes)
            {
                //
                //var titleNode = attribute.ChildNodes[1].ChildNodes.First(node => node.Attributes.ContainsKey("title"));
                foreach (var childNode in node.ChildNodes[1].ChildNodes)
                {
                    foreach (var a in childNode.Attributes)
                    {
                        if (a.Value == "title")
                        {
                            titles.Add(a.OwnerNode.InnerText);
                        }
                    }
                }
            }

            Console.WriteLine("Found {0} title", titles.Count());
            foreach (string title in titles)
            {
                Console.WriteLine("{0}", title);
            }
        }
    }
}
