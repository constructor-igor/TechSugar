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
    public interface IPosition
    {

    }
    public class Position: IPosition
    {
        public double X;
        public double Y;
        public double Z;
        public List<double> Coordinates = new List<double> {0, 1, 2};
        public double[] CoordinatesDouble = {10, 11, 12};
        public double[] Dummy = { };
        public List<IPosition> OtherPositions = new List<IPosition>();
    }
    public class MyMessage
    {
        public IPosition Position {get;set;}
        public List<int> IntList = new List<int> { 10, 20, 30 };
    }

    public interface ICustomSubData
    {

    }
    public abstract class BaseCustomSubData: ICustomSubData
    {
        public string Name;
    }

    public class CustomSubData1 : BaseCustomSubData
    {

    }
    public class CustomSubData2 : BaseCustomSubData
    {

    }

    public interface ICustomData
    {
        string Name { get; set; }
        ICustomData Next { get; set; }
    }
    public class CustomData: ICustomData
    {
        public string Name { get; set; }
        public ICustomData Next { get; set; }
        public N1.A A1;
        public N2.A A2;
        public List<ICustomSubData> SubDataList = new List<ICustomSubData>();
        public List<ICustomSubData> DummyList = null;
        public MyMessage MyMessage;
        public List<int> IntList = new List<int>();

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
            ICustomSubData customSubData1 = new CustomSubData1 { Name = "SubDataInterface1" };
            ICustomSubData customSubData2 = new CustomSubData2 { Name = "SubDataInterface2" };
            CustomData root = new CustomData("Root");
            root.A1 = new N1.A { Name = "N1_A" };
            root.A2 = new N2.A { Name = "N2_A" };
            root.IntList.Add(1);
            root.IntList.Add(2);
            Position position = new Position { X = 100, Y = 200, Z = 300 };
            //position.OtherPositions.Add(new Position { X = 1, Y = 2, Z = 3 });
            //position.OtherPositions.Add(position);
            root.MyMessage = new MyMessage { Position = position };
            root.SubDataList.Add(customSubData1);
            root.SubDataList.Add(customSubData2);
            MyMessage myMessage = new MyMessage{Position = new Position {X=10, Y=20, Z=30}};
            root.Next = new CustomData("Child") { Next = null, MyMessage = myMessage};
            //root.Next = new CustomData("Child") { Next = root, MyMessage = myMessage};
            return root;
        }

        static void Main(string[] args)
        {
            CustomData root = CreateCustomData();
            ICustomSerializer serializer = new GeneralSystemSerializer();
            //ICustomSerializer serializer = new SystemSerializer();
            //ICustomSerializer serializer = new NewtonSerializer();
            //ICustomSerializer serializer = new NewtonSerializer2();
            string jsonContent = serializer.Serialize(root);
            Console.WriteLine($"Serialized content: {jsonContent}");

            try
            {
                CustomData deserialized = serializer.Deserialize<CustomData>(jsonContent);
                //Console.WriteLine($"Deserialized content: Name={deserialized.Name}, Next.Name={deserialized.Next.Name}, Next.Next.Name={deserialized.Next.Next.Name}");

                Position expectedP = (Position)root.MyMessage.Position;
                Position actualP = (Position)deserialized.MyMessage.Position;

                if (expectedP.Coordinates.Count != actualP.Coordinates.Count)
                {
                    throw new Exception("Incorrect deserialized result");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization failed: {ex}");
            }
        }
    }
}
