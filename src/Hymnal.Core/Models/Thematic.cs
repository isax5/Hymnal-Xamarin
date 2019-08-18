using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hymnal.Core.Models
{
    public class Thematic
    {
        [JsonProperty("thematic")]
        public string ThematicName { get; set; }

        [JsonProperty("ambits")]
        public List<Ambit> Ambits { get; set; }
    }
}
