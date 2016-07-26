using Nancy;

namespace SelfHostingSample
{
    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {
            Get["/"] = parameters => "Hello World";
        }
    }
}