using System;
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

            ISeparationLayer separationLayer = SeparationLayerFactory.Create();
            IAddService addService = separationLayer.GetService<IAddService>();
            double x = 3;
            double y = 5;
            double r = addService.Add(3, 5);
            Console.WriteLine("[addService demo] {0} + {1} => {2}", x, y, r);

            x = 30;
            y = 50;
            SeparationOperation<IAddService> addServiceOperation1 = separationLayer.CreateOperation<IAddService>();
            addServiceOperation1
                .Perform(() => { r = addServiceOperation1.Service.Add(x, y); })
                .Synchronously()
                .PostOperation();
            Console.WriteLine("[addService demo] {0} + {1} => {2}", x, y, r);

            x = 300;
            y = 500;
            SeparationOperation<IAddService> addServiceOperation2 = separationLayer.CreateOperation<IAddService>();
            IAsyncOperation operation = addServiceOperation2
                .Perform(() => { r = addServiceOperation2.Service.Add(x, y); })
                .Asynchronously()
                .PostOperation();
            Console.WriteLine("waiting, r={0}", r);
            operation.Wait();
            Console.WriteLine("[addService demo] {0} + {1} => {2}", x, y, r);

            Console.WriteLine("Client completed.");
        }
    }
}
