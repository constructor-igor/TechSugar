using System.IO;
using ApiMedic.Interfaces;

namespace ApiMedic.Impls
{
    public class LoadFromCacheApiDataProvider : IMedicalApiDataProvider
    {
        #region IMedicalApiDataProvider
        public string GetSymptoms()
        {
            string symptomsResponse = File.ReadAllText(@"..\..\cache\symptomsResponse.json");
            return symptomsResponse;
        }
        #endregion
    }
}