using System;
using Akka.Actor;

namespace Demo
{
    public class WorkerActor : UntypedActor
    {
        private int m_count;

        protected override void OnReceive(object message)
        {
            if (message is UserMessage)
            {
                UserMessage userMessage = message as UserMessage;
                string response = String.Format("{0}: {1}", m_count, userMessage.Message);
                m_count = m_count + 1;
                ResponseMessage responseMessage = new ResponseMessage(response);
                Context.ActorSelection("/user/repository").Tell(responseMessage);
            }
            if (message is ResponseMessage)
            {
                Context.Parent.Forward(message);
            }
        }
    }
}