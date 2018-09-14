using System;
using NUnit.Framework;

namespace csharp_tips
{
    public delegate bool MyDelegate(int p);

    [TestFixture]
    public class DelegateSamples
    {       
        [Test]
        public void DelegateSample_SingleMethod()
        {
            MyDelegate f0 = foo1;
            Console.WriteLine("f0({0})={1}", 10, f0(10));
            Func<int, bool> f1 = foo1;
            Console.WriteLine("f1({0})={1}", 10, f1(10));
            Predicate<int> f2 = foo1;
            Console.WriteLine("f2({0})={1}", 10, f2(10));
        }
        [Test]
        public void DelegateSample_MultiMethod()
        {
            MyDelegate fooDelegate = foo1;
            Console.WriteLine("1 method");
            fooDelegate(10);
            fooDelegate += foo2;
            Console.WriteLine("2 methods");
            fooDelegate(20);
            fooDelegate -= foo2;
            Console.WriteLine("1 method");
            fooDelegate(30);
        }

        [Test]
        public void DelegateSample_Manager()
        {
            MyDelegate fooDelegate = foo1;
            fooDelegate += foo2;
            Manager manager = new Manager(fooDelegate);
            int result = manager.Run(10, 20);
            Console.WriteLine($"Run(10, 20) = {result}");
        }

        bool foo1(int p)
        {
            bool result = p > 0;
            Console.WriteLine($"foo1({p}) = {result}");
            return result;
        }
        bool foo2(int p)
        {
            bool result = p > 0;
            Console.WriteLine($"foo2({p}) = {result}");
            return result;
        }
    }

    public class Manager
    {
        private readonly MyDelegate m_myDelegateMethod;
        public Manager(MyDelegate myDelegateMethod)
        {
            m_myDelegateMethod = myDelegateMethod;
        }

        public int Run(int x, int y)
        {
            int result = x + y;
            m_myDelegateMethod(result);
            return result;
        }
    }
}