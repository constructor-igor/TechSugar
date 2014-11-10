using System.Messaging;

namespace MessageQueueSamples
{
    //
    //  http://www.outsystems.com/forums/discussion/6831/microsoft-message-queue-installation-for-community-edition/
    //  http://msdn.microsoft.com/en-us/library/aa967729(v=vs.110).aspx
    //
    //  http://www.codeproject.com/Articles/5830/Using-MSMQ-from-C
    //  http://stackoverflow.com/questions/11076790/the-bare-minimum-needed-to-write-a-msmq-sample-application
    //  http://www.codeproject.com/Articles/481/The-Microsoft-Message-Queue
    //  http://www.codeproject.com/Articles/3944/Programming-MSMQ-in-NET-Part
    //  http://www.codeproject.com/Articles/356317/Using-MSMQ-Backgroundworker-Threads-in-Csharp
    //
    //  [msdn "Message Queuing (MSMQ)"] http://msdn.microsoft.com/en-us/library/ms711472(v=vs.85).aspx
    //  http://msdn.microsoft.com/en-us/library/ee960153(v=vs.110).aspx
    //

    class Program
    {
        const string queuePath = @".\Private$\MessageQueueSamples";
        private static void Main()
        {
            using (MessageQueue mq = MessageQueueHelper.GetMessageQueue(queuePath))
            {

                MessageQueueCustomTypeMessageImpl.SendMessage(mq);
                MessageQueueCustomTypeMessageImpl.ReceiveMessage(mq);
//                MessageQueueStringMessageImpl.SendMessage(mq);
//                MessageQueueStringMessageImpl.ReceiveMessage(mq);
            }
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
