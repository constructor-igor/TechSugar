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
        public double GetParameterNominal(string parameterName)
        {
            ModelParameter found = _model.ModelParameters.Find(modelParameter => modelParameter.Name.ToLower() == parameterName.ToLower());
            return found.Nominal;
        }
    }
}
