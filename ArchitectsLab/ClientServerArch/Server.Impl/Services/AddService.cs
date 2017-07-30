using System;
using System.Threading;
using Ctor.Server.Interfaces.Services;

namespace Ctor.Server.Impl.Services
{
    public class AddService: IAddService, IBaseServiceCancelled
    {
        #region Implementation of IAddService
        public double Add(double x, double y)
        {
            if (x ==0 && y==0)
                throw new Exception("X and y are 0.");
            if (x < 1000)
                return x + y;
            InvokeProgress?.Invoke(new ProgressMessage(0, 10));
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                if (CancellationToken.IsCancellationRequested)
                    throw new Exception();
                InvokeProgress?.Invoke(new ProgressMessage(i, 10));
            }
            InvokeProgress?.Invoke(new ProgressMessage(10, 10));
            return x + y;
        }

        #endregion

        #region IBaseServiceCancelled
        public CancellationToken CancellationToken { get; set; }
        public Action<ProgressMessage> InvokeProgress { get; set; }

        #endregion
    }
}
