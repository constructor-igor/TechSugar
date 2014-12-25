using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using EngineAPI.Interfaces;
using Plugin.Framework.Interfaces;

namespace EngineAPI.DataEntities
{
    [Export(typeof(IDataProvider))]
    public class ModelDataProvider : IDataProvider, IDataProvider<IModelDataEntity>
    {
        public void ExportToFile(string filePath, IModelDataEntity dataEntity)
        {
            var modelDataEntity = dataEntity as ModelDataEntity;
            StringBuilder modelContent = new StringBuilder()
                .AppendLine(String.Format("name={0}", modelDataEntity.Name))
                .AppendLine(String.Format("history={0}", modelDataEntity.History));
            File.WriteAllText(filePath, modelContent.ToString());
        }
    }
}