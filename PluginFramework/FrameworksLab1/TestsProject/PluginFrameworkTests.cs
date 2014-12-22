using Engine;
using Engine.Measurement;
using EngineAPI;
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
            const string commandUnique = "pluginB";
            const string commandParameters = "threshold=0.5";

            var dataFramework = new DataFramework();
            dataFramework.Add<IModelDataEntity>(new ModelDataEntity(model));

            var commandFramework = new CommandFramework(dataFramework);
            commandFramework.AddPluginsFolder(new DataFolder(@"..\..\..\@PluginsBinaries"));
            commandFramework.Init();

            IDataEntity commandResult = commandFramework.RunCommand(commandUnique, commandParameters);
            Assert.IsAssignableFrom<ModelDataEntity>(commandResult);
        }

        private static Model CreateModel()
        {
            var model = new Model("model");
            model.ModelMaterials.Add(new ModelMaterial("material_1"));
            model.ModelMaterials.Add(new ModelMaterial("material_2"));
            model.ModelMaterials.Add(new ModelMaterial("material_3"));
            model.ModelMaterials.Add(new ModelMaterial("material_4"));
            model.ModelParameters.Add(new ModelParameter("parameter_1"));
            model.ModelParameters.Add(new ModelParameter("parameter_2"));
            model.ModelParameters.Add(new ModelParameter("parameter_3"));
            model.ModelParameters.Add(new ModelParameter("parameter_4"));
            return model;
        }
    }
}
