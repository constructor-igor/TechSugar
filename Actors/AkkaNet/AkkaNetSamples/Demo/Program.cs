using System;
using Akka.Actor;
using Akka.Routing;

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

            demoSystem.ActorOf(Props.Create<MailActor>().WithRouter(new RoundRobinPool(2)), "mail");
            demoSystem.ActorOf(Props.Create<RepositoryActor>().WithRouter(new RoundRobinPool(2)), "repository");
            
            IActorRef workerActor = demoSystem.ActorOf(Props.Create<WorkerActor>(), "worker");            
            demoSystem.ActorOf(Props.Create(()=>new ClientActor(workerActor)), "client");

            Console.ReadLine();
        }
    }
}
