using System;

namespace SimpleUI
{
    [Serializable]
    public class ConfigInfo
    {
        public int UpdateInterval { get; set; }
        public string ServerName { get; set; }
    }

    public class ConfigStatusModel
    {
        public string Message { get; set; }
        public ConfigInfo Config { get; set; }
        public bool HasMessage
        {
            get { return !string.IsNullOrWhiteSpace(Message); }
        }
    }
}