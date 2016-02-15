using System.Globalization;
using NUnit.Framework;
//[assembly: SetCulture("")]

namespace NUnit_v3_samples
{    
    [TestFixture]
    //[SetCulture("")]
    public class NUnitIssue1266
    {
        [Test]
        //[SetCulture("")]
        public void It_should_set_proper_culture()
        {
            Assert.That(CultureInfo.CurrentCulture, Is.EqualTo(CultureInfo.InvariantCulture));
        }

//        [Test]
//        public void It_should_not_set_proper_culture()
//        {
//            Assert.That(CultureInfo.CurrentCulture, Is.Not.EqualTo(CultureInfo.InvariantCulture));
//        }
    }
}