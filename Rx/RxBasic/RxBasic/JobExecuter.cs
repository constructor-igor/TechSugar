using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RxBasic
{
    public class JobExecuter : IEnumerable<WorkerResult>, IEnumerator<WorkerResult>
    {
        List<WorkerResult> preparedResults;
        public JobExecuter()
        {
            Init();
        }

        void Init()
        {
            preparedResults = new List<WorkerResult> {new WorkerResult(1), new WorkerResult(2), new WorkerResult(3)};
        }

        public void Run()
        {
        }

        public IEnumerable<WorkerResult> GetResults()
        {
            return this;
        }

        #region IEnumerable<WorkerResult>
        public IEnumerator<WorkerResult> GetEnumerator()
        {
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region IEnumerator<WorkerResult>
        public void Dispose()
        {
            ToConsole("IEnumerator<WorkerResult>.Dispose()");
            internal_Reset();
        }
        public bool MoveNext()
        {
            bool result = preparedResults.Any();
            ToConsole("IEnumerator<WorkerResult>.MoveNext() = {0}", result);
            if (result)
                m_index = m_random.Next(preparedResults.Count);
            return result;
        }
        //
        // http://msdn.microsoft.com/en-us/library/system.collections.ienumerator.reset(v=vs.110).aspx
        // The Reset method is provided for COM interoperability. It does not necessarily need to be implemented; instead, the implementer can simply throw a NotSupportedException.
        //
        public void Reset()
        {
            ToConsole("IEnumerator<WorkerResult>.Reset()");
            internal_Reset();
        }
        private void internal_Reset()
        {
            m_random = new Random();
            Init();
            m_index = m_random.Next(preparedResults.Count);
        }

        public WorkerResult Current
        {
            get
            {
                ToConsole("IEnumerator<WorkerResult>.Current");
                m_current = preparedResults[m_index];
                preparedResults.RemoveAt(m_index);
                return m_current;
            }
        }
        object IEnumerator.Current
        {
            get
            {
                ToConsole("IEnumerator<WorkerResult>.Current");
                return Current;
            }
        }
        #endregion

        private Random m_random = new Random();
        private int m_index = -1;
        private WorkerResult m_current;
        void ToConsole(string message, params object[] parameters )
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message, parameters);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}