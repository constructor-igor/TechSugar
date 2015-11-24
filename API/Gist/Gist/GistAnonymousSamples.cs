using System.Collections.Generic;
using System.Linq;
using System.Net;
using Gist.GitHub;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;

namespace Gist
{
    [TestFixture]
    public class GistAnonymousSamples
    {
        [Test]
        public void Test()
        {
            GitHubClient client = new GitHubClient();
            var content = new Dictionary<string, string> {{"test", "var i = 0;"}};
            string url = Publish(client, content);

        }

        private string Publish(GitHubClient client, IDictionary<string, string> content)
        {
            var response = client.Execute<GitHub.Gist>(new RestRequest("/gists", Method.POST) { RequestFormat = DataFormat.Json }
              .AddBody(new GitHub.Gist { IsPublic = true, Files = content.ToDictionary(_ => _.Key, _ => new GistFile { Content = _.Value }) }));
            if ((response.ResponseStatus == ResponseStatus.Error) 
                //|| !response.StatusCode.InRange(HttpStatusCode.OK, HttpStatusCode.Ambiguous - 1)
                )
            {
                //Logger.LogMessage("Gist error: {0}", response.ErrorMessage ?? string.Format("{0:D} {1}", response.StatusCode, response.StatusDescription));
                return null;
            }
            return response.Data != null ? response.Data.HtmlUrl : null;
        }
    }
}
