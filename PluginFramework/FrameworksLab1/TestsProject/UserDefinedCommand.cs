using System.Collections.Generic;
using Engine.Model;
using EngineAPI.DataEntities;
using EngineAPI.Interfaces;
using Plugin.Framework;
using Plugin.Framework.Interfaces;

namespace TestsProject
{
    public class UserDefinedCommand : IAlgorithmCommand
    {
        private readonly CommandFramework commandFramework;
        private readonly DataFramework dataFramework;
        public string Name { get; set; }
        public string Body { get; set; }
        public void Run()
        {
            IDataEntity commandResult = commandFramework.RunCommand(CommandUnique, Body);
            //TODO command returns result, ? how to place the results to correct target?
            var modelParametersDataEntity = commandResult as ModelParametersDataEntity;
            if (modelParametersDataEntity != null)  // somehow we need decide target of results (for example, by returned type or special attributes)
            {
                var modelDataEntity = dataFramework.GetDataEntity<IModelDataEntity>();
                foreach (KeyValuePair<string, double> parameterValue in modelParametersDataEntity.ParametersValues)
                {
                    modelDataEntity.SetParametersValue(parameterValue.Key, parameterValue.Value);
                }
            }
        }

        public string CommandUnique { get; private set; }

        public UserDefinedCommand(Client client, string commandUnique, string commandName, string commandParameters)
        {
            this.commandFramework = client.CommandFramework;
            this.dataFramework = client.DataFramework;

            Name = commandName;
            Body = commandParameters;
            CommandUnique = commandUnique;
        }
    }
}
