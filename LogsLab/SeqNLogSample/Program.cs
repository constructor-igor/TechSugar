using System.Linq;
using NLog;

namespace SeqNLogSample
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            foreach (int index in Enumerable.Range(0, 100))
            {
                Logger.Info($"Hello, {index}!");
            }            
        }
    }
}
