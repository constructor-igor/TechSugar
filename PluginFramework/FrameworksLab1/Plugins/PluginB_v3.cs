using System.Collections.Generic;
using System.ComponentModel.Composition;
using EngineAPI.DataEntities;
using EngineAPI.Interfaces;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace Plugins
{
    public class Plugin_v3_SingleGet_Entities : IDataEntity
    {
        public IModelDataEntity ModelEntity { get; private set; }
        public IMeasurementDataEntity MeasurementEntity { get; private set; }
        public ModelParametersDataEntity ResultParameters { get; private set; }

        public Plugin_v3_SingleGet_Entities(IModelDataEntity modelEntity, IMeasurementDataEntity measurementEntity, ModelParametersDataEntity resultParameters)
        {
            ModelEntity = modelEntity;
            MeasurementEntity = measurementEntity;
            ResultParameters = resultParameters;
        }
    }
    public class Plugin_v3_SingleGet_Services
    {
        public IMeasurementPropertiesService MeasurementPropertiesService { get; private set; }
        public IMaterialPropertiesService MaterialPropertiesService { get; private set; }

        //[Dependency("calc_measurement")] public Func<IModelDataEntity, IMeasurementDataEntity> Calc { get; set; }         //TODO check thew possibility
        public Plugin_v3_SingleGet_Services(IMeasurementPropertiesService measurementPropertiesService,
            IMaterialPropertiesService materialPropertiesService)
        {
            MeasurementPropertiesService = measurementPropertiesService;
            MaterialPropertiesService = materialPropertiesService;
        }
    }


    [Export(typeof(ICommand))]
    public class PluginB_v3 : ICommand
    {
        //private Plugin_v3_SingleGet_Services services;

        //[ImportingConstructorAttribute]
        public PluginB_v3()
        {
            Descriptor = new CommandDescriptor("pluginB_single_get", "command B");
            //this.services = services;
        }

        public ICommandDescriptor Descriptor { get; private set; }
        public IDataEntity Run(ICommandContext commandContext)
        {
            //
            //  dataEntities contains all necessary input data entities
            //
            var dataEntities = commandContext.Get<Plugin_v3_SingleGet_Entities>();
            var services = commandContext.Get<Plugin_v3_SingleGet_Services>();
            IMeasurementPropertiesEntity measurementPropertiesEntity = services.MeasurementPropertiesService.GetProperties(dataEntities.ModelEntity, activeProperties: true);

            //var getMaterialsCommand = commandContext.Get<ICommand>("get_material_properties");

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