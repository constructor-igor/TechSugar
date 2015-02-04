using System;
using Microsoft.Practices.Unity;

namespace UnityLab
{
    class Program
    {
        static void Main(string[] args)
        {
            InstanceWithParameter instanceWithParameter;
            using (UnityContainer unityContainer = new UnityContainer())
            {
                unityContainer.RegisterInstance(new Instance());
                InjectionMember[] injectionMembers = { new InjectionConstructor(typeof(InstanceParameter)) };
                //unityContainer.RegisterType<InstanceWithParameter>(injectionMembers);
                unityContainer.RegisterType<InstanceWithParameter>(new ContainerControlledLifetimeManager(), injectionMembers);

                instanceWithParameter = unityContainer.Resolve<InstanceWithParameter>();
            }
            Console.WriteLine("unityContainer disposed");
        }
    }
    public class Instance : IDisposable
    {
        public Instance()
        {
            Console.WriteLine("[Instance].ctor");
        }
        #region IDisposable
        public void Dispose()
        {
            Console.WriteLine("[Instance].Dispose");
        }
        #endregion
    }

    public class InstanceWithParameter : IDisposable
    {
        private readonly InstanceParameter m_instanceParameter;

        public InstanceWithParameter(InstanceParameter instanceParameter)
        {
            m_instanceParameter = instanceParameter;
            Console.WriteLine("[InstanceWithParameter].ctor");
        }

        #region IDisposable
        public void Dispose()
        {
            Console.WriteLine("[InstanceWithParameter].Dispose");
        }
        #endregion
    }

    public class InstanceParameter : IDisposable
    {
        public InstanceParameter()
        {
            Console.WriteLine("[InstanceParameter].ctor");
        }
        #region IDisposable
        public void Dispose()
        {
            Console.WriteLine("[InstanceParameter].Dispose");
        }
        #endregion
    }
}
