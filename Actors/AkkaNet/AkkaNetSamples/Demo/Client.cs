using System;
using System.Diagnostics;
using System.Linq;

namespace Demo
{
    public class Client
    {
        private readonly Worker m_worker;

        public Client()
        {
            m_worker = new Worker();
        }

        public void Run()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (int index in Enumerable.Range(0, 100))
            {
                String response = m_worker.Run("Hello");
                Console.WriteLine(response);
            }

            stopWatch.Stop();

            Console.WriteLine("duration: {0}", stopWatch.Elapsed);
        }
    }
}