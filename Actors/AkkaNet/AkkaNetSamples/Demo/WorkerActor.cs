using System;
using Akka.Actor;

namespace Demo
{
    public class WorkerActor : UntypedActor
    {
        private int m_count;
        private Repository m_repository;
        private MailService m_mailService;

        protected override void PreStart()
        {
            base.PreStart();
            m_repository = new Repository();
            m_mailService = new MailService();
        }

        protected override void OnReceive(object message)
        {
            if (message is UserMessage)
            {
                UserMessage userMessage = message as UserMessage;
                string response = String.Format("{0}: {1}", m_count, userMessage.Message);
                m_count = m_count + 1;
                m_repository.Save(response);
                m_mailService.Send(response);
                Sender.Tell(new ResponseMessage(response));
            }
        }
    }
}