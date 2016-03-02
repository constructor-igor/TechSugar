using NUnit.Framework;

namespace AlgLibSamples
{
    /*
     * References:
     * - http://www.alglib.net/translator/man/manual.csharp.html#example_linlsqr_d_1
     * 
     * */
    [TestFixture]
    public class AlgLibIterativeSolversTests
    {
        [Test]
        public void SimpleSample()
        {
            int m = 5;
            int n = 2;

            alglib.sparsematrix a;
            alglib.sparsecreate(m, n, out a);
            alglib.sparseset(a, 0, 0, 1.0);
            alglib.sparseset(a, 0, 1, 1.0);
            alglib.sparseset(a, 1, 0, 1.0);
            alglib.sparseset(a, 1, 1, 1.0);
            alglib.sparseset(a, 2, 0, 2.0);
            alglib.sparseset(a, 2, 1, 1.0);
            alglib.sparseset(a, 3, 0, 1.0);
            alglib.sparseset(a, 4, 1, 1.0);

            alglib.sparseconverttocrs(a);

            double[] b = { 4, 2, 4, 1, 2 };

            alglib.linlsqrstate s;
            alglib.linlsqrreport rep;
            double[] x;
            alglib.linlsqrcreate(a.innerobj.m, a.innerobj.n, out s);
            alglib.linlsqrsolvesparse(s, a, b);
            alglib.linlsqrresults(s, out x, out rep);

            System.Console.WriteLine("{0}", rep.terminationtype); // EXPECTED: 4
            System.Console.WriteLine("{0}", alglib.ap.format(x, 2)); // EXPECTED: [1.000,2.000]
        }
    }
}
