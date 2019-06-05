using System;
using System.Runtime.Serialization;
using System.ServiceModel;

/*
 * https://code.i-harness.com/en/q/703546
 * https://www.codeproject.com/Articles/97204/Implementing-a-Basic-Hello-World-WCF-Service
 * https://www.c-sharpcorner.com/article/create-simple-wcf-service-and-host-it-on-console-application/
 *
 */

namespace Demo.Contracts
{
    [ServiceContract]
    public interface IHelloWorldService
    {
        [OperationContract]
        string GetMessage(string name);

        [OperationContract]
        void SendMessage(HelloWordMessage message);
    }

    public enum MessageType { Text, Warning, Error }

    [DataContract]
    [KnownType(typeof(TextHelloWorldMessage))]
    [KnownType(typeof(DigitalHelloWorldMessage))]
    public class HelloWordMessage
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public DateTime TimeStamp { get; set; }
        [DataMember] public MessageType MessageType { get; set; }
    }

    [DataContract]
    public class TextHelloWorldMessage : HelloWordMessage
    {
        [DataMember] public string MessageText { get; set; }
    }
    [DataContract]
    public class DigitalHelloWorldMessage : HelloWordMessage
    {
        [DataMember] public int MessageData { get; set; }
    }
}
