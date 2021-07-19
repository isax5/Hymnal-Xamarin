using System.Linq;
using Hymnal.XF.Constants;
using Hymnal.XF.Models;

namespace Hymnal.XF.Extensions
{
    public static class HymnalLanguageExtension
    {
        /// <summary>
        /// Get updated information about this Language
        /// Useful for Favorites and Records Hymnals becouse the name of the files of the hymals can change
        /// </summary>
        public static HymnalLanguage Configuration(this HymnalLanguage hymnalLanguage)
        {
            return InfoConstants.HymnsLanguages.First(hl => hl.Id == hymnalLanguage.Id);
        }

        /// <summary>
        /// Get Music Sheet
        /// </summary>
        /// <param name="hymnalLanguage"></param>
        /// <param name="hymnNymber"></param>
        /// <returns></returns>
        public static string GetMusicSheetSource(this HymnalLanguage hymnalLanguage, int hymnNymber)
        {
            return hymnalLanguage.HymnsSheetsFileName.Replace("###", hymnNymber.ToString("D3"));
        }
    }
}
