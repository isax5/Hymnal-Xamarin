using Newtonsoft.Json;

namespace Hymnal.Models;

/// <summary>
/// <see cref="Ambit"/> for <see cref="Thematic"/> for <see cref="Hymn"/>
/// </summary>
public sealed class Ambit
{
    [JsonProperty("ambit")]
    public string AmbitName { get; set; }

    [JsonProperty("star")]
    public int Star { get; set; }

    [JsonProperty("end")]
    public int End { get; set; }

    [JsonProperty("backimage")]
    public string BackImage { get; set; }
}
