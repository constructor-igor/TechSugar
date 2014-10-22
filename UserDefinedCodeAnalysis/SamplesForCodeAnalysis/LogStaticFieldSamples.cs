namespace SamplesForCodeAnalysis
{
    public interface ILog
    {
        
    }

    public class LogStaticFieldSamples
    {
        private static ILog m_logger1 = LogManager.GetInstance();
        private ILog m_logger2 = LogManager.GetInstance();

        private const int static_sample = 0;
        public int CurrentValue { get; private set; }

        public LogStaticFieldSamples()
        {
            CurrentValue = static_sample;
        }
    }

    public static class LogManager
    {
        public static ILog GetInstance()
        {
            return null;
        }
    }
}
