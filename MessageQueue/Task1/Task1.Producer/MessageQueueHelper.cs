using System.Messaging;

namespace Task1.Producer
{
    public class MessageQueueHelper
    {
        public static MessageQueue GetMessageQueue(string path)
        {
            MessageQueue mq = !MessageQueue.Exists(path) ? MessageQueue.Create(path) : new MessageQueue(path);
            return mq;
        }
    }
}