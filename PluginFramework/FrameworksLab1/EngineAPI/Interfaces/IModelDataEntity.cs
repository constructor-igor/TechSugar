using Plugin.Framework.Interfaces;

namespace EngineAPI.Interfaces
{
    public interface IModelDataEntity: IDataEntity
    {
        string Name { get; }
        string History { get; set; }
        double GetParameterNominal(string parameterName);
        void SetParametersValue(string parameterName, double parameterValue);
    }
}
