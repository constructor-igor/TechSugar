using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using Plugin.Command.Interfaces;

namespace Plugin.Command.Framework
{
    public class PluginCommandDescriptor
    {
        public readonly ICommandPlugin Command;
        public readonly IDictionary<string, object> MetaData;

        public PluginCommandDescriptor(ICommandPlugin command, IDictionary<string, object> metaData)
        {
            Command = command;
            MetaData = metaData;
        }
    }
    public class PluginCommandFramework
    {
        private readonly CommandCompositionHelper helper;
        public List<ICommandPlugin> Commands 
        {
            get { return helper.CommandPlugins.ToList().ConvertAll(pluginItem => pluginItem.Value); }
        }
        public List<IDictionary<string, object>> MetaData
        {
            get { return helper.CommandPlugins.ToList().ConvertAll(pluginItem => pluginItem.Metadata); }
        }
        public List<PluginCommandDescriptor> Descriptors
        {
            get { return helper.CommandPlugins.ToList().ConvertAll(pluginItem => new PluginCommandDescriptor(pluginItem.Value, pluginItem.Metadata)); }
        }        

        public PluginCommandFramework()
        {
            helper = new CommandCompositionHelper();
        }
    }

    public class CommandCompositionHelper
    {
        [ImportMany]
        public System.Lazy<ICommandPlugin, IDictionary<string, object>>[] CommandPlugins { get; set; }

        public CommandCompositionHelper()
        {
            // Creating an instance of aggregate catalog. It aggregates other catalogs
            var aggregateCatalog = new AggregateCatalog();

            // Load parts from the current assembly if available
            var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

            // Load parts from the available DLLs in the specified path using the directory catalog
            var pluginDirectoryCatalog = new DirectoryCatalog(@"..\..\..\@PluginBinaries", "*.dll");
            var targetDirectoryCatalog = new DirectoryCatalog(".", "*.dll");

            //Add to the aggregate catalog
            aggregateCatalog.Catalogs.Add(pluginDirectoryCatalog);
            aggregateCatalog.Catalogs.Add(targetDirectoryCatalog);
            aggregateCatalog.Catalogs.Add(asmCatalog);

            //Crete the composition container
            var container = new CompositionContainer(aggregateCatalog);

            // Composable parts are created here i.e. the Import and Export components assembles here
            container.ComposeParts(this);
        }
    }
}
