using System;
using System.Threading;

namespace Ctor.Server.Interfaces.Services
{
    public interface IBaseService
    {
    }

    public class ProgressMessage
    {
        public readonly int Progress;
        public readonly int Total;
        public readonly string Message;
        public readonly object Data;

        public ProgressMessage(int progress, int total, string message = null, object data = null)
        {
            Progress = progress;
            Total = total;
            Message = message;
            Data = data;
        }
    }

    public interface IBaseServiceCancelled
    {
        CancellationToken CancellationToken { get; set; }
        Action<ProgressMessage> InvokeProgress { get; set; }
    }
}
