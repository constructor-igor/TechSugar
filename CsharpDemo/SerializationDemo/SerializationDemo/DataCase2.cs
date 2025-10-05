using System;
using System.Collections.Generic;

namespace SerializationDemo
{
    public class Matrix
    {
        private double[,] m_matrix;
        public Matrix()
        {
            m_matrix = new double[1, 1];
        }
        public double this[int index1, int index2]
        {
            get => m_matrix[index1, index2];
            set => m_matrix[index1, index2] = value;
        }
    }
    public interface IDataCase2
    {
        string Name { get; set; }
        IDataCase2 Next { get; set; }

        List<int> Data { get; set; }
    }
    public class DataCase2 : IDataCase2
    {
        public string Name { get; set; }
        public IDataCase2 Next { get; set; }

        public List<int> Data { get; set; }
        public List<double> DblData = new List<double>();
        public int[] IntDate = new int[10];
        public Matrix Matrix;
        public DataCase2() { }
        public DataCase2(string name)
        {
            Name = name;
            Data = new List<int> { 10, 20, 30 };
            Matrix = new Matrix();
        }
        public double this[int index1, int index2]
        {
            get => Matrix[index1, index2];
            set => Matrix[index1, index2] = value;
        }
    }

    public class DataCase2Factory: IDataCaseFactory<IDataCase2>
    {
        public IDataCase2 CreateCustomData(bool circle)
        {
            DataCase2 dataCase = new DataCase2("Joe");
            dataCase.DblData.Add(1.0);
            dataCase.DblData.Add(2.0);
            dataCase.Next = new DataCase2("Ross");
            dataCase[0, 0] = 1.0;
            if (circle)
            {
                dataCase.Next = dataCase;
            }
            return dataCase;
        }
        public void Compare(IDataCase2 expected, IDataCase2 actual)
        {
            // Use a HashSet to track visited pairs and avoid infinite loops on circular references
            var visited = new HashSet<(IDataCase2, IDataCase2)>();
            CompareInternal(expected, actual, visited);
        }

        private void CompareInternal(IDataCase2 expected, IDataCase2 actual, HashSet<(IDataCase2, IDataCase2)> visited)
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

            if ((((DataCase2)expected).DblData?.Count ?? 0) != (((DataCase2)actual).DblData?.Count ?? 0))
                throw new Exception($"DblData count mismatch: {((DataCase2)expected).DblData?.Count} != {((DataCase2)actual).DblData?.Count}");


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
