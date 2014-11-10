using System;
using System.Messaging;

namespace MessageQueueSamples
{
    public class MessageQueueStringMessageImpl
    {
        public static void SendMessage(MessageQueue mq)
        {
            var msg = new Message
            {
                Priority = MessagePriority.Normal,
                Label = "Test Message",
                Body = "This is only a test"
            };
            mq.Send(msg);
            Console.WriteLine("Message sent.");
        }
        
        public static void ReceiveMessage(MessageQueue mq)
        {
            string messageBody;
            try
            {
                Message msg = mq.Receive(new TimeSpan(0, 0, 3));
                msg.Formatter = new XmlMessageFormatter(new[] { "System.String,mscorlib" });
                messageBody = msg.Body.ToString();
            }
            catch (Exception e)
            {
                messageBody = String.Format("No message ({0})", e.Message);
            }
            Console.WriteLine("received message: {0}", messageBody);
        }
    }
}