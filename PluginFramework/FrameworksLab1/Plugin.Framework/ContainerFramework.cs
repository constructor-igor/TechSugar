using System;
using Microsoft.Practices.Unity;

namespace Plugin.Framework
{
    public class ContainerFramework: IDisposable
    {
        readonly UnityContainer unityContainer = new UnityContainer();
        public void Add<T>(T registeredObject)
        {
            unityContainer.RegisterInstance(registeredObject);
        }

        public T Get<T>()
        {
            return unityContainer.Resolve<T>();
        }
        #region IDisposable
        public void Dispose()
        {
            unityContainer.Dispose();
        }
        #endregion
    }
}