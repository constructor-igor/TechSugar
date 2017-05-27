using NLog;
using Project1;
using Project2;

namespace NlogSample
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Main started");
            Project1Library library1 = new Project1Library();
            Project2Library library2 = new Project2Library();

            library1.Run();
            library1.Run();
            library2.Run();

            logger.Info("Main complted");
        }
    }
}
