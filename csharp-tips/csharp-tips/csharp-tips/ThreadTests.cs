using System;
using System.Threading;
using NUnit.Framework;

//
//    https://msdn.microsoft.com/en-us/library/system.threading.thread.interrupt(v=vs.110).aspx
//

namespace csharp_tips
{
    [TestFixture]
    public class ThreadTests
    {
        [Test, Repeat(100)]
        public void DemoInterrupt()
        {
            Console.WriteLine();
            ThreadInterrupt.Main();
        }

        [Test]
        public void Sync()
        {
            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/threading/thread-synchronization
            ThreadingExample.Main();
        }

        class ThreadingExample
        {
            static AutoResetEvent autoEvent;

            static void DoWork()
            {
                Console.WriteLine("   worker thread started, now waiting on event...");
                autoEvent.WaitOne();
                Console.WriteLine("   worker thread reactivated, now exiting...");
            }

            public static void Main()
            {
                autoEvent = new AutoResetEvent(false);

                Console.WriteLine("main thread starting worker thread...");
                Thread t = new Thread(DoWork);
                t.Start();

                Console.WriteLine("main thread sleeping for 1 second...");
                Thread.Sleep(1000);

                Console.WriteLine("main thread signaling worker thread...");
                autoEvent.Set();
            }
        }
    }

    public class ThreadInterrupt
    {
        public static void Main()
        {
            StayAwake stayAwake = new StayAwake();
            Thread newThread = new Thread(new ThreadStart(stayAwake.ThreadMethod));
            newThread.Start();

            // The following line causes an exception to be thrown 
            // in ThreadMethod if newThread is currently blocked
            // or becomes blocked in the future.
            newThread.Interrupt();
            Console.WriteLine("Main thread calls Interrupt on newThread.");

            // Tell newThread to go to sleep.
            stayAwake.SleepSwitch = true;

            // Wait for newThread to end.
            newThread.Join();
        }
    }

    class StayAwake
    {
        bool sleepSwitch = false;

        public bool SleepSwitch
        {
            set { sleepSwitch = value; }
        }

        public StayAwake() { }

        public void ThreadMethod()
        {
            Console.WriteLine("newThread is executing ThreadMethod.");
            while (!sleepSwitch)
            {
                // Use SpinWait instead of Sleep to demonstrate the 
                // effect of calling Interrupt on a running thread.
                Thread.SpinWait(10000000);
            }
            try
            {
                Console.WriteLine("newThread going to sleep.");

                // When newThread goes to sleep, it is immediately 
                // woken up by a ThreadInterruptedException.
                Thread.Sleep(Timeout.Infinite);
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine("newThread cannot go to sleep - interrupted by main thread.");
            }
        }
    }
}