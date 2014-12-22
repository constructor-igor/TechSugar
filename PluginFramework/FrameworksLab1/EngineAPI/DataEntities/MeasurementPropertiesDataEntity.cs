using System.Collections.Generic;
using EngineAPI.Interfaces;

namespace EngineAPI.DataEntities
{
    public class MeasurementPropertiesDataEntity : IMeasurementPropertiesEntity
    {
        private ModelDataEntity modelDataEntity;
        public MeasurementPropertiesDataEntity(ModelDataEntity modelDataEntity)
        {
            this.modelDataEntity = modelDataEntity;
            Properties = new List<IMeasurementPropertyEntity>();
        }

        public List<IMeasurementPropertyEntity> Properties { get; private set; }
    }

    public class MeasurementPropertiesEntity : IMeasurementPropertiesEntity
    {
        public List<IMeasurementPropertyEntity> Properties { get; private set; }

        public MeasurementPropertiesEntity()
        {
            Properties = new List<IMeasurementPropertyEntity>();
        }
    }

    public class MeasurementPropertyEntity : IMeasurementPropertyEntity
    {
        public IMeasurementPropertyKey PropertyKey { get; private set; }
        public double[] X { get; private set; }
        public double[] Y { get; private set; }

        public MeasurementPropertyEntity(IMeasurementPropertyKey propertyKey)
        {
            PropertyKey = propertyKey;
            X = new[] {0.0, 1.0};
            Y = new[] {10.0, 100.0};            
        }
    }

    public class MeasurementPropertyKey : IMeasurementPropertyKey
    {
        public bool Active { get; private set; }

        public MeasurementPropertyKey(bool active)
        {
            Active = active;
        }
    }
}
