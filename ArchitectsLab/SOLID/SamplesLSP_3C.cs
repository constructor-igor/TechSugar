using NUnit.Framework;

namespace SOLID
{
    [TestFixture]
    public class SamplesLSP_3C
    {
        [Test]
        public void Test_C()
        {
            C c = new C();
            A a = ExecuteTest(c);
        }
        [Test]
        public void Test_D()
        {
            D d = new D();
            A a = ExecuteTest(d);            
        }

        A ExecuteTest(C c)
        {
            A a = c.Foo();
            Assert.That(a, Is.InstanceOf<A>());
            return a;
        }

        public class A
        {
        }
        public class B : A
        {
        }

        public class C
        {
            public virtual A Foo()
            {
                return new A();
            }
        }
        public class D : C
        {
            public override A Foo()
            {
                return new B();
            }
        }
    }
}