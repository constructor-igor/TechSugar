using System;
using System.Messaging;

namespace Task3.Consumer
{
    public class MessageQueueHelper
    {
        public static MessageQueue GetMessageQueue(string path)
        {
            MessageQueue mq = new MessageQueue(path);
            return mq;

//            try
//            {
//                MessageQueue mq = !MessageQueue.Exists(path) ? MessageQueue.Create(path) : new MessageQueue(path);
//                return mq;
//            }
//            catch (ArgumentException)
//            {
//                MessageQueue mq = new MessageQueue(path);
//                return mq;
//            }
        }
    }
}