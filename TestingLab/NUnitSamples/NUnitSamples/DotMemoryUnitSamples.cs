using System;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    public interface IFoo
    {
        void Run();
    }
    public class Foo : IFoo
    {
        public byte[] Data;
        #region Implementation of IFoo
        public void Run()
        {
            Data = new byte[1024];
        }

        #endregion
    }

    [TestFixture]
    public class DotMemoryUnitSamples
    {
        [Test]
        public void NoFoo_ObjectsCount_0()
        {
            dotMemory.Check(memory =>
            {
                Assert.That(memory.GetObjects(where=>where.Type.Is<Foo>()).ObjectsCount, Is.EqualTo(0));
            });
        }
        [Test]
        public void SingleFoo_ObjectsCount_1()
        {
            Foo foo = new Foo();
            dotMemory.Check(memory =>
            {
                Assert.That(memory.GetObjects(where => where.Type.Is<Foo>()).ObjectsCount, Is.EqualTo(1));
            });
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000)]
        [Test]
        public void MemoryAllocationTest()
        {
            //byte[] data = new byte[1024];
        }

        [DotMemoryUnit(CollectAllocations = true)]
        [Test]
        public void MemoryAllocationTrendTest()
        {
            IFoo foo = new Foo();
            MemoryCheckPoint memoryCheckpoint1 = dotMemory.Check();

            foo.Run();
            IFoo foo1 = new Foo();
            IFoo foo2 = new Foo();

            var memoryCheckPoint2 = dotMemory.Check(memory =>
            {
                long actualObjectsCount = memory
                    .GetTrafficFrom(memoryCheckpoint1)
                    .Where(obj => obj.Interface.Is<IFoo>()).AllocatedMemory.ObjectsCount;
                    
//                long actualObjectsCount = memory
//                    .GetTrafficFrom(memoryCheckpoint1)
//                    .Where(obj => obj.Interface.Is<IFoo>())
//                    .AllocatedMemory;
                Console.WriteLine("CollectedMemory.ObjectCount: {0}", actualObjectsCount);
                Assert.That(actualObjectsCount, Is.EqualTo(2));
            });

            Assert.That(foo, Is.Not.Null);
            Assert.That(foo1, Is.Not.Null);
            Assert.That(foo2, Is.Not.Null);
        }
    }
}
