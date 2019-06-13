using System;
using Nest;

namespace cs_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string host = @"http://localhost:9200";
            var node = new Uri(host);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            PingResponse pingResponse = client.Ping();
            // var response = client.ClusterHealth();
            Console.WriteLine(pingResponse);
            // Console.Read();
            Console.WriteLine("finished");
        }
    }
}
