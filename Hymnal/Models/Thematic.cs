using Newtonsoft.Json;

namespace Hymnal.Models;

public sealed class Thematic
{
    [JsonProperty("thematic")]
    public string ThematicName { get; set; }

    [JsonProperty("ambits")]
    public List<Ambit> Ambits { get; set; }
}
