using System;
using System.Collections.Generic;
using ApiMedic.DataTypes;

namespace ApiMedic.Interfaces
{
    public interface IMedicalApi
    {
        IEnumerable<Symptom> SelectSymptoms(Func<Symptom, bool> request);
    }
}