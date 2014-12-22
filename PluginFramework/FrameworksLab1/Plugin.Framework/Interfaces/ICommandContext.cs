namespace Plugin.Framework.Interfaces
{
    public interface ICommandContext
    {
        T GetDataEntity<T>() where T : IDataEntity;
        IDataEntity RunCommand(string commandUnique, string commandParameters);
    }
}