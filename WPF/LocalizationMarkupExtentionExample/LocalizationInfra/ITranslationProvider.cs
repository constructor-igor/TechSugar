using System.Collections.Generic;
using System.Globalization;

namespace LocalizationInfra
{
    public interface ITranslationProvider
    {
        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Translate(string key);
        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <value>The available languages.</value>
        IEnumerable<CultureInfo> Languages { get; }

        bool HasKey(string key, CultureInfo culture);
    }
}