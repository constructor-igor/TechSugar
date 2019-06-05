using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfLogLibService
{
    [ServiceContract(
        SessionMode = SessionMode.Required,
        CallbackContract = typeof(ILogCallback))]

    public interface ILogService
    {
        [OperationContract]
        void SendMessage(string user, string msg);

        [OperationContract]
        bool Subscribe();

        [OperationContract]
        bool Unsubscribe();
    }


    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
