using System.Collections.Generic;
using System.ComponentModel.Composition;
using EngineAPI.DataEntities;
using EngineAPI.Interfaces;
using Microsoft.Practices.Unity;
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

    public class Plugin_v2_Dependency_Entities : IDataEntity
    {
        [Dependency] public IModelDataEntity ModelEntity { get; set; }
        [Dependency] public IMeasurementDataEntity MeasurementEntity { get; set; }
        [Dependency] public ModelParametersDataEntity ResultParameters { get; set; }
    }

    public class Plugin_v2_Dependency_Services
    {
        [Dependency] public IMeasurementPropertiesService MeasurementPropertiesService { get; set; }
        [Dependency] public IMaterialPropertiesService MaterialPropertiesService { get; set; }

        //[Dependency("calc_measurement")] public Func<IModelDataEntity, IMeasurementDataEntity> Calc { get; set; }         //TODO check thew possibility
    }

    [Export(typeof(ICommand))]
    public class PluginB_v2 : ICommand
    {
        public ICommandDescriptor Descriptor { get; private set; }

        public PluginB_v2()
        {
            Descriptor = new CommandDescriptor("pluginB_dependency", "command B");
        }
        public IDataEntity Run(ICommandContext commandContext)
        {
            //
            //  dataEntities contains all necessary input data entities
            //
            var dataEntities = commandContext.GetDataEntity<Plugin_v2_Dependency_Entities>();
            //var services = commandContext.GetService<Plugin_v2_Dependency_Services>();
            var services = commandContext.Get<Plugin_v2_Dependency_Services>();
            IMeasurementPropertiesEntity measurementPropertiesEntity = services.MeasurementPropertiesService.GetProperties(dataEntities.ModelEntity, activeProperties: true);

            var generalProperty = new List<double>();
            foreach (IMeasurementPropertyEntity measurementPropertyEntity in measurementPropertiesEntity.Properties)
            {
                generalProperty.AddRange(measurementPropertyEntity.Y);
            }

            //
            // Get parameters
            //
            var parameter1 = commandContext.GetCommandParameter<string>("parameter1");
            var parameter2 = commandContext.GetCommandParameter<string>("parameter2");
            var material1 = commandContext.GetCommandParameter<string>("material1");
            var material2 = commandContext.GetCommandParameter<string>("material2");
            var threshold = commandContext.GetCommandParameter<double>("threshold");

            double[] materialRange1 = services.MaterialPropertiesService.CalcRange(dataEntities.ModelEntity, material1, generalProperty.ToArray());
            double[] materialRange2 = services.MaterialPropertiesService.CalcRange(dataEntities.ModelEntity, material2, generalProperty.ToArray());

            double nominal1 = dataEntities.ModelEntity.GetParameterNominal(parameter1);
            double nominal2 = dataEntities.ModelEntity.GetParameterNominal(parameter2);

            double resultValue1;
            double resultValue2;
            // algorithm logic (materialRange1, materialRange2, nominal1, nominal2
            if (threshold < 0.5)
            {
                resultValue1 = 0.1;
                resultValue2 = 0.4;
            }
            else
            {
                resultValue1 = 0.8;
                resultValue2 = 0.9;
            }
            // ==> results
            // value of parameter 1
            // value of parameter 2

            dataEntities.ResultParameters.ParametersValues.Add(parameter1, resultValue1);
            dataEntities.ResultParameters.ParametersValues.Add(parameter2, resultValue2);

            return dataEntities.ResultParameters;
        }
    }
}
