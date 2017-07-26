using System;

namespace Ctor.Infra.SeparationLayer
{
    public class SeparationOperation<T>
    {
        private bool m_synchronously = false;
        private Action m_action;

        public T Service { get; }

        public SeparationOperation(T service)
        {
            Service = service;
        }

        public SeparationOperation<T> Perform(Action action)
        {
            m_action = action;
            return this;
        }

        public SeparationOperation<T> Asynchronously()
        {
            m_synchronously = false;
            return this;
        }
        public SeparationOperation<T> Synchronously()
        {
            m_synchronously = true;
            return this;
        }

        public void PostOperation()
        {
            if (m_synchronously)
            {
                m_action();
            } else
            {
                throw new NotImplementedException();
            }
        }
    }
    public interface ISeparationLayer
    {
        void Register<T>(T service);
        T GetService<T>();
        SeparationOperation<T> CreateOperation<T>();
    }
}