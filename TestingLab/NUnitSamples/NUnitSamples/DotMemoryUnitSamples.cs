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
        private byte[] m_data;
        #region Implementation of IFoo
        public void Run()
        {
            m_data = new byte[1024];
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
            var memoryCheckpoint1 = dotMemory.Check();

            foo.Run();

            var memoryCheckPoint2 = dotMemory.Check(memory =>
            {
                Assert.That(
                    memory.GetTrafficFrom(memoryCheckpoint1)
                        .Where(obj => obj.Interface.Is<IFoo>())
                        .AllocatedMemory.SizeInBytes, Is.LessThan(1000));
            });
        }
    }
}
