using Engine.Measurement;
using Engine.Model;
using EngineAPI.DataEntities;
using EngineAPI.Interfaces;
using Plugin.Framework;

namespace TestsProject
{
    public class Client
    {
        readonly ContainerFramework containerFramework = new ContainerFramework();
        public Model Model { get; set; }
        public Measurement Measurement { get; set; }

        public DataFramework DataFramework { get; private set; }
        public CommandFramework CommandFramework { get; private set; }
        public Client()
        {
            DataFramework = new DataFramework(containerFramework);
            CommandFramework = new CommandFramework(DataFramework);
        }

        public void Init()
        {
            DataFramework.Add<IModelDataEntity>(new ModelDataEntity(Model));
            DataFramework.Add<IMeasurementDataEntity>(new MeasurementDataEntity(Measurement));


            CommandFramework.AddPluginsFolder(new DataFolder(@"..\..\..\@PluginsBinaries"));
            CommandFramework.AddPluginsBinary(new DataFile(@".\EngineAPI.dll"));

            CommandFramework.Init();

            var service1 = CommandFramework.FindPlugin("model_get_measurement_properties").Value as IMeasurementPropertiesService;   //TODO should be implemented automatically
            CommandFramework.RegisterService<IMeasurementPropertiesService>(service1);
            var service2 = CommandFramework.FindPlugin("get_material_properties").Value as IMaterialPropertiesService;   //TODO should be implemented automatically
            CommandFramework.RegisterService<IMaterialPropertiesService>(service2);
        }
    }

}
