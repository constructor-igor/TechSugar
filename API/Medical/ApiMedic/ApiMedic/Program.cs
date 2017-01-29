using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ApiMedic.DataTypes;
using ApiMedic.Impls;
using ApiMedic.Interfaces;

namespace ApiMedic
{
    class Program
    {
        static void Main()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string token = appSettings["Token"];
            IMedicalApiDataProvider dataProvider = new WebMedicalApiDataProvider(token);
//            IMedicalApiDataProvider storeToCacheProvider = new StoreToCacheApiDataProvider(dataProvider);
//            storeToCacheProvider.GetSymptoms();
            IMedicalApiDataProvider localProvider = new LoadFromCacheApiDataProvider();

            IMedicalApi medicalApi = new MedicalApi(localProvider);
            List<Symptom> allSymptoms = medicalApi.SelectSymptoms(symptom => true).ToList();
            Console.WriteLine("Downloaded {0} symptoms", allSymptoms.Count);
            allSymptoms.ForEach(symptom =>
            {
                Console.WriteLine("Symptom id={0:000}, name={1}", symptom.ID, symptom.Name);
            });
        }
    }
}
