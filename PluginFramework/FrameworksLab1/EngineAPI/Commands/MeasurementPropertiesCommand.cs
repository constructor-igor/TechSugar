using System.ComponentModel.Composition;
using EngineAPI.DataEntities;
using EngineAPI.Interfaces;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace EngineAPI.Commands
{
    [Export(typeof(ICommand))]
    [ExportMetadata("type", "API")]         // command from API layer can know Engine layer
    public class MeasurementPropertiesCommand: ICommand
    {
        public MeasurementPropertiesCommand()
        {
            Descriptor = new CommandDescriptor("model_get_measurement_properties", "get_measurement_properties");
        }
        public ICommandDescriptor Descriptor { get; private set; }
        public IDataEntity Run(ICommandContext commandContext)
        {
            // get input data
            var model = (ModelDataEntity) commandContext.GetDataEntity<IModelDataEntity>();
            // get input parameters
            bool activeProperties = commandContext.GetCommandParameter<bool>("active");
            
            var measurementProperties = new MeasurementPropertiesDataEntity(model);
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
    }
}
