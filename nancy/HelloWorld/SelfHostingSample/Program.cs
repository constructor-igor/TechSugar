using System;
using System.Text;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;
using Nancy.Session;
using Nancy.TinyIoc;

namespace SelfHostingSample
{
    class Program
    {
        static void Main()
        {
            var url = "http://127.0.0.1:9000";
            //UrlReservations urlReservations = new UrlReservations {CreateAutomatically = true};
            HostConfiguration hostConfiguration = new HostConfiguration {UrlReservations = {CreateAutomatically = true}};
            using (var host = new NancyHost(new Uri(url), new CustomBootstrapper(), hostConfiguration))
            {
                host.Start();
                Console.WriteLine("Nancy Server listening on {0}", url);
                Console.ReadLine();
            }
        }

        public class CustomBootstrapper : DefaultNancyBootstrapper
        {
            protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
            {
                base.ApplicationStartup(container, pipelines);
                CookieBasedSessions.Enable(pipelines);

                pipelines.OnError += (context, exception) =>
                {
                    if (exception is NameNotFoundException)
                        return new Response
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            ContentType = "text/html",
                            Contents = (stream) =>
                            {
                                var errorMessage = Encoding.UTF8.GetBytes(exception.Message);
                                stream.Write(errorMessage, 0, errorMessage.Length);
                            }
                        };
                    return HttpStatusCode.InternalServerError;
                };
            }
        }
    }
}
