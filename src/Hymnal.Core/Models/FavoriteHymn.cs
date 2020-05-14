using System;
using Realms;

namespace Hymnal.Core.Models
{
    public class FavoriteHymn : RealmObject, IHymnReference
    {
        /// <summary>
        /// Hymn Number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Saved At
        /// </summary>
        public DateTimeOffset SavedAt { get; set; }

        /// <summary>
        /// Language Id <see cref="HymnalLanguage.Id"/>
        /// </summary>
        public string HymnalLanguageId { get; set; }
    }
}
