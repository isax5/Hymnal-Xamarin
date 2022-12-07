namespace Hymnal.Models.DataBase;

public abstract class HymnReference : StorageModel
{
    /// <summary>
    /// Hymn Number
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Language Id <see cref="HymnalLanguage.Id"/>
    /// </summary>
    public string HymnalLanguageId { get; set; }
}
