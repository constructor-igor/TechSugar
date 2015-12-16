using NUnit.Framework;

namespace SOLID
{
    [TestFixture]
    public class SamplesLSP_3E
    {
        [Test]
        public void Test_C()
        {
            C c = new C();
            ExecuteTest(c);
        }
        [Test]
        public void Test_D()
        {
            D d = new D();
            ExecuteTest(d);
        }

        void ExecuteTest(C c)
        {
            c.Foo();
        }

        public class A
        {
        }
        public class B : A
        {
        }

        public class C
        {
            public virtual void Foo()
            {
            }
        }
        public class D : C
        {
            public override void Foo()
            {
                Assert.Fail("cannot be created with 'protected'");
            }
        }
    }
}