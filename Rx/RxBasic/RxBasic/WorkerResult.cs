using System;

namespace RxBasic
{
    public class WorkerResult
    {
        public string Caption { get; private set; }

        public WorkerResult(int workerId)
        {
            Caption = String.Format("id: {0}", workerId);
        }
    }
}