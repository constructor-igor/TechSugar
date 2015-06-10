using System.Threading;

namespace SingleImplementation
{
    public class MailService
    {
        public void Send(string message)
        {
            Thread.Sleep(100);
        }
    }
}