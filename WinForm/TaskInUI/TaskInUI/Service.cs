using System;
using System.Linq;
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
        public void DoWithProgress(int seconds, Action<int> progress, CancellationToken token)
        {
            foreach (int currentSecond in Enumerable.Range(0, seconds))
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                progress(currentSecond);
                Thread.Sleep(S1);
            }
            progress(seconds);
        }
    }
}