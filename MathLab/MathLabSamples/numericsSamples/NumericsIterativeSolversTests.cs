using System;
using System.Collections.Generic;
using System.Globalization;
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
            CompositeSolver(matrix, vector);
        }
        [Test]
        public void CompositeSolver_SparseFloat()
        {
            Matrix<float> matrix = SparseMatrix.OfArray(new[,] {{1f, 0f, 0f}, {0f, 1f, 0f}, {0f, 0f, 1f}});
            Vector<float> vector = new DenseVector(new[] {1f, 2f, 3f});
            CompositeSolver(matrix, vector);
        }
        void CompositeSolver(Matrix<float> matrixA, Vector<float> vectorB)
        {
            var formatProvider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            formatProvider.TextInfo.ListSeparator = " ";

            IIterativeSolver<float> solver = new CompositeSolver(new List<IIterativeSolverSetup<float>> {new UserBiCgStabFloat()});

            //Matrix<float> matrixA = SparseMatrix.OfArray(new[,] {{5.00f, 2.00f, -4.00f}, {3.00f, -7.00f, 6.00f}, {4.00f, 1.00f, 5.00f}});
            //Vector<float> vectorB = new DenseVector(new[] {-7.0f, 38.0f, 43.0f});

            Console.WriteLine(@"Matrix 'A' with coefficients");
            Console.WriteLine(matrixA.ToString("#0.00\t", formatProvider));
            Console.WriteLine();
            Console.WriteLine(@"Vector 'b' with the constant terms");
            Console.WriteLine(vectorB.ToString("#0.00\t", formatProvider));
            Console.WriteLine();

            var iterationCountStopCriterion = new IterationCountStopCriterion<float>(1000);
            var residualStopCriterion = new ResidualStopCriterion<float>(1e-10);
            var monitor = new Iterator<float>(iterationCountStopCriterion, residualStopCriterion);
            Vector<float> resultX = matrixA.SolveIterative(vectorB, solver, monitor);

            Console.WriteLine(@"2. Solver status of the iterations");
            Console.WriteLine(monitor.Status);
            Console.WriteLine();

            Console.WriteLine(@"3. Solution result vector of the matrix equation");
            Console.WriteLine(resultX.ToString("#0.00\t", formatProvider));
            Console.WriteLine();
        }
    }

    public class MyPreconditioner : IPreconditioner<float>
    {
        readonly IPreconditioner<float> m_actual = new ILU0Preconditioner();
        public MyPreconditioner()
        {            
        }

        #region IPreconditioner<float>
        public void Initialize(Matrix<float> matrix)
        {
            m_actual.Initialize(matrix);
        }
        public void Approximate(Vector<float> rhs, Vector<float> lhs)
        {
            m_actual.Approximate(rhs, lhs);
        }
        #endregion
    }
}
