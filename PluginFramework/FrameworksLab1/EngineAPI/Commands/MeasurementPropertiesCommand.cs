using System.ComponentModel.Composition;
using EngineAPI.DataEntities;
using EngineAPI.Interfaces;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace EngineAPI.Commands
{
    [Export(typeof(ICommand))]
    [ExportMetadata("type", "API")]         // command from API layer can know Engine layer
    public class MeasurementPropertiesCommand: ICommand, IMeasurementPropertiesService
    {
        public MeasurementPropertiesCommand()
        {
            Descriptor = new CommandDescriptor("model_get_measurement_properties", "get_measurement_properties");
        }
        #region ICommand
        public ICommandDescriptor Descriptor { get; private set; }
        public IDataEntity Run(ICommandContext commandContext)
        {
            // get input data
            var model = commandContext.GetDataEntity<IModelDataEntity>();
            // get input parameters
            bool activeProperties = commandContext.GetCommandParameter<bool>("active");
            
            return GetProperties(model, activeProperties);
        }
        #endregion
        #region IMeasurementPropertiesService
        public IMeasurementPropertiesEntity GetProperties(IModelDataEntity modelDataEntity, bool activeProperties)
        {
            var measurementProperties = new MeasurementPropertiesDataEntity(modelDataEntity as ModelDataEntity);
            if (activeProperties)
            {
                measurementProperties.Properties.Add(new MeasurementPropertyEntity(new MeasurementPropertyKey(true)));
                measurementProperties.Properties.Add(new MeasurementPropertyEntity(new MeasurementPropertyKey(true)));
            }
            else
            {
                measurementProperties.Properties.Add(new MeasurementPropertyEntity(new MeasurementPropertyKey(true)));
                measurementProperties.Properties.Add(new MeasurementPropertyEntity(new MeasurementPropertyKey(true)));
                measurementProperties.Properties.Add(new MeasurementPropertyEntity(new MeasurementPropertyKey(true)));
                measurementProperties.Properties.Add(new MeasurementPropertyEntity(new MeasurementPropertyKey(true)));
            }

            return measurementProperties;
        }
        #endregion
    }
}
