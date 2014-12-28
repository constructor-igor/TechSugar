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
            containerFramework.Register(dataEntity);
        }

        public T GetDataEntity<T>()
        {
            return containerFramework.Get<T>();
        }
    }
}