using Akka.Actor;

namespace Demo
{
    public class MailActor: ReceiveActor
    {
        private readonly MailService m_mailService = new MailService();

        public MailActor()
        {
            Receive<ResponseMessage>(responseMessage =>
            {
                m_mailService.Send(responseMessage.Response);
                Context.ActorSelection("/user/client").Tell(responseMessage);
            });
        }
    }
}