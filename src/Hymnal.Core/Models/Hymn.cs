using Newtonsoft.Json;

namespace Hymnal.Core.Models
{
    public class Hymn
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
