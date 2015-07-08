using System;
using System.Diagnostics;
using System.Linq;
using Akka.Actor;

namespace Demo
{
    public class ClientActor : ReceiveActor
    {
        readonly Stopwatch m_stopWatch = new Stopwatch();
        private readonly IActorRef m_worker;

        public ClientActor(IActorRef worker)
        {
            m_worker = worker;
            Receive<ResponseMessage>(responseMessage => Console.WriteLine(responseMessage.Response + " >" + m_stopWatch.Elapsed));
        }

        protected override void PreStart()
        {            
            m_stopWatch.Start();

            Enumerable.Range(0, 100).ToList().ForEach(i=> { m_worker.Tell(new UserMessage("Hello"), Self); });
        }
    }
}