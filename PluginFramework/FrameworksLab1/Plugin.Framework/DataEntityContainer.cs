using System.Runtime.InteropServices.ComTypes;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class DataEntityContainer : IDataEntity
    {
        public readonly string Name;
        public readonly object Data;

        public DataEntityContainer(string name, object data)
        {
            Name = name;
            Data = data;
        }

        public T DataAs<T>()
        {
            return (T) Data;
        }
    }
}