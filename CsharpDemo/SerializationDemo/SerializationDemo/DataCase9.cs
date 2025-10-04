using System;
using System.Collections.Generic;

namespace SerializationDemo
{
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

    public interface IPosition
    {

    }
    public class Position : IPosition
    {
        public double X;
        public double Y;
        public double Z;
        public List<double> Coordinates = new List<double>();
        public List<double> CoordinatesDouble = new List<double>();
        public List<double> Dummy;
        public List<IPosition> OtherPositions = new List<IPosition>();
        public Position()
        {
            Dummy = new List<double>();
            //Coordinates = new List<double> { 0, 1, 2 };
            //CoordinatesDouble = new List<double> { 10, 11, 12 };
        }
    }
    public class MyMessage
    {
        public IPosition Position { get; set; }
        public List<int> IntList = new List<int>() { 10, 20, 30 };
        public double[] DblData = new double[] { 1.0, 2.0 };
        public MyMessage()
        {
            //IntList = new List<int>() { 10, 20, 30 };
        }
    }

    public interface ICustomSubData
    {

    }
    public abstract class BaseCustomSubData : ICustomSubData
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
    public class CustomData : ICustomData
    {
        public string Name { get; set; }
        public ICustomData Next { get; set; }
        public N1.A A1;
        public N2.A A2;
        public List<ICustomSubData> SubDataList;
        public List<ICustomSubData> DummyList = null;
        public MyMessage MyMessage;
        public List<int> IntList;

        public CustomData()
        {
            SubDataList = new List<ICustomSubData>();
            IntList = new List<int>();
        }
        public CustomData(string name) : this()
        {
            Name = name;
        }
    }

    public class DataCase9Factory : IDataCaseFactory<CustomData>
    {
        CustomData IDataCaseFactory<CustomData>.CreateCustomData(bool circle)
        {
            ICustomSubData customSubData1 = new CustomSubData1 { Name = "SubDataInterface1" };
            ICustomSubData customSubData2 = new CustomSubData2 { Name = "SubDataInterface2" };
            CustomData root = new CustomData("Root");
            root.A1 = new N1.A { Name = "N1_A" };
            root.A2 = new N2.A { Name = "N2_A" };
            root.IntList.Add(1);
            root.IntList.Add(2);
            Position position = new Position { X = 100, Y = 200, Z = 300 };
            position.OtherPositions.Add(new Position { X = 1, Y = 2, Z = 3 });
            position.OtherPositions.Add(position);
            root.MyMessage = new MyMessage { Position = position };
            root.SubDataList.Add(customSubData1);
            root.SubDataList.Add(customSubData2);
            MyMessage myMessage = new MyMessage { Position = new Position { X = 10, Y = 20, Z = 30 } };
            myMessage.IntList.Add(10);
            myMessage.IntList.Add(20);
            if (!circle)
            {
                root.Next = new CustomData("Child") { Next = null, MyMessage = myMessage };
            }
            else
            {
                root.Next = new CustomData("Child") { Next = root, MyMessage = myMessage };
            }

            return root;
        }
        public void Compare(CustomData expected, CustomData actual)
        {
            var visited = new HashSet<(CustomData, CustomData)>();
            CompareInternal(expected, actual, visited);
        }

        private void CompareInternal(CustomData expected, CustomData actual, HashSet<(CustomData, CustomData)> visited)
        {
            if (ReferenceEquals(expected, actual))
                return;

            if (expected == null || actual == null)
                throw new Exception("One of the objects is null while the other is not.");

            // Skip already visited pairs (handle circular references)
            if (visited.Contains((expected, actual)))
                return;

            visited.Add((expected, actual));

            if (expected.Name != actual.Name)
                throw new Exception($"Name mismatch: {expected.Name} != {actual.Name}");

            // Compare A1
            if ((expected.A1 == null) != (actual.A1 == null))
                throw new Exception("A1 null mismatch");
            if (expected.A1 != null && expected.A1.Name != actual.A1.Name)
                throw new Exception($"A1.Name mismatch: {expected.A1.Name} != {actual.A1.Name}");

            // Compare A2
            if ((expected.A2 == null) != (actual.A2 == null))
                throw new Exception("A2 null mismatch");
            if (expected.A2 != null && expected.A2.Name != actual.A2.Name)
                throw new Exception($"A2.Name mismatch: {expected.A2.Name} != {actual.A2.Name}");

            CompareLists(expected.IntList, actual.IntList, "IntList");

            // Compare SubDataList
            if ((expected.SubDataList?.Count ?? 0) != (actual.SubDataList?.Count ?? 0))
                throw new Exception("SubDataList count mismatch");
            for (int i = 0; i < (expected.SubDataList?.Count ?? 0); i++)
            {
                var eSub = expected.SubDataList[i] as BaseCustomSubData;
                var aSub = actual.SubDataList[i] as BaseCustomSubData;
                if (eSub?.Name != aSub?.Name)
                    throw new Exception($"SubDataList[{i}].Name mismatch: {eSub?.Name} != {aSub?.Name}");
            }

            // Compare MyMessage
            if ((expected.MyMessage == null) != (actual.MyMessage == null))
                throw new Exception("MyMessage null mismatch");
            if (expected.MyMessage != null)
            {
                CompareLists(expected.MyMessage.IntList, actual.MyMessage.IntList, "MyMessage.IntList");
                CompareArrays(expected.MyMessage.DblData, actual.MyMessage.DblData, "MyMessage.DblData");
                ComparePosition(expected.MyMessage.Position as Position, actual.MyMessage.Position as Position);
            }

            // Compare Next recursively
            CompareInternal(expected.Next as CustomData, actual.Next as CustomData, visited);
        }

        private void ComparePosition(Position expected, Position actual)
        {
            if ((expected == null) != (actual == null))
                throw new Exception("Position null mismatch");
            if (expected == null) return;

            if (expected.X != actual.X || expected.Y != actual.Y || expected.Z != actual.Z)
                throw new Exception($"Position coordinates mismatch: ({expected.X},{expected.Y},{expected.Z}) != ({actual.X},{actual.Y},{actual.Z})");

            CompareLists(expected.Coordinates, actual.Coordinates, "Coordinates");
            CompareLists(expected.CoordinatesDouble, actual.CoordinatesDouble, "CoordinatesDouble");
            CompareLists(expected.Dummy, actual.Dummy, "Dummy");

            if ((expected.OtherPositions?.Count ?? 0) != (actual.OtherPositions?.Count ?? 0))
                throw new Exception($"OtherPositions count mismatch: {expected.OtherPositions?.Count} != {actual.OtherPositions?.Count}");
        }

        private void CompareLists<T>(List<T> expected, List<T> actual, string name)
        {
            if ((expected == null) != (actual == null))
                throw new Exception($"{name} null mismatch");

            if (expected == null) return;

            if (expected.Count != actual.Count)
                throw new Exception($"{name} count mismatch: {expected.Count} != {actual.Count}");

            for (int i = 0; i < expected.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(expected[i], actual[i]))
                    throw new Exception($"{name}[{i}] mismatch: {expected[i]} != {actual[i]}");
            }
        }
        private void CompareArrays<T>(T[] expected, T[] actual, string name)
        {
            if ((expected == null) != (actual == null))
                throw new Exception($"{name} null mismatch");

            if (expected == null) return;

            if (expected.Length != actual.Length)
                throw new Exception($"{name} count mismatch: {expected.Length} != {actual.Length}");

            for (int i = 0; i < expected.Length; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(expected[i], actual[i]))
                    throw new Exception($"{name}[{i}] mismatch: {expected[i]} != {actual[i]}");
            }
        }
    }
}


