using System.Globalization;
using System.Linq;
using LocalizationInfra;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class TranslationManagerTests
    {
        [Test]
        public void Create()
        {
            TranslationManager instance = TranslationManager.Instance;
            Assert.That(instance, Is.Not.Null);
        }
        [Test]
        public void Languages()
        {
            TranslationManager instance = TranslationManager.Instance;
            Assert.That(instance.Languages.Count(), Is.EqualTo(5));
            Assert.That(instance.Languages.First().EnglishName, Is.EqualTo("German (Germany)"));
        }
        [Test]
        public void DefaultCurrentLanguage()
        {
            TranslationManager instance = TranslationManager.Instance;
            Assert.That(instance.CurrentLanguage.EnglishName, Is.EqualTo("English (United States)"));
        }
        [Test]
        public void Key()
        {
            TranslationManager instance = TranslationManager.Instance;
            string sendValue = instance.Translate("Send");
            Assert.That(sendValue, Is.EqualTo("Send"));
        }
        [Test]
        public void ChangeLanguage()
        {
            TranslationManager instance = TranslationManager.Instance;
            instance.CurrentLanguage = FindCultureByName("English (United States)");
            string sendValue = instance.Translate("Send");
            Assert.That(sendValue, Is.EqualTo("Send"));

            //instance.CurrentLanguage = instance.Languages.First();
            instance.CurrentLanguage = FindCultureByName("German (Germany)");
            Assert.That(instance.CurrentLanguage.EnglishName, Is.EqualTo("German (Germany)"));
            string sendValueGermany = instance.Translate("Send");
            Assert.That(sendValueGermany, Is.EqualTo("Senden"));
        }

        private CultureInfo FindCultureByName(string cultureEnglishName)
        {
            return TranslationManager.Instance.Languages.First(l => l.EnglishName == cultureEnglishName);
        }
    }
}