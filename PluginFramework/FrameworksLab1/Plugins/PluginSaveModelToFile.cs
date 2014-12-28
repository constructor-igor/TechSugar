using System.ComponentModel.Composition;
using EngineAPI.Interfaces;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace Plugins
{
    public class PluginSaveModelToFileEntities : IDataEntity
    {
        public IModelDataEntity ModelDataEntity { get; private set; }

        public PluginSaveModelToFileEntities(IModelDataEntity modelDataEntity)
        {
            ModelDataEntity = modelDataEntity;
        }
    }
    [Export(typeof(ICommand))]
    public class PluginSaveModelToFile : ICommand
    {
        public PluginSaveModelToFile()
        {
            Descriptor = new CommandDescriptor("save_model_to_file", "save model to file");
        }
        public ICommandDescriptor Descriptor { get; private set; }
        public IDataEntity Run(ICommandContext commandContext)
        {
            var dataEntities = commandContext.Get<PluginSaveModelToFileEntities>();
            var filePath = commandContext.GetCommandParameter<string>("file");

            var dataProvider = commandContext.GetDataProvider<IDataProvider<IModelDataEntity>>();
            dataProvider.ExportToFile(filePath, dataEntities.ModelDataEntity);
            return null;
        }
    }
}