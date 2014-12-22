using System.ComponentModel.Composition;
using Engine.Model;
using EngineAPI.DataEntities;
using EngineAPI.Interfaces;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace EngineAPI.Commands
{
    [Export(typeof(ICommand))]
    [ExportMetadata("type", "API")]         // command from API layer can know Engine layer
    public class MaterialsPropertiesCommand : ICommand
    {
        public MaterialsPropertiesCommand()
        {
            Descriptor = new CommandDescriptor("get_material_properties", "get materials properties");
        }
        public ICommandDescriptor Descriptor { get; private set; }
        public IDataEntity Run(ICommandContext commandContext)
        {
            var model = (ModelDataEntity)commandContext.GetDataEntity<IModelDataEntity>();
            var materialName = commandContext.GetCommandParameter<string>("material");
            DataEntityContainer rangeEntity = commandContext.GetDataParameter("range");
            var range = rangeEntity.DataAs<double[]>();

            Model modelObject = model._model;
            ModelMaterial foundMaterial = modelObject.ModelMaterials.Find(materialObject => materialObject.Name.ToLower() == materialName.ToLower());
            var materialRangeDataEntity = new DataEntityContainer("range", foundMaterial.Y);
            return materialRangeDataEntity;
        }
    }
}