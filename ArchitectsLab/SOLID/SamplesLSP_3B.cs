using NUnit.Framework;

namespace SOLID
{
    [TestFixture]
    public class SamplesLSP_3B
    {
        [Test]
        public void Test_C()
        {
            C c = new C();
            ExecuteTest(c, new B());
            Assert.Pass();
        }
        [Test]
        public void Test_D()
        {
            D d = new D();
            ExecuteTest(d, new B());
            Assert.Pass();
        }

        void ExecuteTest(C c, B b)
        {
            c.Foo(b);
        }

        public class A
        {
        }
        public class B : A
        {
        }

        public class C
        {
            public virtual void Foo(B x)
            {
                B b = (B)x;
            }
        }
        public class D : C
        {
            public override void Foo(B x)
            {
                A a = (A)x;
            }
        }
    }
}