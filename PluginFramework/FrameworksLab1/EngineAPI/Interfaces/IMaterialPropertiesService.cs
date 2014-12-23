using Plugin.Framework.Interfaces;

namespace EngineAPI.Interfaces
{
    public interface IMaterialPropertiesService : IService
    {
        double[] CalcRange(IModelDataEntity modelDataEntity, string materialName, double[] generalRange);
    }
}