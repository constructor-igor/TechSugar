﻿namespace Plugin.Framework.Interfaces
{
    public interface ICommandContext
    {
        T GetDataEntity<T>() where T : IDataEntity;
        IDataEntity RunCommand(string commandUnique, string commandParameters);
        IDataEntity RunCommand(string commandUnique, string commandParameters, DataEntityContainer dataParameters);
        T GetCommandParameter<T>(string parameterName);
        DataEntityContainer GetDataParameter(string name);
    }
}