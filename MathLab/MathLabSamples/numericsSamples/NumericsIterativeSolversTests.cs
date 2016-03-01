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
        [Test, TestCaseSource("MatrixVectorData")]
        public void CompositeSolver_SparseFloat(Matrix<float> matrixA, Vector<float> vectorB)
        {
            var formatProvider = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            formatProvider.TextInfo.ListSeparator = " ";

            var solver = new CompositeSolver(new List<IIterativeSolverSetup<float>> { new UserBiCgStab() });

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
            var resultX = matrixA.SolveIterative(vectorB, solver, monitor);

            Console.WriteLine(@"2. Solver status of the iterations");
            Console.WriteLine(monitor.Status);
            Console.WriteLine();

            Console.WriteLine(@"3. Solution result vector of the matrix equation");
            Console.WriteLine(resultX.ToString("#0.00\t", formatProvider));
            Console.WriteLine();
        }

        static readonly object[] MatrixVectorData =
        {
            new object[]
            {
                SparseMatrix.OfArray(new[,] {{5.00f, 2.00f, -4.00f}, {3.00f, -7.00f, 6.00f}, {4.00f, 1.00f, 5.00f}}), 
                new DenseVector(new[] {-7.0f, 38.0f, 43.0f})
            },
            new object[]
            {
                SparseMatrix.OfArray(new[,] {{1f, 0f, 0f}, {0f, 1f, 0f}, {0f, 0f, 1f}}), 
                new DenseVector(new[] {1f, 2f, 3f})
            }
        };
    }

    /// <summary>
    /// Sample of user-defined solver setup
    /// </summary>
    public class UserBiCgStab : IIterativeSolverSetup<float>
    {
        /// <summary>
        /// Gets the type of the solver that will be created by this setup object.
        /// </summary>
        public Type SolverType
        {
            get { return null; }
        }

        /// <summary>
        /// Gets type of preconditioner, if any, that will be created by this setup object.
        /// </summary>
        public Type PreconditionerType
        {
            get { return null; }
        }

        /// <summary>
        /// Creates a fully functional iterative solver with the default settings
        /// given by this setup.
        /// </summary>
        /// <returns>A new <see cref="IIterativeSolver{T}"/>.</returns>
        public IIterativeSolver<float> CreateSolver()
        {
            return new BiCgStab();
        }

        public IPreconditioner<float> CreatePreconditioner()
        {
            return null;
        }

        /// <summary>
        /// Gets the relative speed of the solver.
        /// </summary>
        /// <value>Returns a value between 0 and 1, inclusive.</value>
        public double SolutionSpeed
        {
            get { return 0.99; }
        }

        /// <summary>
        /// Gets the relative reliability of the solver.
        /// </summary>
        /// <value>Returns a value between 0 and 1 inclusive.</value>
        public double Reliability
        {
            get { return 0.99; }
        }
    }
}
