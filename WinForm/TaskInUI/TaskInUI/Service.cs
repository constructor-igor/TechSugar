using System.Threading;

namespace TaskInUI
{
    public class Service
    {
        public const int S1 = 1000;
        public void Do(int seconds)
        {
            Thread.Sleep(seconds*S1);
        }
    }
}