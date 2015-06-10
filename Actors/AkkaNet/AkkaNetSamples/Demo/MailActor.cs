using System;
using Akka.Actor;

namespace Demo
{
    public class MailActor: UntypedActor
    {
        private readonly MailService m_mailService = new MailService();

        protected override void OnReceive(object message)
        {
            var responseMessage = message as ResponseMessage;
            if (responseMessage!=null)
            {
                m_mailService.Send(responseMessage.Response);
                Context.ActorSelection("/user/client").Tell(responseMessage);
            }
        }
    }
}