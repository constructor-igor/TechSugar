using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class CommandCompositionHelper
    {
        [ImportMany]
        public Lazy<ICommand, IDictionary<string, object>>[] Commands { get; set; }
        public CommandCompositionHelper(IEnumerable<DataFolder> pluginBinariesFolders)
        {
            // Creating an instance of aggregate catalog. It aggregates other catalogs
            var aggregateCatalog = new AggregateCatalog();

            // Load parts from the current assembly if available
            //var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
           // var dirCatalog = new DirectoryCatalog(@"..\..\..\@PluginsBinaries", "*.dll");

            //// Load parts from the available DLLs in the specified path using the directory catalog
            //var pluginDirectoryCatalog = new DirectoryCatalog(@"..\..\..\@PluginBinaries", "*.dll");
            //var targetDirectoryCatalog = new DirectoryCatalog(".", "*.dll");

            //Add to the aggregate catalog
            //aggregateCatalog.Catalogs.Add(pluginDirectoryCatalog);
            //aggregateCatalog.Catalogs.Add(targetDirectoryCatalog);
            //aggregateCatalog.Catalogs.Add(asmCatalog);
            foreach (DataFolder pluginsFolder in pluginBinariesFolders)
            {
                var dirCatalog = new DirectoryCatalog(pluginsFolder.Folder, "*.dll");
                aggregateCatalog.Catalogs.Add(dirCatalog);  
            }
            //aggregateCatalog.Catalogs.Add(dirCatalog);

            //Crete the composition container
            var container = new CompositionContainer(aggregateCatalog);

            // Composable parts are created here i.e. the Import and Export components assembles here
            container.ComposeParts(this);
        }
    }
}