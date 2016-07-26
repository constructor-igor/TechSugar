using Nancy;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class SampleModule : NancyModule
    {
        public SampleModule()
        {
            Get["/"] = _ => "Hello World!";
        }
    }
}
