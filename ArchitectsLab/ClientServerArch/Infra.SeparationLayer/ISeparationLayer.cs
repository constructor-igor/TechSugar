using System;
using System.Threading;
using System.Threading.Tasks;
using Ctor.Server.Interfaces.Services;

namespace Ctor.Infra.SeparationLayer
{
    public interface IAsyncOperation
    {
        void Cancel();
        void Wait();
    }

    public class ASyncOperation : IAsyncOperation
    {
        private readonly CancellationTokenSource m_cancellationTokenSource;
        private readonly Task m_task;

        public ASyncOperation(CancellationTokenSource cancellationTokenSource, Task task)
        {
            m_cancellationTokenSource = cancellationTokenSource;
            m_task = task;
        }

        #region IAsyncOperation
        public void Cancel()
        {
            m_cancellationTokenSource?.Cancel();
        }
        public void Wait()
        {
            m_task.Wait();
        }
        #endregion
    }

    public class DummyASyncOperation : IAsyncOperation
    {
        #region IAsyncOperation
        public void Cancel()
        {
            
        }
        public void Wait()
        {
        }
        #endregion
    }

    public class SeparationOperation<T>
    {
        private bool m_synchronously = false;
        private Action m_action;
        private Action m_finishlOperation;
        private Action m_cancelOperation;
        private Action<Exception> m_errorOperation;
        private readonly CancellationTokenSource m_cancellationTokenSource = new CancellationTokenSource();

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

        public SeparationOperation<T> OnFinishInvoke(Action action)
        {
            m_finishlOperation = action;
            return this;
        }
        public SeparationOperation<T> OnCancelInvoke(Action action)
        {
            m_cancelOperation = action;
            return this;
        }

        public SeparationOperation<T> OnErrorInvoke(Action<Exception> action)
        {
            m_errorOperation = action;
            return this;
        }

        public IAsyncOperation PostOperation()
        {
            if (Service is IBaseServiceCancelled)
                (Service as IBaseServiceCancelled).CancellationToken = m_cancellationTokenSource.Token;
            if (m_synchronously)
            {
                PerformTask();
                return new DummyASyncOperation();
            } else
            {
                Task task = new Task(PerformTask);
                task.Start();
                return new ASyncOperation(m_cancellationTokenSource, task);
            }
        }

        private void PerformTask()
        {
            bool exceptionRaised = false;
            try
            {
                m_action();
            }
            catch (Exception e)
            {
                exceptionRaised = true;
                if (!m_cancellationTokenSource.IsCancellationRequested)
                {
                    m_errorOperation?.Invoke(e);
                } else
                {
                    m_cancelOperation?.Invoke();
                }
            }
            if (!exceptionRaised && !m_cancellationTokenSource.IsCancellationRequested)
                m_finishlOperation?.Invoke();
        }
    }
    public interface ISeparationLayer
    {
        void Register<T>(T service);
        T GetService<T>();
        SeparationOperation<T> CreateOperation<T>();
    }
}