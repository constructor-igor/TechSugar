using System.Threading;

namespace Demo
{
    public class Repository
    {
        public void Save(string message)
        {
            Thread.Sleep(100);
        }
    }
}