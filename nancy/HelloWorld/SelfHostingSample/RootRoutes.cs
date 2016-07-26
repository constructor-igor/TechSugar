using System;
using Nancy;
using Nancy.ModelBinding;

namespace SelfHostingSample
{
    public class RootRoutes : NancyModule
    {
        public RootRoutes()
        {
            Get["/"] = Index;
            Get["jsontest"] = JsonTest;
            Get["hello/{name}"] = HelloName;
            Post["posttest"] = PostTest;
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

        private dynamic HelloName(dynamic parameters)
        {
            var name = parameters.name;
            return String.Format("Hello there {0}", name);
        }

        private dynamic PostTest(dynamic parameters)
        {
            var myPerson = this.Bind<Person>();
            return String.Format("Hello there {0} with twitter handle {1} who works as a {2}", myPerson.Name, myPerson.Twitter, myPerson.Occupation);
        }
    }
}