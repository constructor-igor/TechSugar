using Plugin.Framework.Interfaces;

namespace EngineAPI.Interfaces
{
    public interface IModelDataEntity: IDataEntity
    {
        double GetParameterNominal(string parameterName);
        void SetParametersValue(string parameterName, double parameterValue);
    }
}
