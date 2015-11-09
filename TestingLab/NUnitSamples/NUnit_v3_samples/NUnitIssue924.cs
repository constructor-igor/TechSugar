using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue924
    {
        [Test]
        public async void AsyncError()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var token = source.Token;
            int i = 0;

            var t = Task.Factory.StartNew(() =>
            {

                token.Register(() => { Debug.WriteLine("End"); }, true);

                while (i++ < 20 && !token.IsCancellationRequested)
                {
                    Debug.WriteLine(i.ToString());
                    Thread.Sleep(100);
                }
            }, token, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            var t1 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                source.Cancel();
            });

            try
            {
                Task.WaitAll(t, t1);
            }
            finally
            {
                source.Dispose();
            }
        }
    }
}