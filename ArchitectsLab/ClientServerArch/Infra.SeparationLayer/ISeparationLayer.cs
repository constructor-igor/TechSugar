using System;
using System.Threading.Tasks;

namespace Ctor.Infra.SeparationLayer
{
    public interface IAsyncOperation
    {
        void Wait();
    }

    public class ASyncOperation : IAsyncOperation
    {
        private readonly Task m_task;

        public ASyncOperation(Task task)
        {
            m_task = task;
        }
        #region IAsyncOperation
        public void Wait()
        {
            m_task.Wait();
        }
        #endregion
    }

    public class DummyASyncOperation : IAsyncOperation
    {
        #region IAsyncOperation
        public void Wait()
        {
        }
        #endregion
    }

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

        public IAsyncOperation PostOperation()
        {
            if (m_synchronously)
            {
                PerformTask();
                return new DummyASyncOperation();
            } else
            {
                Task task = new Task(PerformTask);
                task.Start();
                return new ASyncOperation(task);
            }
        }

        private void PerformTask()
        {
            m_action();
        }
    }
    public interface ISeparationLayer
    {
        void Register<T>(T service);
        T GetService<T>();
        SeparationOperation<T> CreateOperation<T>();
    }
}