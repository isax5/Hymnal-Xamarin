using System;
#if __IOS__ || __ANDROID__
using Realms;
#endif

namespace Plugin.StorageManager.Models
{
#if __IOS__ || __ANDROID__
    public class FavoriteHymn : RealmObject, IHymnReference, IStorageModel
#else
    public class FavoriteHymn : IHymnReference, IStorageModel
#endif
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
