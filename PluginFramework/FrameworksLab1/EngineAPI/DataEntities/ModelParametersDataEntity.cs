using System.Collections.Generic;
using Plugin.Framework.Interfaces;

namespace EngineAPI.DataEntities
{
    public class ModelParametersDataEntity : IDataEntity
    {
        readonly public Dictionary<string, double> ParametersValues = new Dictionary<string, double>();
        public ModelParametersDataEntity()
        {
            
        }
    }
}