using System;
using System.Linq;
using NLog;

namespace SeqNLogSample
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Logger.Info($"started, {DateTime.Now}");
            foreach (int index in Enumerable.Range(0, 100))
            {
                Logger.Info($"Hello, {index}!");
                Logger.Warn("warning");
                if (index == 90)
                    Logger.Error("error");
                if (index == 95)
                    Logger.Fatal("fatal");
            }
            Logger.Info($"finished, {DateTime.Now}");
        }
    }
}
