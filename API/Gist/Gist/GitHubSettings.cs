namespace Gist
{
    public class GitHubSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAnonymous
        {
            get { return Username.IsEmpty() || Password.IsEmpty(); }
        }
    }
}