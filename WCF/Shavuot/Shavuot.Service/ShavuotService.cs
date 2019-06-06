using System;
using System.ServiceModel;
using Shavuot.Contract;

namespace Shavuot.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ShavuotService: IShavuotService
    {
        #region IShavuotService
        public void Greeting(string message)
        {
            Console.WriteLine($"[ShavuotService.Greeting] {message}");
        }
        #endregion
    }
}
