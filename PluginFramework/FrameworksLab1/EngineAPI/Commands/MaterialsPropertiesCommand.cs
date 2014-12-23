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
    public class MaterialsPropertiesCommand : ICommand, IMaterialPropertiesService
    {
        public MaterialsPropertiesCommand()
        {
            Descriptor = new CommandDescriptor("get_material_properties", "get materials properties");
        }
        public ICommandDescriptor Descriptor { get; private set; }
        public IDataEntity Run(ICommandContext commandContext)
        {
            var model = commandContext.GetDataEntity<IModelDataEntity>();
            var materialName = commandContext.GetCommandParameter<string>("material");
            DataEntityContainer rangeEntity = commandContext.GetDataParameter("range");
            var range = rangeEntity.DataAs<double[]>();

            return new DataEntityContainer("material_range", CalcRange(model, materialName, range));
        }

        #region IMaterialProperties
        public double[] CalcRange(IModelDataEntity modelDataEntity, string materialName, double[] generalRange)
        {
            Model modelObject = (modelDataEntity as ModelDataEntity)._model;
            ModelMaterial foundMaterial = modelObject.ModelMaterials.Find(materialObject => materialObject.Name.ToLower() == materialName.ToLower());
            return foundMaterial.Y;
        }
        #endregion
    }
}