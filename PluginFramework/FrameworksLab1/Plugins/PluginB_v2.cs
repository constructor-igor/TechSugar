using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using EngineAPI.DataEntities;
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
            //  Get model
            //
            var model = commandContext.GetDataEntity<IModelDataEntity>();

            //
            // Get active only measurement properties
            //
            var measurementPropertiesEntity = commandContext.RunCommand("model_get_measurement_properties", "active=true") as IMeasurementPropertiesEntity;

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

            string commandParameters = String.Format("material={0}", material1);
            var materialRange = new DataEntityContainer("range", generalProperty.ToArray());
            var materialProperties = commandContext.RunCommand("get_material_properties", commandParameters, materialRange) as DataEntityContainer;
            var materialRanges1 = materialProperties.DataAs<double[]>();

            commandParameters = String.Format("material={0}", material2);
            materialProperties = commandContext.RunCommand("get_material_properties", commandParameters, materialRange) as DataEntityContainer;
            var materialRanges2 = materialProperties.DataAs<double[]>();

            double nominal1 = model.GetParameterNominal(parameter1);
            double nominal2 = model.GetParameterNominal(parameter2);

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

            var modelParametersDataEntity = new ModelParametersDataEntity();
            modelParametersDataEntity.ParametersValues.Add(parameter1, resultValue1);
            modelParametersDataEntity.ParametersValues.Add(parameter2, resultValue2);

            return modelParametersDataEntity;
        }
    }
}
