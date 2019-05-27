using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using LocalizationMarkupExtensionExample.Properties;

namespace LocalizationMarkupExtensionExample.Extensions
{
    public class ResxTranslationProvider : ITranslationProvider
    {
        private readonly ResourceManager _resourceManager;
        private readonly List<CultureInfo> _languages;

        public ResxTranslationProvider()
            : this(Resources.ResourceManager)
        {
        }
        public ResxTranslationProvider(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            _languages = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(x => x.TwoLetterISOLanguageName != "iv" &&
                            _resourceManager.GetResourceSet(x, true, false) != null)
                .ToList();
        }
        public ResxTranslationProvider(Type resourceSource)
            : this(new ResourceManager(resourceSource))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ResxTranslationProvider"/> class.
        /// _resourceManager = new ResourceManager(baseName, assembly);
        /// </summary>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="assembly">The assembly.</param>
        public ResxTranslationProvider(string baseName, Assembly assembly)
            : this(new ResourceManager(baseName, assembly))
        {
        }
        /// <summary>
        /// See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public string Translate(string key)
        {
            return _resourceManager.GetString(key);
        }
        public bool HasKey(string key, CultureInfo culture)
        {
            return !string.IsNullOrEmpty(_resourceManager.GetString(key, culture));
        }
        /// <summary>
        /// See <see cref="ITranslationProvider.Languages" />
        /// </summary>
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                return _languages;
            }
        }
    }
}