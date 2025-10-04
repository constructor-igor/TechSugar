using System;
using System.Collections.Generic;

namespace SerializationDemo
{
    public interface IDataCase1
    {
        string Name { get; set; }
        IDataCase1 Next { get; set; }

        List<int> Data { get; set; }
    }
    public class DataCase1: IDataCase1
    {
        public string Name { get; set; }
        public IDataCase1 Next { get; set; }

        public List<int> Data { get; set; }
        public List<double> DblData = new List<double>();
        public int[] IntDate = new int[10];
        public DataCase1() { }
        public DataCase1(string name)
        {
            Name = name;
            Data = new List<int> { 10, 20, 30 };
        }
    }

    public class DataCase1Factory: IDataCaseFactory<IDataCase1>
    {
        public IDataCase1 CreateCustomData(bool circle)
        {
            DataCase1 dataCase = new DataCase1("Joe");
            dataCase.DblData.Add(1.0);
            dataCase.DblData.Add(2.0);
            dataCase.Next = new DataCase1("Ross");
            if (circle)
            {
                dataCase.Next.Next = dataCase;
            }
            return dataCase;
        }
        public void Compare(IDataCase1 expected, IDataCase1 actual)
        {
            // Use a HashSet to track visited pairs and avoid infinite loops on circular references
            var visited = new HashSet<(IDataCase1, IDataCase1)>();
            CompareInternal(expected, actual, visited);
        }

        private void CompareInternal(IDataCase1 expected, IDataCase1 actual, HashSet<(IDataCase1, IDataCase1)> visited)
        {
            if (ReferenceEquals(expected, actual))
                return; // same instance, no need to compare

            if (expected == null || actual == null)
                throw new Exception("One of the objects is null while the other is not.");

            // Skip already visited pairs to handle circular references
            if (visited.Contains((expected, actual)))
                return;

            visited.Add((expected, actual));

            // Compare Name
            if (expected.Name != actual.Name)
                throw new Exception($"Name mismatch: {expected.Name} != {actual.Name}");

            // Compare Data list
            if ((expected.Data?.Count ?? 0) != (actual.Data?.Count ?? 0))
                throw new Exception($"Data count mismatch: {expected.Data?.Count} != {actual.Data?.Count}");

            if ((((DataCase1)expected).DblData?.Count ?? 0) != (((DataCase1)actual).DblData?.Count ?? 0))
                throw new Exception($"DblData count mismatch: {((DataCase1)expected).DblData?.Count} != {((DataCase1)actual).DblData?.Count}");


            if (expected.Data != null)
            {
                for (int i = 0; i < expected.Data.Count; i++)
                {
                    if (expected.Data[i] != actual.Data[i])
                        throw new Exception($"Data[{i}] mismatch: {expected.Data[i]} != {actual.Data[i]}");
                }
            }

            // Recursively compare Next
            CompareInternal(expected.Next, actual.Next, visited);
        }
    }
}
