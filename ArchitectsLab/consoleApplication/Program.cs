using System;

namespace consoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ITrain train = new Train();             // with memory leak
            //ITrain train = new FixedTrain();      // without memory leak
            ExecuteProgram(train);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("program finish");
        }

        public static void ExecuteProgram(ITrain train)
        {
            RemoteControl remoteControl = new RemoteControl();
            train.Control = remoteControl;
            train.Start();
            train.Stop();
            //train.Control = null;     // memory leak
        }
    }

    public interface ITrain
    {
        RemoteControl Control { set; }
        void Start();
        void Stop();
    }

    public class Train: ITrain
    {
        private RemoteControl _control;
        public RemoteControl Control
        {
            set { _control = value; }
        }

        public Train()
        {
            Console.WriteLine("Train()");
        }

        public void Start()
        {
            // implement Start

            if (_control != null)
                _control.Notification("Start()");
        }

        public void Stop()
        {
            // implement Stop

            if (_control != null)
                _control.Notification("Stop()");
        }

        ~Train()
        {
            Console.WriteLine("~Train()");
        }
    }

    public class FixedTrain: ITrain
    {
        private WeakReference _control;
        public RemoteControl Control
        {
            set { _control = new WeakReference(value); }
        }

        public FixedTrain()
        {
            Console.WriteLine("FixedTrain()");
        }

        public void Start()
        {
            // implement Start

            if (_control != null && _control.IsAlive)
                ((RemoteControl)_control.Target).Notification("Start()");
        }

        public void Stop()
        {
            // implement Stop

            if (_control != null && _control.IsAlive)
                ((RemoteControl)_control.Target).Notification("Stop()");
        }

        ~FixedTrain()
        {
            Console.WriteLine("~FixedTrain()");
        }
    }

    public class RemoteControl
    {
        public RemoteControl()
        {
            Console.WriteLine("RemoteControl()");
        }
        public void Notification(string message)
        {
            Console.WriteLine(message);
        }

        ~RemoteControl()
        {
            Console.WriteLine("~RemoteControl()");
        }
    }

//    class Program
//    {
//        static readonly BillingService BillingService = new BillingService();
//        static void Main(string[] args)
//        {
//            ExecuteTest();
//
//            GC.Collect();
//            GC.WaitForPendingFinalizers();
//
//            Console.WriteLine("before sleep");
//            Thread.Sleep(5000);
//            Console.WriteLine("after sleep");
//        }
//
//        private static void ExecuteTest()
//        {
//            ObserverTests observerTests = new ObserverTests();
//            observerTests.RunTest(true, BillingService);
//        }
//    }
}
