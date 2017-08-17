using System;
using System.Threading;
using Ctor.Infra.SeparationLayer;
using Ctor.Server.Impl;
using Ctor.Server.Interfaces.Services;

namespace Ctor.Client.Impl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client started.");

            Console.WriteLine("==== simple sample ================================================================");

            ISeparationLayer separationLayer = SeparationLayerFactory.Create();
            IAddService addService = separationLayer.GetService<IAddService>();
            double x = 3;
            double y = 5;
            double r = addService.Add(3, 5);
            Console.WriteLine("[addService demo] {0} + {1} => {2}", x, y, r);

            Console.WriteLine("==== Synchronously sample ================================================================");

            x = 30;
            y = 50;
            SeparationOperation<IAddService> addServiceOperation1 = separationLayer.CreateOperation<IAddService>();
            addServiceOperation1
                .Perform(() => { r = addServiceOperation1.Service.Add(x, y); })
                .Synchronously()
                .OnFinishInvoke(() => Console.WriteLine("finish"))
                .PostOperation();
            Console.WriteLine("[addService demo] {0} + {1} => {2}", x, y, r);

            Console.WriteLine("==== Asynchronously sample ================================================================");

            x = 300;
            y = 500;
            SeparationOperation<IAddService> addServiceOperation2 = separationLayer.CreateOperation<IAddService>();
            IAsyncOperation operation2 = addServiceOperation2
                .Perform(() => { r = addServiceOperation2.Service.Add(x, y); })
                .Asynchronously()
                .OnFinishInvoke(() => Console.WriteLine("finish"))
                .PostOperation();
            Console.WriteLine("waiting, r={0}", r);
            operation2.Wait();
            Console.WriteLine("[addService demo] {0} + {1} => {2}", x, y, r);

            Console.WriteLine("==== Cancel/Progress sample ================================================================");

            x = 3000;
            y = 5000;
            r = 0;
            SeparationOperation<IAddService> addServiceOperation3 = separationLayer.CreateOperation<IAddService>();
            IAsyncOperation operation3 = addServiceOperation3
                .Perform(() => { r = addServiceOperation3.Service.Add(x, y); })
                .Asynchronously()
                .OnErrorInvoke(e=>Console.WriteLine("Error: {0}", e.Message))
                .OnCancelInvoke(()=>Console.WriteLine("cancel"))
                .OnProgressInvoke(progressMessage=>Console.WriteLine("{0} from {1}", progressMessage.Progress, progressMessage.Total))
                .OnFinishInvoke(()=> Console.WriteLine("finish"))
                .PostOperation();
            Console.WriteLine("waiting, r={0}", r);
            Thread.Sleep(3000);
            operation3.Cancel();
            operation3.Wait();

            Console.WriteLine("==== Error sample ================================================================");

            SeparationOperation<IAddService> addServiceOperation4 = separationLayer.CreateOperation<IAddService>();
            IAsyncOperation operation4 = addServiceOperation4
                .Perform(() => { r = addServiceOperation3.Service.Add(0, 0); })
                .Asynchronously()
                .OnErrorInvoke(e => Console.WriteLine("Error: {0}", e.Message))
                .OnCancelInvoke(() => Console.WriteLine("cancel"))
                .OnFinishInvoke(() => Console.WriteLine("finish"))
                .PostOperation();
            Console.WriteLine("waiting, r={0}", r);
            operation4.Wait();

            Console.WriteLine("Client completed.");
        }
    }

//    public static class SeparationOperationExtensions
//    {
//        [Obsolete("You should never ever invoke the callbacks on the Main thread. This indicates a problem with your code")]
//        public static SeparationOperation<T> CallbacksThroughUiThread<T>(this SeparationOperation<T> sequence)
//        {
//            return sequence.CallbacksThrough((action, objects) =>
//                (Application.Current != null ? Application.Current.Dispatcher : Dispatcher.CurrentDispatcher).Invoke(action, objects));
//        }
//    }
}
