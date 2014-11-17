using Customer.TextData;
using NUnit.Framework;

namespace SanityTests
{
    [TestFixture]
    public class TextDataTests
    {
        [Test]
        public void Test()
        {
            CustomerTextData textData = new CustomerTextData("Black and White\nBlack");
            int countOfBlacks = textData.GetNumberOf("Black");
            Assert.AreEqual(2, countOfBlacks);
        }
    }
}
