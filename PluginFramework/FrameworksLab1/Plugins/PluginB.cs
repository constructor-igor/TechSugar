using System.ComponentModel.Composition;
using EngineAPI.Interfaces;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace Plugins
{
    /*
     * Input parameters of the plugin command:
     * - Parameter1, Material1
     * - Parameter2, Material2
     * - Threshold
     * 
     * Input data
     * - Active Measurement Arrays
     * 
     * Output data
     * - Parameter1 value
     * - Parameter2 value
     * 
     * */

    [Export(typeof(ICommand))]
    public class PluginB: ICommand
    {
        public ICommandDescriptor Descriptor { get; private set; }

        public PluginB()
        {
            Descriptor = new CommandDescriptor("pluginB", "command B");
        }
        public IDataEntity Run(ICommandContext commandContext)
        {
            var model = commandContext.GetDataEntity<IModelDataEntity>();
            //commandContext.RunService<IMeasurementPropertiesService, IModelDataEntity, IMeasurementPropertiesEntity>(model);
            //IMeasurementPropertiesService measurementPropertiesService = commandContext.GetService<IMeasurementPropertiesService>();
            //IMeasurementPropertiesData measurementProperties = measurementPropertiesServiceService.GetAllMeasurementProperties(model);

            //IDataEntity measurementProperties = commandContext.Run<IMeasurementPropertiesService>();
            //ICommand measurementProperties = commandContext.GetCommand<IMeasurementPropertiesService>();
            //measurementProperties.Run(commandContext);

            return model;
        }
    }
}
