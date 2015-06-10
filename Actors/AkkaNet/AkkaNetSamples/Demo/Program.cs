using System.Threading;

namespace SingleImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            Thread t1 = new Thread(() => client.Run());
            Thread t2 = new Thread(() => client.Run());

            t1.Start();
            t2.Start();
        }
    }
}
