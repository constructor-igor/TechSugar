using System;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace UnityTests
{
    //
    //  https://msdn.microsoft.com/en-us/library/ff650130.aspx
    //

    [TestFixture]
    public class UnityChildLifetimeSamples
    {
        [Test]
        public void Test()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<MyObject>(new ContainerControlledLifetimeManager());

            IUnityContainer childContainer = container.CreateChildContainer();
            
            var a1 = childContainer.Resolve<MyObject>();
            var a2 = container.Resolve<MyObject>();

            Assert.AreSame(a1, a2);
        }
        [Test]
        public void ContainerControlledLifetimeManager_Child_Equal_To_Parent()
        {
            // Create parent container
            IUnityContainer parentCtr = new UnityContainer();

            // Register type in parent container
            parentCtr.RegisterType<MyObject>(new ContainerControlledLifetimeManager());

            // Create nested child container in parent container
            IUnityContainer childCtr = parentCtr.CreateChildContainer();

            MyObject parentObject1 = parentCtr.Resolve<MyObject>();
            MyObject parentObject2 = parentCtr.Resolve<MyObject>();
            Assert.That(parentObject2.GetHashCode(), Is.EqualTo(parentObject1.GetHashCode()), "paren1 != parent2");

            MyObject childObject = childCtr.Resolve<MyObject>();
            Assert.That(childObject.GetHashCode(), Is.EqualTo(parentObject1.GetHashCode()), "child != parent");

            // Dispose child container
            childCtr.Dispose();

            // Dispose parent container
            parentCtr.Dispose();
        }

        [Test]
        public void HierarchicalLifetimeManager_Child_Not_Equal_To_Parent()
        {
            // Create parent container
            IUnityContainer parentCtr = new UnityContainer();

            // Register type in parent container
            parentCtr.RegisterType<MyObject>(new HierarchicalLifetimeManager());

            // Create nested child container in parent container
            IUnityContainer childCtr = parentCtr.CreateChildContainer();

            MyObject parentObject1 = parentCtr.Resolve<MyObject>();
            MyObject parentObject2 = parentCtr.Resolve<MyObject>();
            Assert.That(parentObject2.GetHashCode(), Is.EqualTo(parentObject1.GetHashCode()), "paren1 != parent2");

            MyObject childObject = childCtr.Resolve<MyObject>();
            Assert.That(childObject.GetHashCode(), Is.Not.EqualTo(parentObject1.GetHashCode()), "child != parent");

            // Dispose child container
            childCtr.Dispose();

            // Dispose parent container
            parentCtr.Dispose();
        }

        public class MyObject
        {
            public MyObject()
            {
                Console.WriteLine("created MyObject");
            }
        }
    }
}