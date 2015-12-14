using NUnit.Framework;

namespace SOLID
{
    [TestFixture]
    public class SamplesLSP_3A
    {
        [Test]
        public void Test_C()
        {
            C c = new C();
            ExecuteTest(c, new A());
            Assert.Pass();
        }
        [Test]
        public void Test_D()
        {
            D d = new D();
            ExecuteTest(d, new A());
            Assert.Pass();
        }

        void ExecuteTest(C c, A a)
        {
            c.Foo(a);
        }

        public class A
        {
        }
        public class B : A
        {
        }

        public class C
        {
            public virtual void Foo(A x)
            {
                A a = (A)x;
            }
        }
        public class D : C
        {
            public override void Foo(A x)
            {
                B b = (B)x;
            }
        }
    }
}
