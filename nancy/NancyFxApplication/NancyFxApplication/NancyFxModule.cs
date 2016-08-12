using Nancy;

namespace NancyFxApplication
{
    public class NancyFxModule: NancyModule
    {
        public NancyFxModule()
        {
            Get["/"] = param => base.View["Index.html"];

            Get["/{Name}"] = param =>
            {
                dynamic viewBag = new DynamicDictionary();
                viewBag.Name = param.Name;
                return View["Hello.html", viewBag];
            };
        }
    }
}