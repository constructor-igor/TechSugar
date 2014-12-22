using System;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class CommandContext : ICommandContext
    {
        private readonly DataFramework dataFramework;
        public CommandContext(DataFramework dataFramework)
        {
            this.dataFramework = dataFramework;
        }
        public T GetDataEntity<T>() where T : IDataEntity
        {
            return dataFramework.GetDataEntity<T>();
        }

        public IDataEntity RunCommand(string commandUnique, string commandParameters)
        {
            throw new NotImplementedException();
        }
    }
}