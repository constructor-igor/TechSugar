using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using LocalizationInfra;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class ResxTranslationProviderFromOtherProjectTests
    {
        [Test]
        public void LoadLanguagesFromOtherProject()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string baseName = "LocalizationData.Properties.Resources";
            string assemblyResourcesFile = Path.Combine(assemblyFolder, "LocalizationData.dll");
            Assembly assembly = Assembly.LoadFrom(assemblyResourcesFile);
            ResxTranslationProvider provider = new ResxTranslationProvider(baseName, assembly);
            Assert.That(provider.Languages.Count(), Is.AtLeast(1));
            foreach (CultureInfo language in provider.Languages)
            {
                Console.WriteLine($@"language: {language.EnglishName}");
            }
        }
        [Test]
        public void Translate()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string baseName = "LocalizationData.Properties.Resources";
            string assemblyResourcesFile = Path.Combine(assemblyFolder, "LocalizationData.dll");
            Assembly assembly = Assembly.LoadFrom(assemblyResourcesFile);

            TranslationManager.CreateInstanceFrom(baseName, assembly);
            TranslationManager instance = TranslationManager.Instance;
            instance.CurrentLanguage = FindCultureByName("Belarusian (Belarus)");
            string sendValue = instance.Translate("Header");
            Assert.That(sendValue, Is.EqualTo("Прывет"));
        }
        private CultureInfo FindCultureByName(string cultureEnglishName)
        {
            return TranslationManager.Instance.Languages.First(l => l.EnglishName == cultureEnglishName);
        }
    }
}