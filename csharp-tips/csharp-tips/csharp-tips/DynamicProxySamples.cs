using System;
using System.Dynamic;
using ImpromptuInterface;
using NUnit.Framework;

namespace csharp_tips
{
    /*
     * References:
     * http://stackoverflow.com/questions/8387004/how-to-make-a-simple-dynamic-proxy-in-c-sharp
     * 
     * */
    [TestFixture]
    public class DynamicProxySamples
    {
        [Test]
        public void TestSimpleDynamicProxy()
        {
            IDoStuff actual = new ActualDoStuff();
            actual.Foo();

            IDoStuff wrapper = Wrapper<IDoStuff>.Wrap<IDoStuff>(actual);
            wrapper.Foo();
        }
    }

    public interface IDoStuff
    {
        void Foo();
    }

    public class ActualDoStuff : IDoStuff
    {
        #region IDoStuff
        public void Foo()
        {
            Console.WriteLine("Actual Foo");
        }
        #endregion
    }

    public class Wrapper<T> : DynamicObject
    {
        private readonly T _wrappedObject;

        public static T1 Wrap<T1>(T obj) where T1 : class
        {
            if (!typeof(T1).IsInterface)
                throw new ArgumentException("T1 must be an Interface");

            return new Wrapper<T>(obj).ActLike<T1>();
        }

        //you can make the constructor private so you are forced to use the Wrap method.
        private Wrapper(T obj)
        {
            _wrappedObject = obj;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                //do stuff here
                Console.WriteLine("wrapper <before>");

                //call _wrappedObject object
                result = _wrappedObject.GetType().GetMethod(binder.Name).Invoke(_wrappedObject, args);

                Console.WriteLine("wrapper <after>");

                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}