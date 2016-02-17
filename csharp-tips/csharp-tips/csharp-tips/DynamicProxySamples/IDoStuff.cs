using System;

namespace csharp_tips.DynamicProxySamples
{
    public interface IDoStuff
    {
        void Foo();
    }

    public class ActualDoStuff : MarshalByRefObject, IDoStuff
    {
        #region IDoStuff
        public void Foo()
        {
            Console.WriteLine("Actual Foo");
        }
        #endregion
    }

}