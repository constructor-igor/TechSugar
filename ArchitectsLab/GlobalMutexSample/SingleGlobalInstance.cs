using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

/*
 * copied from https://stackoverflow.com/questions/229565/what-is-a-good-pattern-for-using-a-global-mutex-in-c
 */

namespace GlobalMutexSample
{
    public class SingleGlobalInstance: IDisposable
    {
        private readonly Mutex m_mutex;
        private readonly bool m_hasHandle;
        public bool HasHandle => m_hasHandle;

        public SingleGlobalInstance(int millisecondsTimeOut)
        {
            // get application GUID as defined in AssemblyInfo.cs
            string appGuid = ((GuidAttribute) Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value;
            // unique id for global mutex - Global prefix means it is global to the machine
            string mutexId = $"Global\\{{{appGuid}}}";

            // Need a place to store a return value in Mutex() constructor call
            bool createdNew;

            // edited by Jeremy Wiebe to add example of setting up security for multi-user usage
            // edited by 'Marc' to work also on localized systems (don't use just "Everyone") 
            var allowEveryoneRule =
                new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null)
                    , MutexRights.FullControl
                    , AccessControlType.Allow
                );
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);
            m_mutex = new Mutex(false, mutexId, out createdNew, securitySettings);
            m_hasHandle = false;

            try
            {
                // note, you may want to time out here instead of waiting forever
                // edited by acidzombie24
                // mutex.WaitOne(Timeout.Infinite, false);
                m_hasHandle = m_mutex.WaitOne(millisecondsTimeOut, false);
//                if (m_hasHandle == false)
//                    throw new TimeoutException("Timeout waiting for exclusive access");
            }
            catch (AbandonedMutexException)
            {
                // Log the fact that the mutex was abandoned in another process,
                // it will still get acquired
                m_hasHandle = true;
            }
        }

        #region IDisposable
        public void Dispose()
        {
            if (m_mutex != null)
            {
                if (m_hasHandle)
                    m_mutex.ReleaseMutex();
                m_mutex.Close();
            }
        }
        #endregion
    }
}