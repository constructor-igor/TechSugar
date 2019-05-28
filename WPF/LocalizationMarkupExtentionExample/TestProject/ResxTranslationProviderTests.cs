using System;
using System.Globalization;
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
            Assert.That(provider.Languages.Count(), Is.EqualTo(5));
            foreach (CultureInfo language in provider.Languages)
            {
                Console.WriteLine($@"language: {language.EnglishName}");
            }
        }
    }
}