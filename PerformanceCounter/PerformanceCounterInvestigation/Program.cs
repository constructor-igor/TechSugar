using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace PerformanceCounterInvestigation
{
    class Program
    {
        private const int STANDARD_LOOP_SIZE = 1000;
        static void Main(string[] args)
        {
//            TestPerformanceCounter();            
//            TestProcessMemoryPerformance(getProcessInLoop: true);
//            TestProcessMemoryPerformance(getProcessInLoop: false);

//            TestNLogWithoutPerformanceCounter();
//            TestNLogWithPerformanceCounter();

            Sample_CreatingPerformanceCounter();
//            Sample_CreatingPerformanceCounter_ByStas();
        }

        //
        // http://msdn.microsoft.com/en-us/library/5e3s61wf(v=vs.90).aspx
        //
        private static void Sample_CreatingPerformanceCounter()
        {
            var performanceMonitor = new PerformanceMonitor();
            performanceMonitor.AddCounter("CustomInstance");
            using (var counter = new PerformanceCounter(PerformanceMonitor.pcCategory, "CustomInstance", readOnly: false))
            {
                string command;
                do
                {
                    counter.Increment();
                    Console.Write("enter command: ");
                    command = Console.ReadLine();
                } while (command != "");
            }

            Console.WriteLine("press any key...");
            Console.ReadKey();
        }

        private static void Sample_CreatingPerformanceCounter_ByStas()
        {
            var monitor = new PerformanceMonitorByStas();
            monitor.Run();
        }

        #region nlog and performance counters
        private static void TestNLogWithoutPerformanceCounter()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Logger logger = LogManager.GetLogger("WithoutPerformanceCounter");
            for (int i = 0; i < STANDARD_LOOP_SIZE; i++)
            {
                logger.Trace("message");    
            }
            stopwatch.Stop();

            Console.WriteLine("TestNLogWithoutPerformanceCounter: time={0}", stopwatch.ElapsedMilliseconds);
        }
        private static void TestNLogWithPerformanceCounter()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Logger logger = LogManager.GetLogger("WithPerformanceCounter");
            for (int i = 0; i < STANDARD_LOOP_SIZE; i++)
            {
                logger.Trace("message");
            }
            stopwatch.Stop();

            Console.WriteLine("TestNLogWithPerformanceCounter: time={0}", stopwatch.ElapsedMilliseconds);
        }
        #endregion

        #region nlog samples
        //
        // http://www.codeproject.com/Articles/10631/Introduction-to-NLog
        //
        private static void NLogProgramDefinitionSample()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget
            {
                Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}"
            };
            config.AddTarget("console", consoleTarget);

            var ruleTrace = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(ruleTrace);

            LogManager.Configuration = config;

            Logger logger = LogManager.GetLogger("Example");
            logger.Trace("trace log message");
            logger.Debug("debug log message");
            logger.Info("info log message");
            logger.Warn("warn log message");
            logger.Error("error log message");
            logger.Fatal("fatal log message");

        }
        #endregion

        #region Performance Counters & Process
        private static void TestPerformanceCounter()
        {
            var memoryStorage = new List<float>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var memPerformanceCounter = new PerformanceCounter("Process", "Working Set - Private", "PerformanceCounterInvestigation"))
            {
                for (int i = 0; i < STANDARD_LOOP_SIZE; i++)
                {
                    object dummy = new byte[1024 * 1024];
                    float value = memPerformanceCounter.NextValue();
                    memoryStorage.Add(value);
                }
            }
            stopwatch.Stop();

            Console.WriteLine("TestPerformanceCounter: time={0}, min = {1}, max = {2}", stopwatch.ElapsedMilliseconds, memoryStorage.Min(), memoryStorage.Max());
        }
        private static void TestProcessMemoryPerformance(bool getProcessInLoop)
        {
            var memoryStorage = new List<long>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Process process = null;
            if (!getProcessInLoop)
                process = Process.GetCurrentProcess();
            for (int i = 0; i < STANDARD_LOOP_SIZE; i++)
            {
                if (getProcessInLoop)
                    process = Process.GetCurrentProcess();
                object dummy = new byte[1024*1024];
                long privateMemorySize = process.PrivateMemorySize64;
                memoryStorage.Add(privateMemorySize);
            }
            stopwatch.Stop();

            Console.WriteLine("TestProcessMemoryPerformance(getProcessInLoop={0}): time={1}, min = {2}, max = {3}", getProcessInLoop, stopwatch.ElapsedMilliseconds, memoryStorage.Min(), memoryStorage.Max());
        }
        #endregion
    }

    public class PerformanceMonitor
    {
        public const string pcCategory = "MyActivities2";
        readonly CounterCreationDataCollection counters = new CounterCreationDataCollection();

        public PerformanceMonitor()
        {
            AddCounter("CustomInstance");
            CreateCategory(pcCategory);
        }

        public void AddCounter(string name)
        {
            var customCounter = new CounterCreationData
            {
                CounterName = name,
                CounterHelp = "Help - " + name,
                CounterType = PerformanceCounterType.NumberOfItems32,                
            };
            counters.Add(customCounter);
        }
        void CreateCategory(string category)
        {
            if (!PerformanceCounterCategory.Exists(category))
            {
                PerformanceCounterCategory.Create(category, "Help - " + category, PerformanceCounterCategoryType.SingleInstance, counters);
            }
        }
    }

    public class PerformanceMonitorByStas
    {
        public PerformanceMonitorByStas()
        {
            
        }

        public void Run()
        {
            CreateCategory();
            WriteToCounter();
        }

        void CreateCategory()
        {
            if (!PerformanceCounterCategory.Exists("Sample Category2"))
            {
                var counterData = new CounterCreationDataCollection();
                var sampleCounter = new CounterCreationData
                {
                    CounterType = PerformanceCounterType.NumberOfItems32,
                    CounterName = "SampleCounter"
                };
                counterData.Add(sampleCounter);

                PerformanceCounterCategory.Create("Sample Category2", "Simple performance counter example", PerformanceCounterCategoryType.SingleInstance, counterData);
            }
        }
        void WriteToCounter()
        {
            var counter = new PerformanceCounter("Sample Category2", "SampleCounter", false);
            var task = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Thread.Sleep(1000);
                    counter.RawValue = DateTime.Now.Second;
                    Console.WriteLine(DateTime.Now.Second);
                }
            });
            Console.ReadLine();

        }
    }
}
