using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Metrics;
using Timer = Metrics.Timer;

/*
 *
 *  https://github.com/etishor/Metrics.NET/wiki/Getting-Started
 *
 */

namespace MetricsNetCmdSample
{
    public class SampleMetrics
    {
        private readonly Timer timer = Metric.Timer("Requests", Unit.Requests);
        private readonly Counter counter = Metric.Counter("ConcurrentRequests", Unit.Requests);

        public void Request(int sleepInSeconds)
        {
            this.counter.Increment();
            using (this.timer.NewContext()) // measure until disposed
            {
                Thread.Sleep(sleepInSeconds*1000);
            }
            this.counter.Decrement();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Metric.Config
                .WithHttpEndpoint("http://localhost:1234/")
                .WithAllCounters()
                .WithReporting(config => config.WithCSVReports("report", TimeSpan.FromSeconds(10)));
            
            Random rnd = new Random();
            List<int> result = Enumerable.Range(0, 20)
                .Select(index =>
                {
                    SampleMetrics metrics = new SampleMetrics();
                    int current = rnd.Next(1, 5);
                    metrics.Request(current);
                    Console.WriteLine($"#{index:00}: {current}");
                    return current;
                })
                .ToList();
            int total = result.Sum();
            Console.WriteLine($"Total {total} seconds.");

            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }
    }
}
