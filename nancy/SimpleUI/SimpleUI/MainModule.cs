using Nancy;

namespace SimpleUI
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = p => View["index.html"];
        }
    }
}