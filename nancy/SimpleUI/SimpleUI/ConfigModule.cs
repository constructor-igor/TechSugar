using Nancy;

namespace SimpleUI
{
    public class ConfigModule : NancyModule
    {
        public ConfigModule(): base("/config")
        {
            Get["/"] = x =>
            {
                var model = new ConfigStatusModel
                {
                    Config = new ConfigInfo()
                };
                return View["index.html", model];
            };
//            Post["/update"] = parameters =>
//            {
//                
//            };
        }
    }
}