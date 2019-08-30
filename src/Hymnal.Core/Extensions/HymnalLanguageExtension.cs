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
    }
}