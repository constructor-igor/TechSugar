using Nancy;

namespace SelfHostingSample
{
    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {
            Get["/"] = Index;
            Get["jsontest"] = JsonTest;
        }

        private dynamic Index(dynamic parameters)
        {
            return "Hello World";
        }

        private dynamic JsonTest(dynamic parameters)
        {
            var test = new { Name = "Peter Shaw", Twitter = "shawty_ds", Occupation = "Software Developer" };
            return Response.AsJson(test);
        }
    }
}