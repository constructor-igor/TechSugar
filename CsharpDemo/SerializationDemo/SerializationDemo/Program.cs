using System;
using System.Collections.Generic;

namespace N1
{
    public class A
    {
        public string Name;
    }
}
namespace N2
{
    public class A
    {
        public string Name;
    }
}

namespace SerializationDemo
{
    public interface ICustomSubData
    {

    }
    public abstract class BaseCustomSubData: ICustomSubData
    {
        public string Name;
    }

    public class CustomSubData : BaseCustomSubData
    {

    }

    public class CustomData
    {
        public string Name;
        public CustomData Next;
        public N1.A A1;
        public N2.A A2;
        public List<ICustomSubData> SubDataList = new List<ICustomSubData>();

        public CustomData()
        {
        }
        public CustomData(string name)
        {
            Name = name;
        }
    }
    internal class Program
    {
        static CustomData CreateCustomData()
        {
            ICustomSubData customSubData = new CustomSubData { Name = "SubDataInterface" };
            CustomData root = new CustomData("Root");
            root.A1 = new N1.A { Name = "N1_A" };
            root.A2 = new N2.A { Name = "N2_A" };
            root.SubDataList.Add(customSubData);
            root.Next = new CustomData("Child") { Next = root };
            return root;
        }

        static void Main(string[] args)
        {
            CustomData root = CreateCustomData();
            //ICustomSerializer serializer = new SystemSerializer();
            ICustomSerializer serializer = new NewtonSerializer();
            string jsonContent = serializer.Serialize(root);
            Console.WriteLine($"Serialized content: {jsonContent}");

            try
            {
                CustomData deserialized = serializer.Deserialize(jsonContent);
                Console.WriteLine($"Deserialized content: Name={deserialized.Name}, Next.Name={deserialized.Next.Name}, Next.Next.Name={deserialized.Next.Next.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization failed: {ex}");
            }
        }
    }
}
