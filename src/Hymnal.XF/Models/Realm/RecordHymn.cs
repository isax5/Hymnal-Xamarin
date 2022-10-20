using System;
using Realms;

namespace Hymnal.XF.Models.Realm
{
    public sealed class RecordHymn : RealmObject, IHymnReference, IStorageModel, IComparable
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

        public int CompareTo(object obj)
        {
            if (obj is null || obj is not RecordHymn recordHymn)
                return 1;

            return Number.CompareTo(recordHymn.Number);
        }
    }
}
