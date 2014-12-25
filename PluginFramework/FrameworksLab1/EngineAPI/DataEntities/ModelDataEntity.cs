using Engine.Model;
using EngineAPI.Interfaces;

namespace EngineAPI.DataEntities
{
    public class ModelDataEntity: IModelDataEntity
    {
        internal Model _model;
        public ModelDataEntity(Model model)
        {
            _model = model;
        }

        public string Name { get { return _model.Name; } }
        public string History { get { return _model.History; } set { _model.History = value; }}
        public double GetParameterNominal(string parameterName)
        {
            ModelParameter found = _model.ModelParameters.Find(modelParameter => modelParameter.Name.ToLower() == parameterName.ToLower());
            return found.Nominal;
        }

        public void SetParametersValue(string parameterName, double parameterValue)
        {
            ModelParameter found = _model.ModelParameters.Find(modelParameter => modelParameter.Name.ToLower() == parameterName.ToLower());
            found.Nominal = parameterValue;
        }
    }
}
