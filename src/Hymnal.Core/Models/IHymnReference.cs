namespace Hymnal.Core.Models
{
    public interface IHymnReference
    {
        /// <summary>
        /// Hymn Number
        /// </summary>
        int Number { get; set; }

        /// <summary>
        /// Language Id <see cref="HymnalLanguage.Id"/>
        /// </summary>
        string HymnalLanguageId { get; set; }
    }
}
