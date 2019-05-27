using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using LocalizationInfra.Properties;

namespace LocalizationInfra
{
    public class TranslationManager
    {
        private static TranslationManager _translationManager;
        private TranslationManager()
        {
        }
        public event EventHandler LanguageChanged;
        public static TranslationManager Instance
        {
            get
            {
                if (_translationManager != null) return _translationManager;
                _translationManager = new TranslationManager
                {
                    TranslationProvider = new ResxTranslationProvider(),
                    CurrentLanguage = Settings.Default.Culture
                };
                return _translationManager;
            }
        }
        public CultureInfo CurrentLanguage
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (!Equals(value, Thread.CurrentThread.CurrentUICulture))
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    OnLanguageChanged();
                }
            }
        }
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                if (TranslationProvider != null)
                {
                    return TranslationProvider.Languages;
                }
                return Enumerable.Empty<CultureInfo>();
            }
        }
        public ITranslationProvider TranslationProvider { get; set; }
        private void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }
        public string Translate(string key)
        {
            string translatedValue = TranslationProvider?.Translate(key);
            if (translatedValue != null)
            {
                return translatedValue;
            }
            return $"!{key}!";
        }
        public bool HasKey(string key, CultureInfo culture)
        {
            return TranslationProvider.HasKey(key, culture);
        }
    }
}
