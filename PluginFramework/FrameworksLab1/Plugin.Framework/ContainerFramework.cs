using System;
using Microsoft.Practices.Unity;

namespace Plugin.Framework
{
    public class ContainerFramework: IDisposable
    {
        readonly UnityContainer unityContainer = new UnityContainer();
        public void RegisterInstance(Type instanceType, object instance)
        {
            unityContainer.RegisterInstance(instanceType, instance);
        }
        public void RegisterInstance(Type instanceType, string instanceName, object instance)
        {
            unityContainer.RegisterInstance(instanceType, instanceName, instance);
        }
        public void Register<T>(T registeredObject)
        {
            unityContainer.RegisterInstance(registeredObject);
        }

        public T Get<T>()
        {
            return unityContainer.Resolve<T>();
        }
        public T Get<T>(string instanceName)
        {
            return unityContainer.Resolve<T>(instanceName);
        }

        public object Get(Type instanceType, string instanceName)
        {
            return unityContainer.Resolve(instanceType, instanceName);
        }

        #region IDisposable
        public void Dispose()
        {
            unityContainer.Dispose();
        }
        #endregion
    }
}