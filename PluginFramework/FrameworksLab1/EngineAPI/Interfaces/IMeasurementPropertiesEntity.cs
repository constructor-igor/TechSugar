using System.Collections.Generic;
using Plugin.Framework.Interfaces;

namespace EngineAPI.Interfaces
{
    public interface IMeasurementPropertyKey
    {
        bool Active { get; }
    }
    public interface IMeasurementPropertyEntity
    {
        IMeasurementPropertyKey PropertyKey { get; }
        double[] X { get; }
        double[] Y { get; }
    }
    public interface IMeasurementPropertiesEntity : IDataEntity
    {
        List<IMeasurementPropertyEntity> Properties { get; }
    }
}