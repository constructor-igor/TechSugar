using System.ServiceModel;

namespace WcfLogLibService
{
    public interface ILogCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnNewMessage(string user, string msg);
    }
}