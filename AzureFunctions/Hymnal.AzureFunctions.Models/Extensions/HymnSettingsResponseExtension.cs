using Hymnal.AzureFunctions.Models;

namespace Hymnal.AzureFunctions.Extensions
{
    public static class HymnSettingsResponseExtension
    {
        /// <summary>
        /// Get Instrument URL
        /// </summary>
        /// <param name="hymnalLanguage"></param>
        /// <param name="hymnNumber"></param>
        /// <returns></returns>
        public static string GetInstrumentURL(this HymnSettingsResponse hymnSettings, int hymnNumber)
        {
            // Number in 3 digits number when it's less than 3 digits. In an other situation
            // the number will not change
            return hymnSettings.InstrumentalMusicUrl.Replace("###", hymnNumber.ToString("D3"));
        }

        /// <summary>
        /// Get Sung URL
        /// </summary>
        /// <param name="hymnalLanguage"></param>
        /// <param name="hymnNumber"></param>
        /// <returns></returns>
        public static string GetSungURL(this HymnSettingsResponse hymnSettings, int hymnNumber)
        {
            // Number in 3 digits number when it's less than 3 digits. In an other situation
            // the number will not change
            return hymnSettings.SungMusicUrl.Replace("###", hymnNumber.ToString("D3"));
        }
    }
}
