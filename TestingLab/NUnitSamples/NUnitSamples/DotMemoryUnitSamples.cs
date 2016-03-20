using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    public interface IFoo
    {
        
    }
    public class Foo : IFoo
    {
        
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
    }
}
