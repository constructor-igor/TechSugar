using System;
using System.Threading;
using Akka.Actor;

namespace AkkaNetSamples
{
    public class Greet
    {
        public Greet(string who)
        {
            Who = who;
        }
        public string Who { get; private set; }
    }

    // Create the actor class
    public class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            // Tell the actor to respond
            // to the Greet message
            Receive<Greet>(greet =>
               Console.WriteLine("Hello {0} ({1})", greet.Who, Thread.CurrentThread.ManagedThreadId));
        }
    }

    public class GreetingActor2 : TypedActor, IHandle<Greet>
    {
        public void Handle(Greet greet)
        {
            Console.WriteLine("Hello {0}!", greet.Who);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new actor system (a container for your actors)
            ActorSystem system = ActorSystem.Create("MySystem");

            // Create your actor and get a reference to it.
            // This will be an "ActorRef", which is not a
            // reference to the actual actor instance
            // but rather a client or proxy to it.
            IActorRef greeter = system.ActorOf<GreetingActor>("greeter");

            // Send a message to the actor
            for (int i = 0; i < 1000; i++)
            {
                Thread thread = new Thread(() => { greeter.Tell(new Greet(String.Format("World({0})", Thread.CurrentThread.ManagedThreadId))); });
                thread.Start();
            }

            // This prevents the app from exiting
            // before the async work is done
            Console.ReadLine();
        } 
    }
}
