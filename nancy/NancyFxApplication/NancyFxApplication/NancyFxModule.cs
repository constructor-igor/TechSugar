using Nancy;

namespace NancyFxApplication
{
    public class NancyFxModule: NancyModule
    {
        public NancyFxModule()
        {
            Get["/"] = param => View["Index.html"];

            Get["/{Name}"] = param =>
            {
                dynamic viewBag = new DynamicDictionary();
                viewBag.Name = param.Name;
                return View["Hello.html", viewBag];
            };
        }
    }
}