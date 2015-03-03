using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Plugin.Command.Interfaces;

namespace MEF_loading_issue
{
    /*
     * Project created from issue http://stackoverflow.com/questions/28836436/error-when-try-load-plugin-using-mef
     * 
     * */
    class Program
    {
        static void Main()
        {
            var client = new Client();
            client.LoadPlugins();
            Console.WriteLine("plugins count: {0}", client.CommandPlugins.Count());
        }
    }

    public class Client
    {
        [ImportMany]
        public Lazy<ICommandPlugin, IDictionary<string, object>>[] CommandPlugins { get; set; }

        public void LoadPlugins()
        {
            var aggregateCatalog = new AggregateCatalog();

            //var pluginDirectoryCatalog = new DirectoryCatalog(@"..\..\..\@PluginBinaries", "*.dll");
            //aggregateCatalog.Catalogs.Add(pluginDirectoryCatalog);

            var pluginAssemblyCatalog = new AssemblyCatalog(@"..\..\..\@PluginBinaries\PluginAdd.dll");            
            aggregateCatalog.Catalogs.Add(pluginAssemblyCatalog);

            var container = new CompositionContainer(aggregateCatalog);
            container.ComposeParts(this);
        }
    }
}
