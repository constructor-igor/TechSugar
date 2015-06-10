using System.Threading;

namespace SingleImplementation
{
    public class Repository
    {
        public void Save(string message)
        {
            Thread.Sleep(100);
        }
    }
}