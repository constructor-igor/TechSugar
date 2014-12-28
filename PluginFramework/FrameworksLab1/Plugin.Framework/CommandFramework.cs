using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class CommandFramework
    {
        readonly List<ComposablePartCatalog> pluginCatalogs = new List<ComposablePartCatalog>();

        private CommandCompositionHelper commandCompositionHelper;
        private ContainerFramework containerFramework;
        private readonly DataFramework dataFramework;
        //public readonly UnityContainer serviceUnityContainer = new UnityContainer();

        public CommandFramework(ContainerFramework containerFramework, DataFramework dataFramework)
        {
            this.containerFramework = containerFramework;
            this.dataFramework = dataFramework;
        }

        public void AddPluginsFolder(DataFolder pluginsFolder)
        {
            pluginCatalogs.Add(new DirectoryCatalog(pluginsFolder.Folder, "*.dll"));
        }
        public void AddPluginsBinary(DataFile pluginFile)
        {
            pluginCatalogs.Add(new DirectoryCatalog(Path.GetDirectoryName(pluginFile.File), Path.GetFileName(pluginFile.File)));
        }

        public void Init()
        {
            commandCompositionHelper = new CommandCompositionHelper(pluginCatalogs);     

            foreach (Lazy<ICommand, IDictionary<string, object>> command in commandCompositionHelper.Commands)
            {
                ICommand commandInstance = command.Value;
                string commandUnique = commandInstance.Descriptor.Unique;
                //Type commandInstanceType = commandInstance.GetType();
                containerFramework.RegisterInstance(typeof(ICommand), commandUnique, commandInstance);

                var service = commandInstance as IService;
                if (service != null)
                {
                    foreach (Type serviceInterfaces in service.GetType().GetInterfaces())
                    {
                        //typeof(serviceInterfaces).GetInterfaces().Contains(typeof(IMyInterface))

                        if (serviceInterfaces.GetInterface("IService")!=null)
                        {
                            containerFramework.RegisterInstance(serviceInterfaces, commandInstance);
                        }

                        if (serviceInterfaces.IsAssignableFrom(typeof (IService)))
                        {
                            containerFramework.RegisterInstance(serviceInterfaces, commandInstance);
                        }
                    }
                }

            }
            /*
             * 
             * var service1 = CommandFramework.FindPlugin("model_get_measurement_properties").Value as IMeasurementPropertiesService;   //TODO should be implemented automatically
            CommandFramework.RegisterService<IMeasurementPropertiesService>(service1);
            var service2 = CommandFramework.FindPlugin("get_material_properties").Value as IMaterialPropertiesService;   //TODO should be implemented automatically
            CommandFramework.RegisterService<IMaterialPropertiesService>(service2);
             * */

            // register data providers
            foreach (Lazy<IDataProvider, IDictionary<string, object>> dataProvider in commandCompositionHelper.DataProviders)
            {
                IDataProvider dataProviderInstance = dataProvider.Value;
                foreach (Type dataProviderInterfaces in dataProviderInstance.GetType().GetInterfaces())
                {
                    if (dataProviderInterfaces.IsGenericType && dataProviderInterfaces.GetGenericTypeDefinition().Name == "IDataProvider`1")
                    {
                        containerFramework.RegisterInstance(dataProviderInterfaces, dataProviderInstance);
                    }
                }
            }
            /*
             *             Lazy<IDataProvider, IDictionary<string, object>> foundProvider = commandCompositionHelper.DataProviders.First(dataProvider => dataProvider.Value is T);
            return (T)foundProvider.Value;

             * */
        }

        public Lazy<ICommand, IDictionary<string, object>> FindPlugin(string commandUnique)
        {
            Lazy<ICommand, IDictionary<string, object>> foundPlugin = commandCompositionHelper.Commands.First(command => command.Value.Descriptor.Unique.ToLower() == commandUnique.ToLower());
            return foundPlugin;
        }

        public IDataEntity RunCommand(string commandUnique, string commandParameters)
        {
            var commandContext = new CommandContext(dataFramework, this, commandUnique, commandParameters);
            return commandContext.RunCommand();
        }

        public void RegisterService<T>(IService service) where T: IService
        {
            containerFramework.RegisterInstance(typeof(T), service);            
        }

        public T GetService<T>()
        {
            return containerFramework.Get<T>();
        }
        public T GetService<T>(string instanceName)
        {
            return containerFramework.Get<T>(instanceName);
        }

//        public T GetDataProvider<T>() 
//        {
//            Lazy<IDataProvider, IDictionary<string, object>> foundProvider = commandCompositionHelper.DataProviders.First(dataProvider => dataProvider.Value is T);
//            return (T)foundProvider.Value;
//        }
    }
}
