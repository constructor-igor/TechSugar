using NUnit.Framework;
using System;

namespace proxyExample
{
    public interface IVehicle
    {
        void start();
    }

    public class Car : IVehicle
    {
        private String name;

        public Car(String name)
        {
            this.name = name;
        }

        public void start()
        {
            Console.WriteLine("Car " + name + " started");
        }
    }

    public class VehicleProxy : IVehicle
    {
        IVehicle _vehicle;

        public VehicleProxy(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public void start()
        {
            Console.WriteLine("VehicleProxy: Begin of start()");
            _vehicle.start();
            Console.WriteLine("VehicleProxy: End of start()");
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return _vehicle.GetHashCode();
        }
    }

    [TestFixture]
    public class TestProxyEquals
    {
        [Test]
        public void CompareTwoProxies()
        {
            IVehicle c = new Car("Botar");
            IVehicle v1 = new VehicleProxy(c);
            IVehicle v2 = new VehicleProxy(c);
            Assert.That(v1.Equals(v2), Is.True);
        }
    }
}