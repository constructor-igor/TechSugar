using System;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class DelegateSamples
    {
        delegate bool Check(int p);

        [Test]
        public void DelegateSample()
        {
            Check f0 = foo;
            Console.WriteLine("f0({0})={1}", 10, f0(10));
            Func<int, bool> f1 = foo;
            Console.WriteLine("f1({0})={1}", 10, f1(10));
            Predicate<int> f2 = foo;
            Console.WriteLine("f2({0})={1}", 10, f2(10));
        }

        bool foo(int p)
        {
            return p > 0;
        }

    }
}