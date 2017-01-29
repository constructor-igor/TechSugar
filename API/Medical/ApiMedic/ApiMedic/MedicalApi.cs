using System;
using System.Collections.Generic;
using System.Linq;
using ApiMedic.DataTypes;
using ApiMedic.Interfaces;
using Newtonsoft.Json;

namespace ApiMedic
{
    public class MedicalApi : IMedicalApi
    {
        private readonly IMedicalApiDataProvider m_dataProvider;

        public MedicalApi(IMedicalApiDataProvider dataProvider)
        {
            m_dataProvider = dataProvider;
        }
        #region Implementation of IMedicalApi
        public IEnumerable<Symptom> SelectSymptoms(Func<Symptom, bool> request)
        {
            string jsonReply = m_dataProvider.GetSymptoms();
            IEnumerable<dynamic> symptoms = (IEnumerable<dynamic>)JsonConvert.DeserializeObject(jsonReply);
            IEnumerable<Symptom> symptomsList = symptoms.Select((dynamic symptom) =>
            {
                int id = Convert.ToInt32(symptom.ID.Value);
                string name = Convert.ToString(symptom.Name.Value);
                return new Symptom(id, name);
            }).Where(request);
            return symptomsList;
        }
        #endregion
    }
}