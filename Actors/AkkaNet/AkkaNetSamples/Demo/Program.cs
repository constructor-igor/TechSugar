using System;
using Akka.Actor;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorExecution();
        }

        private static void ActorExecution()
        {
            ActorSystem demoSystem = ActorSystem.Create("demo");            
            demoSystem.ActorOf<MailActor>("mail");
            demoSystem.ActorOf<RepositoryActor>("repository");
            IActorRef workerActor = demoSystem.ActorOf(Props.Create<WorkerActor>(), "worker");            
            demoSystem.ActorOf(Props.Create(()=>new ClientActor(workerActor)), "client");

            Console.ReadLine();
        }
    }
}
