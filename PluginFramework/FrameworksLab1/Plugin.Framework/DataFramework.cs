using Microsoft.Practices.Unity;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class DataFramework
    {
        private readonly ContainerFramework containerFramework;
        public UnityContainer unityContainer = new UnityContainer();

        public DataFramework(ContainerFramework containerFramework)
        {
            this.containerFramework = containerFramework;
        }
        public void Add<T>(T dataEntity) where T: IDataEntity
        {
            containerFramework.Add(dataEntity);
        }

        public T GetDataEntity<T>()
        {
            return containerFramework.Get<T>();
        }
    }
}