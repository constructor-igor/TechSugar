using System;
using WcfLogLibService;

namespace WPFClient
{
    public class LogCallback : ILogCallback
    {
        MainWindow _client;

        public LogCallback(MainWindow client)
        {
            _client = client;
        }

        public void OnNewMessage(string user, string msg)
        {
            _client.LogPeerMessage(user + " says: " + Environment.NewLine + msg);
        }
    }
}