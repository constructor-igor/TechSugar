using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security.Permissions;
using NUnit.Framework;

namespace csharp_tips.DynamicProxySamples
{
    /*
     * References:
     * - https://msdn.microsoft.com/en-us/library/system.runtime.remoting.proxies.realproxy(v=VS.80).aspx
     * - http://blog.benoitblanchon.fr/realproxy/
     * 
     * */
    [TestFixture]
    public class RealProxySamples
    {
        [Test]
        public void Test()
        {
            IDoStuff actual = new ActualDoStuff();
            actual.Foo();

            RealProxySample realProxy = new RealProxySample(typeof(IDoStuff), actual);
            IDoStuff wrapper = (IDoStuff) realProxy.GetTransparentProxy();

            wrapper.Foo();
        }
    }

    //: MarshalByRefObject

    // Create a custom 'RealProxy'.
    public class RealProxySample : RealProxy
    {
        String myURIString;
        MarshalByRefObject myMarshalByRefObject;

        [PermissionSet(SecurityAction.LinkDemand)]
        public RealProxySample(Type myType, object actual)
            : base(myType)
        {
            // RealProxy uses the Type to generate a transparent proxy.
            //myMarshalByRefObject = (MarshalByRefObject)Activator.CreateInstance((myType));
            myMarshalByRefObject = (MarshalByRefObject)actual;
            // Get 'ObjRef', for transmission serialization between application domains.
            ObjRef myObjRef = RemotingServices.Marshal(myMarshalByRefObject);
            // Get the 'URI' property of 'ObjRef' and store it.
            myURIString = myObjRef.URI;
            Console.WriteLine("URI :{0}", myObjRef.URI);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        public override IMessage Invoke(IMessage myIMessage)
        {
            Console.WriteLine("MyProxy.Invoke Start");
            Console.WriteLine("");

            if (myIMessage is IMethodCallMessage)
                Console.WriteLine("IMethodCallMessage");

            if (myIMessage is IMethodReturnMessage)
                Console.WriteLine("IMethodReturnMessage");

            Type msgType = myIMessage.GetType();
            Console.WriteLine("Message Type: {0}", msgType.ToString());
            Console.WriteLine("Message Properties");
            IDictionary myIDictionary = myIMessage.Properties;
            // Set the '__Uri' property of 'IMessage' to 'URI' property of 'ObjRef'.
            myIDictionary["__Uri"] = myURIString;
            IDictionaryEnumerator myIDictionaryEnumerator =
                (IDictionaryEnumerator)myIDictionary.GetEnumerator();

            while (myIDictionaryEnumerator.MoveNext())
            {
                Object myKey = myIDictionaryEnumerator.Key;
                String myKeyName = myKey.ToString();
                Object myValue = myIDictionaryEnumerator.Value;

                Console.WriteLine("\t{0} : {1}", myKeyName,
                    myIDictionaryEnumerator.Value);
                if (myKeyName == "__Args")
                {
                    Object[] myObjectArray = (Object[])myValue;
                    for (int aIndex = 0; aIndex < myObjectArray.Length; aIndex++)
                        Console.WriteLine("\t\targ: {0} myValue: {1}", aIndex,
                            myObjectArray[aIndex]);
                }

                if ((myKeyName == "__MethodSignature") && (null != myValue))
                {
                    Object[] myObjectArray = (Object[])myValue;
                    for (int aIndex = 0; aIndex < myObjectArray.Length; aIndex++)
                        Console.WriteLine("\t\targ: {0} myValue: {1}", aIndex,
                            myObjectArray[aIndex]);
                }
            }

            IMessage myReturnMessage;

            myIDictionary["__Uri"] = myURIString;
            Console.WriteLine("__Uri {0}", myIDictionary["__Uri"]);

            Console.WriteLine("ChannelServices.SyncDispatchMessage");
            myReturnMessage = ChannelServices.SyncDispatchMessage(myIMessage);

            // Push return value and OUT parameters back onto stack.

            IMethodReturnMessage myMethodReturnMessage = (IMethodReturnMessage)
                myReturnMessage;
            Console.WriteLine("IMethodReturnMessage.ReturnValue: {0}",
                myMethodReturnMessage.ReturnValue);

            Console.WriteLine("MyProxy.Invoke - Finish");

            return myReturnMessage;
        }
    }
}