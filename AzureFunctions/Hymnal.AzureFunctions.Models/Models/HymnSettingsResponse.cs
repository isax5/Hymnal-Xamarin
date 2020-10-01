using Newtonsoft.Json;

namespace Hymnal.AzureFunctions.Models
{
    public class HymnSettingsResponse
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "ISOLanguage")]
        public string TwoLetterISOLanguageName { get; set; }

        [JsonProperty(PropertyName = "InstrumentalMusicUrl")]
        public string InstrumentalMusicUrl { get; set; }

        [JsonProperty(PropertyName = "SungMusicUrl")]
        public string SungMusicUrl { get; set; }
    }
}
