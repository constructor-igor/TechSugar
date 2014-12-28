using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    internal class CommandContext : ICommandContext
    {
        private readonly DataFramework dataFramework;
        private readonly CommandFramework commandFramework;
        private readonly ICommand foundCommand;
        private readonly string commandParameters;
        private readonly List<DataEntityContainer> dataParameters = new List<DataEntityContainer>();        
        protected internal CommandContext(DataFramework dataFramework, CommandFramework commandFramework, string commandUnique, string commandParameters, DataEntityContainer dataParameter = null)
        {
            this.dataFramework = dataFramework;
            this.commandFramework = commandFramework;
            this.commandParameters = commandParameters;
            if (dataParameter!=null)
                dataParameters.Add(dataParameter);
            Lazy<ICommand, IDictionary<string, object>> foundPlugin = commandFramework.FindPlugin(commandUnique);
            foundCommand = foundPlugin.Value;
        }
        #region ICommandContext
        public T GetDataEntity<T>() where T : IDataEntity
        {
            return dataFramework.GetDataEntity<T>();
        }
        public T GetService<T>()
        {
            return commandFramework.GetService<T>();
        }

        public T GetDataProvider<T>()
        {
            return commandFramework.GetDataProvider<T>();
        }
        public IDataEntity RunCommand(string commandUnique, string commandParameters)
        {
            var commandContext = new CommandContext(dataFramework, commandFramework, commandUnique, commandParameters);
            return commandContext.RunCommand();
        }
        public IDataEntity RunCommand(string commandUnique, string commandParameters, DataEntityContainer dataParameters)
        {
            var commandContext = new CommandContext(dataFramework, commandFramework, commandUnique, commandParameters, dataParameters);
            return commandContext.RunCommand();
        }
        public T GetCommandParameter<T>(string parameterName)
        {
            string[] keyValueList = commandParameters.Split(';');
            string foundParameterKeyValue = keyValueList.First(keyValueItem => keyValueItem.Trim().ToLower().StartsWith(parameterName.ToLower() + '='));
            string[] keyValuePair = foundParameterKeyValue.Trim().Split('=');
            string value = keyValuePair[1];

            if (typeof(T) == typeof(string))
                return (T)(object)value;
            if (typeof(T) == typeof(double))
                return (T)(object)Double.Parse(value);
            if (typeof(T) == typeof(Boolean))
                return (T)(object)Boolean.Parse(value);
            return (T)(object)value;
        }
        public DataEntityContainer GetDataParameter(string name)
        {
            return dataParameters.Find(dataParameter => dataParameter.Name.ToLower() == name.ToLower());
        }

        public T Get<T>()
        {
            try
            {
                return dataFramework.GetDataEntity<T>();
            }
            catch
            {
                return commandFramework.GetService<T>();
            }
            throw new NotImplementedException();
        }
        public T Get<T>(string name)
        {
            return commandFramework.GetService<T>(name);
        }

        #endregion

        public IDataEntity RunCommand()
        {
            return foundCommand.Run(this);
        }
    }
}