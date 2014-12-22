using Plugin.Framework.Interfaces;

namespace EngineAPI.Interfaces
{
    public interface IMeasurementPropertiesService: IService
    {
        IMeasurementPropertiesEntity GetProperties(IModelDataEntity modelDataEntity, bool activeProperties);
    }
}