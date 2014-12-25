using System.ComponentModel.Composition;
using EngineAPI.Interfaces;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace Plugins
{
    public class PluginMessageToHistoryEntities : IDataEntity
    {
        public IModelDataEntity ModelDataEntity { get; private set; }

        public PluginMessageToHistoryEntities(IModelDataEntity modelDataEntity)
        {
            ModelDataEntity = modelDataEntity;
        }
    }
    [Export(typeof(ICommand))]
    public class PluginMessageToModelHistory : ICommand
    {
        public PluginMessageToModelHistory()
        {
            Descriptor = new CommandDescriptor("message_to_history", "Add message to history");
        }
        public ICommandDescriptor Descriptor { get; private set; }
        public IDataEntity Run(ICommandContext commandContext)
        {
            var dataEntities = commandContext.GetDataEntity<PluginMessageToHistoryEntities>();
            var message = commandContext.GetCommandParameter<string>("message");

            dataEntities.ModelDataEntity.History += message;
            return null;
        }
    }
}