using System;
using System.Collections.Generic;
using System.Globalization;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.LinearAlgebra.Single.Solvers;
using MathNet.Numerics.LinearAlgebra.Solvers;
using NUnit.Framework;

namespace NumericsSamples
{
    /*
     * References:
     * - https://github.com/mathnet/mathnet-numerics/blob/b8ae065e0fc7bdce08b17c3ae60be3872b37b2de/src/Examples/LinearAlgebra/IterativeSolvers/CompositeSolverExample.cs
     * 
     * */
    [TestFixture]
    public class NumericsIterativeSolversTests
    {
        [Test]
        public void CompositeSolver_DenseFloat()
        {
            Matrix<float> matrix = DenseMatrix.OfArray(new[,] {{5.00f, 2.00f, -4.00f}, {3.00f, -7.00f, 6.00f}, {4.00f, 1.00f, 5.00f}});
            Vector<float> vector = new DenseVector(new[] {-7.0f, 38.0f, 43.0f});
            IIterativeSolver<float> solver = new CompositeSolver(new List<IIterativeSolverSetup<float>> { new UserBiCgStabFloat() });
            CompositeSolver(matrix, vector, solver);
        }
        [Test]
        public void CompositeSolver_SparseFloat()
        {
            Matrix<float> matrix = SparseMatrix.OfArray(new[,] {{1f, 0f, 0f}, {0f, 1f, 0f}, {0f, 0f, 1f}});
            Vector<float> vector = new DenseVector(new[] {1f, 2f, 3f});
            IIterativeSolver<float> solver = new CompositeSolver(new List<IIterativeSolverSetup<float>> { new UserBiCgStabFloat() });
            CompositeSolver(matrix, vector, solver);
        }

        [Test]
        public void CompositeSolver_SparseComplex()
        {
            Matrix<Complex32> matrix = MathNet.Numerics.LinearAlgebra.Complex32.SparseMatrix.OfArray(new[,]
            {
                { Complex32.One, Complex32.Zero, Complex32.Zero }, 
                { Complex32.Zero, Complex32.One, Complex32.Zero }, 
                { Complex32.Zero, Complex32.Zero, Complex32.One} 
            });
            Vector<Complex32> vector = new MathNet.Numerics.LinearAlgebra.Complex32.DenseVector(new[] { new Complex32(1, 0), new Complex32(2, 0), new Complex32(3, 0) });
            IIterativeSolver<Complex32> solver = new MathNet.Numerics.LinearAlgebra.Complex32.Solvers.CompositeSolver(new List<IIterativeSolverSetup<Complex32>> { new UserBiCgStabComplex32() });
            CompositeSolver(matrix, vector, solver);
        }

        void CompositeSolver<T>(Matrix<T> matrixA, Vector<T> vectorB, IIterativeSolver<T> solver) where T : struct, IEquatable<T>, IFormattable
        {
            var formatProvider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            formatProvider.TextInfo.ListSeparator = " ";            

            Console.WriteLine(@"Matrix 'A' with coefficients");
            Console.WriteLine(matrixA.ToString("#0.00\t", formatProvider));
            Console.WriteLine();
            Console.WriteLine(@"Vector 'b' with the constant terms");
            Console.WriteLine(vectorB.ToString("#0.00\t", formatProvider));
            Console.WriteLine();

            var iterationCountStopCriterion = new IterationCountStopCriterion<T>(1000);
            var residualStopCriterion = new ResidualStopCriterion<T>(1e-10);
            var monitor = new Iterator<T>(iterationCountStopCriterion, residualStopCriterion);
            Vector<T> resultX = matrixA.SolveIterative(vectorB, solver, monitor);

            Console.WriteLine(@"2. Solver status of the iterations");
            Console.WriteLine(monitor.Status);
            Console.WriteLine();

            Console.WriteLine(@"3. Solution result vector of the matrix equation");
            Console.WriteLine(resultX.ToString("#0.00\t", formatProvider));
            Console.WriteLine();
        }
    }
}
