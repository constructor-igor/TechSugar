using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using Plugin.Command.Interfaces;

namespace MEF_loading_issue
{
    /*
     * Project created from issue http://stackoverflow.com/questions/28836436/error-when-try-load-plugin-using-mef
     * 
     * same issues
     * http://stackoverflow.com/questions/11164797/compositioncontainer-loading-wrong-directory-through-directorycatalog
     * section "Assembly Load Issues" http://blogs.msdn.com/b/dsplaisted/archive/2010/07/13/how-to-debug-and-diagnose-mef-failures.aspx
     * */
    class Program
    {
        static void Main()
        {
            try
            {
                var client = new Client();
                client.LoadPlugins();
                Console.WriteLine("plugins count: {0}", client.CommandPlugins.Count());
            }
            catch (Exception)
            {
                    
                throw;
            }
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
            // possible workaround: load plugin assembly and create AssemblyCatalog with help of loaded assembly (not by file path)
            //var pluginAssemblyCatalog = new AssemblyCatalog(Assembly.LoadFrom(@"..\..\..\@PluginBinaries\PluginAdd.dll"));
            aggregateCatalog.Catalogs.Add(pluginAssemblyCatalog);

            var container = new CompositionContainer(aggregateCatalog);
            container.ComposeParts(this);
        }
    }
    // try to load via catalog.Catalogs.Add(new AssemblyCatalog(Assembly.LoadFrom(@"C:\yosra\project\project.Plugin.Nav\bin\Debug\NavPlugin.dll")));
}
