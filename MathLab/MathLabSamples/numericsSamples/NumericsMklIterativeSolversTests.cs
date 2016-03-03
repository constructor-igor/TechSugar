using System;
using System.Collections.Generic;
using System.Globalization;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra.Complex32;
using MathNet.Numerics.Providers.LinearAlgebra.Mkl;
using NUnit.Framework;

namespace NumericsSamples
{
    /*
     * References:
     * - https://github.com/mathnet/mathnet-numerics/blob/36a46bd7fcd3dc273fbd147bf6f27dabfdd0d586/src/UnitTests/LinearAlgebraProviderTests/Complex32/LinearAlgebraProviderTests.cs
     * 
     * 
     * */

    [TestFixture]
    public class NumericsMklIterativeSolversTests
    {
        static readonly IContinuousDistribution Dist = new Normal();
        /// <summary>
        /// Test matrix to use.
        /// </summary>
        readonly IDictionary<string, DenseMatrix> _matrices = new Dictionary<string, DenseMatrix>
            {
                {"identity", DenseMatrix.OfArray(new[,] {{Complex32.One, Complex32.Zero, Complex32.Zero}, {Complex32.Zero, Complex32.One, Complex32.Zero}, {Complex32.Zero, Complex32.Zero, Complex32.One}})},
                {"Singular3x3", DenseMatrix.OfArray(new[,] {{new Complex32(1.0f, 0.0f), 1.0f, 2.0f}, {1.0f, 1.0f, 2.0f}, {1.0f, 1.0f, 2.0f}})},
                {"Square3x3", DenseMatrix.OfArray(new[,] {{new Complex32(-1.1f, 0.0f), -2.2f, -3.3f}, {0.0f, 1.1f, 2.2f}, {-4.4f, 5.5f, 6.6f}})},
                {"Square4x4", DenseMatrix.OfArray(new[,] {{new Complex32(-1.1f, 0.0f), -2.2f, -3.3f, -4.4f}, {0.0f, 1.1f, 2.2f, 3.3f}, {1.0f, 2.1f, 6.2f, 4.3f}, {-4.4f, 5.5f, 6.6f, -7.7f}})},
                {"Singular4x4", DenseMatrix.OfArray(new[,] {{new Complex32(-1.1f, 0.0f), -2.2f, -3.3f, -4.4f}, {-1.1f, -2.2f, -3.3f, -4.4f}, {-1.1f, -2.2f, -3.3f, -4.4f}, {-1.1f, -2.2f, -3.3f, -4.4f}})},
                {"Tall3x2", DenseMatrix.OfArray(new[,] {{new Complex32(-1.1f, 0.0f), -2.2f}, {0.0f, 1.1f}, {-4.4f, 5.5f}})},
                {"Wide2x3", DenseMatrix.OfArray(new[,] {{new Complex32(-1.1f, 0.0f), -2.2f, -3.3f}, {0.0f, 1.1f, 2.2f}})},
                {"Tall50000x10", DenseMatrix.CreateRandom(50000, 10, Dist)},
                {"Wide10x50000", DenseMatrix.CreateRandom(10, 50000, Dist)},
                {"Square1000x1000", DenseMatrix.CreateRandom(1000, 1000, Dist)}
            };
        [Test]
        public void Test()
        {
            var formatProvider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            formatProvider.TextInfo.ListSeparator = " ";

            var matrix = _matrices["identity"];
            var a = new Complex32[matrix.RowCount * matrix.RowCount];
            Array.Copy(matrix.Values, a, a.Length);

            var tau = new Complex32[matrix.ColumnCount];
            var q = new Complex32[matrix.ColumnCount * matrix.ColumnCount];
            Control.LinearAlgebraProvider.QRFactor(a, matrix.RowCount, matrix.ColumnCount, q, tau);

            var b = new[] { Complex32.One, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };
            var x = new Complex32[matrix.ColumnCount * 2];
            Control.LinearAlgebraProvider.QRSolveFactored(q, a, matrix.RowCount, matrix.ColumnCount, tau, b, 2, x);            
        }

        [Test]
        public void MklProvider()
        {
            MklLinearAlgebraProvider provider = new MklLinearAlgebraProvider();

            var formatProvider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            formatProvider.TextInfo.ListSeparator = " ";

            var matrix = _matrices["identity"];
            var a = new Complex32[matrix.RowCount * matrix.RowCount];
            Array.Copy(matrix.Values, a, a.Length);

            var tau = new Complex32[matrix.ColumnCount];
            var q = new Complex32[matrix.ColumnCount * matrix.ColumnCount];
            provider.QRFactor(a, matrix.RowCount, matrix.ColumnCount, q, tau);

            var b = new[] { Complex32.One, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };
            var x = new Complex32[matrix.ColumnCount * 2];
            provider.QRSolveFactored(q, a, matrix.RowCount, matrix.ColumnCount, tau, b, 2, x);            

        }
    }
}