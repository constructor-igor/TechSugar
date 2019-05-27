using System.Linq;
using LocalizationInfra;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class ResxTranslationProviderTests
    {
        [Test]
        public void LoadLanguages()
        {
            ResxTranslationProvider provider = new ResxTranslationProvider();
            Assert.That(provider.Languages.Count(), Is.AtLeast(2));
        }
    }
}