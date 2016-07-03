using log4net;

/*
 *  References:
 *  - https://csharp.today/log4net-tutorial-great-library-for-logging/
 * 
 * */

namespace Log4NetSample
{
    class Program
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ILog InvestigationLog = log4net.LogManager.GetLogger("investigation");

        static void Main(string[] args)
        {
            Log.Info("your log message");
            InvestigationLog.Info("investigate INFO");
            InvestigationLog.Debug("investigate DEBUG");
        }
    }
}
