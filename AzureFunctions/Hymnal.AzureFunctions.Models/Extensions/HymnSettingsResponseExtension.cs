using Hymnal.AzureFunctions.Models;

namespace Hymnal.AzureFunctions.Extensions
{
    public static class HymnSettingsResponseExtension
    {
        /// <summary>
        /// Get Instrument URL
        /// </summary>
        /// <param name="hymnSettingsResponse"></param>
        /// <param name="hymnNumber"></param>
        /// <returns></returns>
        public static string GetInstrumentUrl(this HymnSettingsResponse hymnSettingsResponse, int hymnNumber)
        {
            // Number in 3 digits number when it's less than 3 digits. In an other situation
            // the number will not change
            return hymnSettingsResponse.InstrumentalMusicUrl.Replace("###", hymnNumber.ToString("D3"));
        }

        /// <summary>
        /// Get Sung URL
        /// </summary>
        /// <param name="hymnSettingsResponse"></param>
        /// <param name="hymnNumber"></param>
        /// <returns></returns>
        public static string GetSungUrl(this HymnSettingsResponse hymnSettingsResponse, int hymnNumber)
        {
            // Number in 3 digits number when it's less than 3 digits. In an other situation
            // the number will not change
            return hymnSettingsResponse.SungMusicUrl.Replace("###", hymnNumber.ToString("D3"));
        }

        /// <summary>
        /// Supports instrumental music
        /// </summary>
        /// <param name="hymnSettingsResponse"></param>
        /// <returns></returns>
        public static bool SupportsInstrumentalMusic(this HymnSettingsResponse hymnSettingsResponse)
        {
            return !string.IsNullOrEmpty(hymnSettingsResponse.InstrumentalMusicUrl);
        }

        /// <summary>
        /// Supports sung music
        /// </summary>
        /// <param name="hymnSettingsResponse"></param>
        /// <returns></returns>
        public static bool SupportsSungMusic(this HymnSettingsResponse hymnSettingsResponse)
        {
            return !string.IsNullOrEmpty(hymnSettingsResponse.SungMusicUrl);
        }

        /// <summary>
        /// Supports music
        /// </summary>
        /// <param name="hymnSettingsResponse"></param>
        /// <returns></returns>
        public static bool SupportsMusic(this HymnSettingsResponse hymnSettingsResponse) =>
            hymnSettingsResponse.SupportsInstrumentalMusic() || hymnSettingsResponse.SupportsSungMusic();
    }
}
