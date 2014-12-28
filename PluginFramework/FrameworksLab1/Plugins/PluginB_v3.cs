using System.ComponentModel.Composition;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace Plugins
{
    [Export(typeof(ICommand))]
    public class PluginB_v3 : ICommand
    {
        public PluginB_v3()
        {
            Descriptor = new CommandDescriptor("pluginB_single_get", "command B");
        }

        public ICommandDescriptor Descriptor { get; private set; }
        public IDataEntity Run(ICommandContext commandContext)
        {
            throw new System.NotImplementedException();
        }        
    }
}