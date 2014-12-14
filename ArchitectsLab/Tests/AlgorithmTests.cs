using Moq;
using NUnit.Framework;
using SimpleMathSample;

namespace Tests
{
    [TestFixture]
    public class AlgorithmTests
    {
        [Test]
        [ExpectedException(typeof(AlgorithmNegativeCalcException))]
        public void Calc_NegativeXY_AlgorithmNegativeCalcException()
        {
            const double x = 10;
            const double y = 20;
            const double z = 30;
            var mock = new Mock<CustomMath>();
            mock.Setup(foo => foo.Calc(It.IsAny<double>(), It.IsAny<double>())).Returns(-1);
            var algorithm = new Algorithm();
            algorithm.Run(mock.Object, x, y, z);
        }
    }
}