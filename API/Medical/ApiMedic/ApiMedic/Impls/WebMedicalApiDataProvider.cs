using System;
using System.Net;
using System.Runtime.CompilerServices;
using ApiMedic.Interfaces;

namespace ApiMedic.Impls
{
    public class WebMedicalApiDataProvider : IMedicalApiDataProvider
    {
        private readonly string m_token;
        private readonly string m_language = "en-gb";
        private readonly string m_format = "json";

        public WebMedicalApiDataProvider(string token)
        {
            m_token = token;
        }

        #region IMedicalApiDataProvider
        public string GetSymptoms()
        {
            string getSymptoms = String.Format(@"https://sandbox-healthservice.priaid.ch/symptoms?token={0}&language={1}&format={2}", m_token, m_language, m_format);
            return DownloadString(getSymptoms);
        }
        #endregion
        public static string DownloadString(string address)
        {
            using (WebClient client = new WebClient())
            {
                string reply = client.DownloadString(address);
                return reply;
            }
        }
    }
}