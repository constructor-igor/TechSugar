using System.Runtime.Serialization;
using System.ServiceModel;

namespace Shavuot.Contract
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Text { get; set; }

        public Message(string text)
        {
            Text = text;
        }

        #region Overrides of Object
        public override string ToString()
        {
            return $"{Text}";
        }
        #endregion
    }

    [ServiceContract]
    public interface IShavuotServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnNewMessage(Message message);
    }

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IShavuotServiceCallback))]
    public interface IShavuotService
    {
        [OperationContract]
        void Greeting(Message message);

        [OperationContract]
        bool Subscribe();

        [OperationContract]
        bool Unsubscribe();
    }
}
