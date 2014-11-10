using System;
using System.Messaging;

namespace MessageQueueSamples
{
    //
    //  http://www.outsystems.com/forums/discussion/6831/microsoft-message-queue-installation-for-community-edition/
    //  http://msdn.microsoft.com/en-us/library/aa967729(v=vs.110).aspx
    //

    class Program
    {
        const string queuePath = @".\Private$\MessageQueueSamples";
        private static void Main()
        {
            MessageQueue mq = MessageQueueHelper.GetMessageQueue(queuePath);

            SendMessage(mq);
            ReceiveMessage(mq);
        }

        private static void ReceiveMessage(MessageQueue mq)
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

        private static void SendMessage(MessageQueue mq)
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
    }

    public class MessageQueueHelper
    {
        public static MessageQueue GetMessageQueue(string path)
        {
            MessageQueue mq = !MessageQueue.Exists(path) ? MessageQueue.Create(path) : new MessageQueue(path);
            return mq;
        }
    }
}
