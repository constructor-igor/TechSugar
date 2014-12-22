using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class CommandFramework
    {
        readonly List<ComposablePartCatalog> pluginCatalogs = new List<ComposablePartCatalog>();

        private CommandCompositionHelper commandCompositionHelper;
        private readonly DataFramework dataFramework;
        public readonly UnityContainer serviceUnityContainer = new UnityContainer();

        public CommandFramework(DataFramework dataFramework)
        {
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
            serviceUnityContainer.RegisterInstance(typeof(T), service);
        }
        public T GetService<T>()
        {
            return serviceUnityContainer.Resolve<T>();
        }
    }
}
