using RestSharp;

namespace Gist.GitHub
{
    public class GitHubClient : RestClient
    {
        public GitHubClient() : base("https://api.github.com") { }
    }
}