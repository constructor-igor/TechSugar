using Microsoft.Practices.Unity;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class DataFramework
    {
        public UnityContainer unityContainer = new UnityContainer();
        public void Add<T>(T dataEntity) where T: IDataEntity
        {
            unityContainer.RegisterInstance(dataEntity);
        }

        public T GetDataEntity<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }
}