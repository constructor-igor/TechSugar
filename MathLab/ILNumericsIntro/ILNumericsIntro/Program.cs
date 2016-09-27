using System;
using System.Collections.Generic;
using System.Linq;
using ILNumerics;

namespace ILNumericsIntro
{
    class Program: ILMath
    {
        public struct Measurement
        {
            public double X;
            public double Y;
            public double Z;
            public double Data1;
            public double Data2;
        }

        static void Main(string[] args)
        {
            double[] A = new double[] { 1.1, 0.2, -1.3, 4, 5 };
            A[4] = 20;
            int r = 10;
            double B = A[4];
            int[] C = new int[] { 1, 2, -1, 4, 5 };

            int[,] D = new int[,]
            {
                {1, 0, -1, 4, 5},
                {0, 1, 0, -1, 2}
            };
            //D[0, 4] = 20;
            int[,,] D3 = new int[,,]
            {
                {{1,10}, {0,4}, {-1, 2}, {4, 20}, {5, 100}},
                {{0, 2}, {1, 2}, {0, 1}, {-1, 0}, {2, 3}}
            };

            int[][] D2 = {
                new[] {11, 10, -11, 14, 15},
                new[] {20, 21, 20, -21, 22},
            };

            List<int> DList = new List<int>(){0, 1, 2, 3, 5, 6, 2, 4, 8, 10};


            // we store the data of 9 measurements:
            Measurement[] myData = new Measurement[]{
                new Measurement{X = 0, Y = 0, Z = 1.1, Data1 = 1.002, Data2 = 0.00205},
                new Measurement{X = 1, Y = 0, Z = 1.1, Data1 = 1.232, Data2 = 0.00224},
                new Measurement{X = 2, Y = 0, Z = 1.1, Data1 = 1.042, Data2 = 0.00155},
                new Measurement{X = 0, Y = 1, Z = 1.1, Data1 = 1.042, Data2 = 0.001852},
                new Measurement{X = 1, Y = 1, Z = 1.1, Data1 = 1.041, Data2 = 0.002258},
                new Measurement{X = 2, Y = 1, Z = 1.1, Data1 = 1.039, Data2 = 0.00215},
                new Measurement{X = 0, Y = 2, Z = 1.1, Data1 = 0.952, Data2 = 0.00204},
                new Measurement{X = 1, Y = 2, Z = 1.1, Data1 = 0.902, Data2 = 0.00233},
                new Measurement{X = 2, Y = 2, Z = 1.1, Data1 = 0.902, Data2 = 0.00233}
            };

            double my1stPoint = myData[4].Data1;


            complex[] complex_sysA = new complex[] {
        new complex(-0.47943,0.87758),
        new complex(-0.41831,0.90831),
        new complex(-0.35523,0.93478),
        new complex(-0.29049,0.95688),
        new complex(-0.22439,0.97450),
        new complex(-0.15724,0.98756),
        new complex(-0.08935,0.99600),
        new complex(-0.02105,0.99978),
        new complex( 0.04735,0.99888),
        new complex( 0.11553,0.99330),
        new complex( 0.18317,0.98308),
        new complex( 0.24995,0.96826),
        new complex( 0.31557,0.94890),
        new complex( 0.37970,0.92511),
        new complex( 0.44206,0.89699),
        new complex( 0.50235,0.86466),
        new complex( 0.56029,0.82830),
        new complex( 0.61561,0.78805),
        new complex( 0.66805,0.74412),
        new complex( 0.71736,0.69671)
};

            ILArraySample sample = new ILArraySample();
            sample.Test();

            ILArray<int> intA = zeros<int>(100);
            ILArray<double> doubleA = ones(5, 4);

            int N = 1024;
            double[] sinData = Enumerable.Range(0, N).Select(i => Math.Sin(i)).ToArray();
            Console.WriteLine("sin() data ({0}) generated", sinData.Length);

        }
    }
}
