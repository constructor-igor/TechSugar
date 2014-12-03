using System;
using System.Messaging;

namespace MessageQueueSamples
{
    public class MessageQueueCustomTypeMessageImpl
    {
        public static void SendMessage(MessageQueue mq)
        {
            var person = new Person {FirstName = "Joe", LastName = "Smith", ID = 10};

            var msg = new Message {Priority = MessagePriority.Normal, Label = "person", Body = person};
            mq.Send(msg);
            Console.WriteLine("Message sent.");
        }

        public static void ReceiveMessage(MessageQueue mq)
        {
            string messageBody;
            try
            {
                Message msg = mq.Receive(new TimeSpan(0, 0, 3));
                msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(Person) });
                var person = (Person)msg.Body;
                messageBody = person.FirstName;
            }
            catch (Exception e)
            {
                messageBody = String.Format("No message ({0})", e.Message);
            }
            Console.WriteLine("received message: {0}", messageBody);
        }
    }
}