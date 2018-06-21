using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ASyncSamples
    {
        // https://stackoverflow.com/questions/14455293/how-and-when-to-use-async-and-await
        [Test]
        public async void ASyncTest()
        {
            Console.WriteLine(DateTime.Now);

            // This block takes 1 second to run because all
            // 5 tasks are running simultaneously
            {
                Task a = Task.Delay(1000);
                Task b = Task.Delay(1000);
                Task c = Task.Delay(1000);
                Task d = Task.Delay(1000);
                Task e = Task.Delay(1000);

                await a;
                await b;
                await c;
                await d;
                await e;
            }

            Console.WriteLine(DateTime.Now);

            // This block takes 5 seconds to run because each "await"
            // pauses the program until the task finishes
            {
                await Task.Delay(1000);
                await Task.Delay(1000);
                await Task.Delay(1000);
                await Task.Delay(1000);
                await Task.Delay(1000);
            }
            Console.WriteLine(DateTime.Now);
        }
    }
}