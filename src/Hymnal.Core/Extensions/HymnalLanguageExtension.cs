using Hymnal.Core.Models;
using System.Linq;

namespace Hymnal.Core.Extensions
{
    public static class HymnalLanguageExtension
    {
        /// <summary>
        /// Get information updated about this Language
        /// Useful for Favorites and History Hymnals
        /// </summary>
        public static HymnalLanguage Configuration(this HymnalLanguage hymnalLanguage)
        {
            return Constants.HymnsLanguages.First(hl => hl.Id == hymnalLanguage.Id);
        }

        /// <summary>
        /// Get Instrument URL
        /// </summary>
        /// <param name="hymnalLanguage"></param>
        /// <param name="hymnNumber"></param>
        /// <returns></returns>
        public static string GetInstrumentURL(this HymnalLanguage hymnalLanguage, int hymnNumber)
        {
            // Number in 3 digits number when it's less than 3 digits. In an other situation
            // the number will not change
            return hymnalLanguage.InstrumentalMusic.Replace("###", hymnNumber.ToString("D3"));
        }

        /// <summary>
        /// Get Sung URL
        /// </summary>
        /// <param name="hymnalLanguage"></param>
        /// <param name="hymnNumber"></param>
        /// <returns></returns>
        public static string GetSungURL(this HymnalLanguage hymnalLanguage, int hymnNumber)
        {
            // Number in 3 digits number when it's less than 3 digits. In an other situation
            // the number will not change
            return hymnalLanguage.SungMusic.Replace("###", hymnNumber.ToString("D3"));
        }
    }
}
