using Engine;
using EngineAPI.Interfaces;

namespace EngineAPI
{
    public class ModelDataEntity: IModelDataEntity
    {
        private Model _model;
        public ModelDataEntity(Model model)
        {
            _model = model;
        }
    }
}
