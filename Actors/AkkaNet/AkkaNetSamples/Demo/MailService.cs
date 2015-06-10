using System.Threading;

namespace Demo
{
    public class MailService
    {
        public void Send(string message)
        {
            Thread.Sleep(100);
        }
    }
}