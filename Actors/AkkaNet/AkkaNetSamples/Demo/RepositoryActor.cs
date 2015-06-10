using Akka.Actor;

namespace Demo
{
    public class RepositoryActor : UntypedActor
    {
        private Repository m_repository;
        protected override void PreStart()
        {
            base.PreStart();
            m_repository = new Repository();
        }
        protected override void OnReceive(object message)
        {
            ResponseMessage responseMessage = message as ResponseMessage;
            if (responseMessage!=null)
            {
                m_repository.Save(responseMessage.Response);
                Context.ActorSelection("/user/mail").Tell(responseMessage);
            }
        }
    }
}