using System;
using System.Diagnostics;
using Extreme.Mathematics;
using Extreme.Mathematics.Generic;
using Extreme.Mathematics.Generic.LinearAlgebra;
using Extreme.Mathematics.LinearAlgebra.IterativeSolvers;
using Extreme.Mathematics.LinearAlgebra.IterativeSolvers.Preconditioners;

namespace ExtremeOptimizationSamples
{
    public class Program
    {
        public void ExecuteSample()
        {

            // The line below sets the path where the native assemblies
            // are located. The "XO_LIBRARY_PATH" environment variable
            // points here, too.
            NumericsConfiguration.NativeProviderPath =
                @"C:\Program Files (x86)\Extreme Optimization\Numerical Libraries for .NET\bin\Net40";

            // Register the single precision providers.
            NumericsConfiguration.Providers.RegisterSinglePrecisionProvider();
            NumericsConfiguration.AutoLoadNativeProviders = true;
            CoreImplementations<float>.UseNative();

            // Which provider are we using?
            Console.WriteLine(CoreImplementations<Complex<float>>.LinearAlgebra.Name);

            int N = 228724; // size
            int K = 96;    // non-zeros per column

            // Create some random matrices. Code is below.
            // Use a seed so we can reproduce the same values.
            NumericsConfiguration.DefaultRandomNumberGenerator = new Extreme.Mathematics.Random.MersenneTwister(117);

            var matrixA = CreateSparseRandom(N, K);
            var vectorB = CreateRandom(N);// CreateRandom(N);

            // Now run the solver with and without preconditioner:
            var sw = Stopwatch.StartNew();
            var solver = new BiConjugateGradientSolver<Complex<float>>(matrixA);

            Console.WriteLine("Starting solve...");
            Vector<Complex<float>> resultVector;
            resultVector = solver.Solve(vectorB);
            sw.Stop();

            Console.WriteLine("Result: {0}", resultVector.GetSlice(0, 10));
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.SolutionReport.Error);
            Console.WriteLine("Total time: {0} s", sw.Elapsed.TotalSeconds);

            // With incomplete LU preconditioner
            sw.Restart();
            solver.Preconditioner = new IncompleteLUPreconditioner<Complex<float>>(matrixA);
            resultVector = solver.Solve(vectorB);
            sw.Stop();

            Console.WriteLine("Result: {0}", resultVector.GetSlice(0, 10));
            Console.WriteLine("Solved in {0} iterations.", solver.IterationsNeeded);
            Console.WriteLine("Estimated error: {0}", solver.EstimatedError);
            Console.WriteLine("Total time: {0} s", sw.Elapsed.TotalSeconds);

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        static SparseCompressedColumnMatrix<Complex<float>> CreateSparseRandom(int N, int K)
        {
            var rng = NumericsConfiguration.DefaultRandomNumberGenerator;
            var values = new Complex<float>[N * K];
            var columns = new int[N * K];
            var rows = new int[N * K];
            int index = 0;
            for (int i = 0; i < N; i++)
            {
                int index0 = index;
                rows[index] = i;
                columns[index] = i;
                values[index] = 1.4f * K;
                for (int k = 1; k < K; k++)
                {
                retry:
                    int j = rng.Next(N);
                    if (j == i)
                        goto retry;
                    ++index;
                    rows[index] = j;
                    columns[index] = i;
                    values[index] = new Complex<float>(
                        (float)rng.NextDouble(), (float)rng.NextDouble());
                }
                index++;
                Array.Sort(rows, values, index0, index - index0);
            }
            return Matrix.CreateSparse(N, N, rows, columns, values);
        }

        static Vector<Complex<float>> CreateRandom(int N)
        {
            var rng = NumericsConfiguration.DefaultRandomNumberGenerator;
            return Vector.Create(N, _ => new Complex<float>(
                (float)rng.NextDouble(), (float)rng.NextDouble()));
        }
    }

    // All we need for the sparse solver to work is a matrix-vector product.
    // If the matrix has structure, it may be more efficient to
    // compute the matrix-vector product directly without forming the
    // sparse matrix. The LinearOperator class encapsulates this functionality.
    // We need to implement two methods to make this work.

    // The code below implements this method for the 2D wave equation
    // over a square grid of a given size.
    class Wave2DOperator : LinearOperator<float>
    {
        int n; // size of the original grid
        int N; // # row and columns of the operator, equal to n*n
        float alpha; // Model factor.

        public Wave2DOperator(int size, float alpha)
            : base(size * size, size * size)
        {
            this.n = size;
            this.N = size * size;
            this.alpha = alpha;
        }

        public override Vector<float> LeastSquaresSolveInto(Vector<float> rightHandSide, Vector<float> result)
        {
            throw new NotSupportedException();
        }

        public override Vector<float> SolveInto(TransposeOperation operation, Vector<float> rightHandSide, Vector<float> result)
        {
            throw new NotSupportedException();
        }

        public override int Rank(float tolerance)
        {
            return this.N;
        }

        // Evaluate result=leftFactor*left + productFactor*this^transpose*rightFactor
        protected override Vector<float> MultiplyAndAddAsLeftFactorCore(
            float leftFactor,
            Vector<float> left,
            float productFactor,
            TransposeOperation transpose,
            Vector<float> rightFactor,
            Vector<float> result)
        {
            // The matrix has 1+4*alpha on the main diagonal,
            // and -alpha on the 1st and nth sub and superdiagonal.

            // result may be null. Using MultiplyInto will create
            // a vector if necessary and will return it.
            result = Vector.MultiplyInto(leftFactor, left, result);
            // Diagonal
            result.AddScaledInPlace(1.0f + 4.0f * alpha, rightFactor);
            // Superdiagonal 1
            result.GetSlice(0, N - 2, 1, Intent.WritableView)
                .AddScaledInPlace(-alpha, rightFactor.GetSlice(1, N - 1));
            // Superdiagonal size
            result.GetSlice(0, N - n - 1, 1, Intent.WritableView)
                .AddScaledInPlace(-alpha, rightFactor.GetSlice(n, N - 1));

            // Subdiagonal 1
            result.GetSlice(1, N - 1, 1, Intent.WritableView)
                .AddScaledInPlace(-alpha, rightFactor.GetSlice(0, N - 2));
            // Subdiagonal size
            result.GetSlice(n, N - 1, 1, Intent.WritableView)
                .AddScaledInPlace(-alpha, rightFactor.GetSlice(0, N - n - 1));

            return result;
        }

        // Evaluate result=this^transpose*right
        protected override Vector<float> MultiplyAsLeftCore(TransposeOperation transpose, Vector<float> right, Vector<float> result)
        {
            // Diagonal
            result = Vector.MultiplyInto(1.0f + 4.0f * alpha, right, result);
            // Superdiagonal 1
            result.GetSlice(0, N - 2, 1, Intent.WritableView)
                .AddScaledInPlace(-alpha, right.GetSlice(1, N - 1));
            // Superdiagonal size
            result.GetSlice(0, N - n - 1, 1, Intent.WritableView)
                .AddScaledInPlace(-alpha, right.GetSlice(n, N - 1));

            // Subdiagonal 1
            result.GetSlice(1, N - 1, 1, Intent.WritableView)
                .AddScaledInPlace(-alpha, right.GetSlice(0, N - 2));
            // Subdiagonal size
            result.GetSlice(n, N - 1, 1, Intent.WritableView)
                .AddScaledInPlace(-alpha, right.GetSlice(0, N - n - 1));

            return result;
        }
    }
}