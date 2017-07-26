using System.Collections.Generic;

namespace Ctor.Infra.SeparationLayer
{
    public class SeparationLayer : ISeparationLayer
    {
        readonly Dictionary<object, object> m_services = new Dictionary<object, object>();
        #region ISeparationLayer
        public void Register<T>(T service)
        {
            m_services.Add(typeof(T), service);
        }
        public T GetService<T>()
        {
            return (T)m_services[typeof(T)];
        }

        public SeparationOperation<T> CreateOperation<T>()
        {
            return new SeparationOperation<T>(GetService<T>());
        }
        #endregion
    }
}
