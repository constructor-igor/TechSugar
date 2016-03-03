using NUnit.Framework;

namespace AlgLibMklSamples
{
    [TestFixture]
    public class AlgLibMklIterativeSolversTests
    {
        [Test]
        public void SimpleSample_Identity()
        {
            int m = 3;
            int n = 3;

            alglib.sparsematrix a;
            alglib.sparsecreate(m, n, out a);
            alglib.sparseset(a, 0, 0, 1.0);
            alglib.sparseset(a, 1, 1, 1.0);
            alglib.sparseset(a, 2, 2, 1.0);

            alglib.sparseconverttocrs(a);

            double[] b = { 4, 2, 3 };

            alglib.linlsqrstate s;
            alglib.linlsqrreport rep;
            double[] x;
            alglib.linlsqrcreate(a.innerobj.m, a.innerobj.n, out s);
            alglib.linlsqrsolvesparse(s, a, b);
            alglib.linlsqrresults(s, out x, out rep);

            System.Console.WriteLine("{0}", rep.terminationtype); // EXPECTED: 4
            System.Console.WriteLine("{0}", alglib.ap.format(x, 2)); // EXPECTED: [4.000, 2.000, 3.000]
        }
    }
}
