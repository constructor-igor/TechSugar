using Engine.Measurement;
using Engine.Model;
using EngineAPI.DataEntities;
using EngineAPI.Interfaces;
using NUnit.Framework;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace TestsProject
{
    [TestFixture]
    public class PluginFrameworkTests
    {
        [Test]
        public void FrameworkPlugins_Intro()
        {
            // engine data objects (not plugin-able business logic)
            Model model = CreateModel();
            var measurement = new Measurement();            
            // plugin definition (unique and parameters)
            const string commandUnique = "pluginB_stright";
            const string commandParameters = "parameter1=P1; parameter2=P2; material1=M1; material2=M2; threshold=0.4";

            var dataFramework = new DataFramework();
            dataFramework.Add<IModelDataEntity>(new ModelDataEntity(model));
            dataFramework.Add<IMeasurementDataEntity>(new MeasurementDataEntity(measurement));

            var commandFramework = new CommandFramework(dataFramework);
            commandFramework.AddPluginsFolder(new DataFolder(@"..\..\..\@PluginsBinaries"));
            commandFramework.AddPluginsBinary(new DataFile(@".\EngineAPI.dll"));
            commandFramework.Init();

            IDataEntity commandResult = commandFramework.RunCommand(commandUnique, commandParameters);
            Assert.IsInstanceOf<ModelParametersDataEntity>(commandResult);            
        }

        [Test]
        public void FrameworkPlugins_Dependency()
        {
            // engine data objects (not plugin-able business logic)
            Model model = CreateModel();
            var measurement = new Measurement();
            // plugin definition (unique and parameters)
            const string commandUnique = "pluginB_dependency";
            const string commandParameters = "parameter1=P1; parameter2=P2; material1=M1; material2=M2; threshold=0.4";

            var dataFramework = new DataFramework();
            dataFramework.Add<IModelDataEntity>(new ModelDataEntity(model));
            dataFramework.Add<IMeasurementDataEntity>(new MeasurementDataEntity(measurement));

            var commandFramework = new CommandFramework(dataFramework);
            commandFramework.AddPluginsFolder(new DataFolder(@"..\..\..\@PluginsBinaries"));
            commandFramework.AddPluginsBinary(new DataFile(@".\EngineAPI.dll"));
            commandFramework.Init();
            var service1 = commandFramework.FindPlugin("model_get_measurement_properties").Value as IMeasurementPropertiesService;   //TODO should be implemented automatically
            commandFramework.RegisterService<IMeasurementPropertiesService>(service1);
            var service2 = commandFramework.FindPlugin("get_material_properties").Value as IMaterialPropertiesService;   //TODO should be implemented automatically
            commandFramework.RegisterService<IMaterialPropertiesService>(service2);

            IDataEntity commandResult = commandFramework.RunCommand(commandUnique, commandParameters);
            Assert.IsInstanceOf<ModelParametersDataEntity>(commandResult);
        }

        private static Model CreateModel()
        {
            var model = new Model("model");
            model.ModelMaterials.Add(new ModelMaterial("M1"));
            model.ModelMaterials.Add(new ModelMaterial("M2"));
            model.ModelMaterials.Add(new ModelMaterial("M3"));
            model.ModelMaterials.Add(new ModelMaterial("M4"));
            model.ModelParameters.Add(new ModelParameter("P1", 0.1));
            model.ModelParameters.Add(new ModelParameter("P2", 0.2));
            model.ModelParameters.Add(new ModelParameter("P3", 0.3));
            model.ModelParameters.Add(new ModelParameter("P4", 0.4));
            return model;
        }
    }
}
