using System;
using Nest;
using NLog;

namespace cs_sample
{
    public class Command{
        public string name;
        public int id;
    }

    class Program
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static void ConfigLog(){
            var config = new NLog.Config.LoggingConfiguration();
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            NLog.LogManager.Configuration = config;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("[ElasticSearch Client] started");

            // ConfigLog();
            Logger.Info("Hello, Logger");
            Logger.Error("Hello, Elastic");

            string host = @"http://localhost:9200";
            var node = new Uri(host);
            var settings = new ConnectionSettings(node)
                //.DefaultIndex("commands*")
                .DefaultMappingFor<Command>(m=>m.IndexName("commands"))
                ;
            var client = new ElasticClient(settings);
            PingResponse pingResponse = client.Ping();
            // var response = client.ClusterHealth();
            Console.WriteLine($"pingResponse = {pingResponse}");

            //SearchResponse<Command> searchResponse = client.Search<Command>(s=>s.Index("commands"));
            SearchResponse<Command> searchResponse = client.Search<Command>();
            Console.WriteLine($"searchResponse = {searchResponse}");
            Console.WriteLine($"searchResponse: found {searchResponse.Total} records.");
            // foreach(object hit in searchResponse.Hits){
            //     Console.WriteLine($"{hit}");
            // }

            Command command = new Command(){name = "mult", id=13};
            var indexResponse = client.IndexDocument(command);
            Console.WriteLine($"indexResponse ({indexResponse.IsValid}) = {indexResponse}");

            command = new Command(){name = "mult-async", id=15};
            var indexAsycResponse = client.IndexDocumentAsync(command);
            Console.WriteLine($"indexAsycResponse (id: {indexAsycResponse.Id}) = {indexAsycResponse}");

            // Console.Read();
            Console.WriteLine("[ElasticSearch Client] finished");
        }
    }
}
