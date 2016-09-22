using System;
using Nancy;
using Nancy.ModelBinding;

namespace SelfHostingSample
{
    public class RootRoutes : NancyModule
    {
        const string nameMessageKey = "name";
        public RootRoutes()
        {
            Get["/"] = Index;
            Get["jsontest"] = JsonTest;
            Get["hello/{name}"] = HelloName;
            Post["posttest"] = PostTest;
        }

        private dynamic Index(dynamic parameters)
        {
            string message = Session[nameMessageKey] != null ? Session[nameMessageKey].ToString() : "World";
            return String.Format("Hello {0}", message);
        }

        private dynamic JsonTest(dynamic parameters)
        {
            var test = new { Name = "Peter Shaw", Twitter = "shawty_ds", Occupation = "Software Developer" };
            return Response.AsJson(test);
        }

        private dynamic HelloName(dynamic parameters)
        {
            var name = parameters.name;
            Session[nameMessageKey] = name.ToString();
            return String.Format("Hello there {0}", name);
        }

        private dynamic PostTest(dynamic parameters)
        {
            var myPerson = this.Bind<Person>();
            return String.Format("Hello there {0} with twitter handle {1} who works as a {2}", myPerson.Name, myPerson.Twitter, myPerson.Occupation);
        }
    }
}