using System;
using System.Diagnostics;
using System.Linq;
using Akka.Actor;

namespace Demo
{
    public class ClientActor : UntypedActor
    {
        readonly Stopwatch m_stopWatch = new Stopwatch();
        private readonly IActorRef m_worker;

        public ClientActor(IActorRef worker)
        {
            m_worker = worker;
        }

        protected override void PreStart()
        {            
            m_stopWatch.Start();

            foreach (int index in Enumerable.Range(0, 100))
            {
                m_worker.Tell(new UserMessage("Hello"), Self);
            }
        }

        protected override void OnReceive(object message)
        {
            ResponseMessage responseMessage = message as ResponseMessage;
            if (responseMessage != null)
                Console.WriteLine(responseMessage.Response + " >" + m_stopWatch.Elapsed);           
        }
    }
}