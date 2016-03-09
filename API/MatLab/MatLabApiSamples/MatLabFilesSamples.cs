using System.Collections.Generic;
using csmatio.io;
using csmatio.types;
using NUnit.Framework;

namespace MatLabApiSamples
{
    [TestFixture]
    public class MatLabFilesSamples
    {
        [Test]
        public void CreateMatlabFile()
        {
            double[][] data3x3 = new double[3][];
            data3x3[0] = new double[] { 100.0, 101.0, 102.0 }; // first row
            data3x3[1] = new double[] { 200.0, 201.0, 202.0 }; // second row
            data3x3[2] = new double[] { 300.0, 301.0, 302.0 }; // third row

            MLDouble mlDoubleArray = new MLDouble("Matrix_3_by_3", data3x3);
            List<MLArray> mlList = new List<MLArray> {mlDoubleArray};
            MatFileWriter mfw = new MatFileWriter("data.mat", mlList, false);
        }
    }
}
