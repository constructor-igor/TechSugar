using System;
using System.Threading;
using Akka.Actor;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //LegacyExecution();
            ActorExecution();
        }

        private static void LegacyExecution()
        {
            Client client = new Client();
            Thread t1 = new Thread(() => client.Run());
            Thread t2 = new Thread(() => client.Run());

            t1.Start();
            t2.Start();
        }

        private static void ActorExecution()
        {
            ActorSystem demoSystem = ActorSystem.Create("demo");
            IActorRef workerActor = demoSystem.ActorOf(Props.Create<WorkerActor>(), "worker");
            demoSystem.ActorOf(Props.Create(()=>new ClientActor(workerActor)));

            Console.ReadLine();
        }
    }
}
