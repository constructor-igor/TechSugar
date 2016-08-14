using System.IO;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;

namespace NancyFxApplication
{
    public class NancyFxModule: NancyModule
    {
        public NancyFxModule()
        {
            Get["/"] = param => View["Index.html"];

            Get["/Input"] = param =>
            {
                dynamic viewBag = new DynamicDictionary();
                viewBag.Name = "InputTest";
                return View["Input.html", viewBag];
            };

            Post["/Input"] = param =>                       // https://github.com/NancyFx/Nancy/wiki/Model-binding
            {
                string content;
                if (Request.Files.Any())
                {                    
                    HttpFile file = Request.Files.First();
                    using (StreamReader streamReader = new StreamReader(file.Value))
                    {
                        content = streamReader.ReadToEnd();
                    }
                }

                InputData newUserName = this.Bind<InputData>();
                dynamic viewBag = new DynamicDictionary();
                viewBag.Name = newUserName.User;
                return View["Input.html", viewBag];
            };

            Get["/{Name}"] = param =>
            {
                dynamic viewBag = new DynamicDictionary();
                viewBag.Name = param.Name;
                return View["Hello.html", viewBag];
            };

        }
    }

    public class InputData
    {
        public string User;
        public string File;
    }
}