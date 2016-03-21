using System;
using Extreme.Mathematics;
using Extreme.Mathematics.LinearAlgebra;
using Extreme.Mathematics.LinearAlgebra.IterativeSolvers;
using Extreme.Mathematics.LinearAlgebra.IterativeSolvers.Preconditioners;
using NUnit.Framework;

namespace ExtremeOptimizationSamples
{
    /*
     * Reference:
     * - http://www.extremeoptimization.com/quickstart/CSharp/IterativeSparseSolvers.aspx
     * 
     * */
    [TestFixture]
    public class ExtremeOptimizationIterativeSolversTests
    {
        [Test]
        public void SimpleSample_Double_Identity()
        {
            Console.WriteLine(CoreImplementations<Complex<float>>.LinearAlgebra.Name);
            // We load a sparse matrix and right-hand side from a data file:
            SparseCompressedColumnMatrix<double> matrixA = SparseCompressedColumnMatrix<double>.CreateIdentity(3);
            Vector<double> vectorB = Vector.Create(1.0, 2.0, 3.0);

            IterativeSparseSolver<double> solver = new BiConjugateGradientSolver<double>(matrixA);
            var resultVector = solver.Solve(vectorB);

            Console.WriteLine("Result: {0}", resultVector);
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.SolutionReport.Error);

            // With incomplete LU preconditioner
            solver.Preconditioner = new IncompleteLUPreconditioner<double>(matrixA);
            resultVector = solver.Solve(vectorB);

            Console.WriteLine("Result: {0}", resultVector);
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.EstimatedError);
        }
        [Test]
        public void SimpleSample_ComplexFloat_Identity()
        {
            NumericsConfiguration.NativeProviderPath = @"C:\Program Files (x86)\Extreme Optimization\Numerical Libraries for .NET\bin\Net40";
            NumericsConfiguration.Providers.RegisterSinglePrecisionProvider();
            NumericsConfiguration.AutoLoadNativeProviders = true;
            CoreImplementations<float>.UseNative();
            Console.WriteLine(CoreImplementations<Complex<float>>.LinearAlgebra.Name);

            // We load a sparse matrix and right-hand side from a data file:
            SparseCompressedColumnMatrix<Complex<float>> matrixA = SparseCompressedColumnMatrix<Complex<float>>.CreateIdentity(3);
            Vector<Complex<float>> vectorB = Vector.Create(new Complex<float>(1.0f, 0), new Complex<float>(2.0f, 0), new Complex<float>(3.0f, 0));

            IterativeSparseSolver<Complex<float>> solver = new BiConjugateGradientSolver<Complex<float>>(matrixA);
            DenseVector<Complex<float>> resultVector = solver.Solve(vectorB);

            Console.WriteLine("Result: {0}", resultVector);
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.SolutionReport.Error);

            // With incomplete LU preconditioner
            solver.Preconditioner = new IncompleteLUPreconditioner<Complex<float>>(matrixA);
            resultVector = solver.Solve(vectorB);

            Console.WriteLine("Result: {0}", resultVector);
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.EstimatedError);
        }
        [Test]
        public void SparseSample_ComplexFloat_Identity()
        {
            NumericsConfiguration.NativeProviderPath = @"C:\Program Files (x86)\Extreme Optimization\Numerical Libraries for .NET\bin\Net40";
            NumericsConfiguration.Providers.RegisterSinglePrecisionProvider();
            NumericsConfiguration.AutoLoadNativeProviders = true;
            CoreImplementations<float>.UseNative();
            Console.WriteLine(CoreImplementations<Complex<float>>.LinearAlgebra.Name);

            SparseCompressedColumnMatrix<Complex<float>> matrixA = Matrix.CreateSparse<Complex<float>>(3, 3);
            matrixA.SetValue(new Complex<float>(1, 0), 0, 0);
            matrixA.SetValue(new Complex<float>(1, 0), 1, 1);
            matrixA.SetValue(new Complex<float>(1, 0), 2, 2);
            Vector<Complex<float>> vectorB = Vector.Create(new Complex<float>(1.0f, 0), new Complex<float>(2.0f, 0), new Complex<float>(3.0f, 0));

            IterativeSparseSolver<Complex<float>> solver = new BiConjugateGradientSolver<Complex<float>>(matrixA);
            DenseVector<Complex<float>> resultVector = solver.Solve(vectorB);

            Console.WriteLine("Result: {0}", resultVector);
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.SolutionReport.Error);

            // With incomplete LU preconditioner
            solver.Preconditioner = new IncompleteLUPreconditioner<Complex<float>>(matrixA);
            resultVector = solver.Solve(vectorB);

            Console.WriteLine("Result: {0}", resultVector);
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.EstimatedError);
        }
    }
}
