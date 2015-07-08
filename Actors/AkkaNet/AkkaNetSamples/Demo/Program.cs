using System;
using System.Diagnostics;
using System.Linq;
using Akka.Actor;
using Akka.Routing;

namespace Demo
{
    //
    //  TODO
    //  - remoting (http://getakka.net/docs/Remoting)
    //
    class Program
    {
        static void Main(string[] args)
        {
            //SequentialExecution();
            ActorExecution();
        }

        private static void ActorExecution()
        {
            ActorSystem demoSystem = ActorSystem.Create("demo");

            demoSystem.ActorOf(Props.Create<MailActor>().WithRouter(new RoundRobinPool(2)), "mail");
            demoSystem.ActorOf(Props.Create<RepositoryActor>().WithRouter(new RoundRobinPool(2)), "repository");

            //demoSystem.ActorOf(Props.Create<MailActor>(), "mail");
            //demoSystem.ActorOf(Props.Create<RepositoryActor>(), "repository");
            
            IActorRef workerActor = demoSystem.ActorOf(Props.Create<WorkerActor>(), "worker");            
            demoSystem.ActorOf(Props.Create(()=>new ClientActor(workerActor)), "client");

            Console.ReadLine();
        }

        private static void SequentialExecution()
        {
            Stopwatch stopWatch = new Stopwatch();
            int counter = 0;
            Repository repository = new Repository();
            MailService mailService = new MailService();

            stopWatch.Start();
            Enumerable.Range(0, 100).ToList().ForEach(i =>
            {
                string response = String.Format("{0}: {1}", counter, "Hello");
                counter++;

                repository.Save(response);
                mailService.Send(response);
                Console.WriteLine(response + " >" + stopWatch.Elapsed);
            });
        }
    }
}
