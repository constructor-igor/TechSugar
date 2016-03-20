using System;
using System.Collections.Generic;
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
        public void SimpleSample_Identity()
        {
            // We load a sparse matrix and right-hand side from a data file:
            SparseCompressedColumnMatrix matrixA = SparseCompressedColumnMatrix.CreateIdentity(3);
            Vector vectorB = Vector.Create(new List<double> {1.0, 2.0, 3.0});

            IterativeSparseSolver solver = new BiConjugateGradientSolver(matrixA);
            DenseVector resultVector = solver.Solve(vectorB);

            Console.WriteLine("Result: {0}", resultVector);
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.SolutionReport.Error);

            // With incomplete LU preconditioner
            solver.Preconditioner = new IncompleteLUPreconditioner(matrixA);
            resultVector = solver.Solve(vectorB);

            Console.WriteLine("Result: {0}", resultVector);
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.EstimatedError);
        }
    }
}
