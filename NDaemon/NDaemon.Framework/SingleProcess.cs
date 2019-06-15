using System;
using System.Threading;

namespace NDaemon.Framework
{
    public class SingleProcess: IDisposable
    {
        private Mutex m_mutex;

        public bool OtherProcessRunning { get; private set; }

        public SingleProcess(string appGuid)
        {
            bool createdNew;
            m_mutex = new Mutex(true, appGuid, out createdNew);
            if (createdNew)
            {
                m_mutex.ReleaseMutex();
            }
            OtherProcessRunning = !createdNew;
        }

        #region IDisposable
        public void Dispose()
        {
            if (m_mutex != null)
            {
                m_mutex.Dispose();
                m_mutex = null;
            }
        }
        #endregion
    }
}