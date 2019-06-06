using System.ServiceModel;

namespace Shavuot.Contract
{
    [ServiceContract]
    public interface IShavuotService
    {
        [OperationContract]
        void Greeting(string message);
    }
}
