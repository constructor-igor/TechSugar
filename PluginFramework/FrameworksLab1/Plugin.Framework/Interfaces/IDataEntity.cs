namespace Plugin.Framework.Interfaces
{
    public interface IDataEntity
    {
    }

    public interface IDataProvider
    {
        
    }
    public interface IDataProvider<in T> where T: IDataEntity
    {
        void ExportToFile(string filePath, T dataEntity);
    }
}
