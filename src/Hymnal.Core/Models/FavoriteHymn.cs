using System;

namespace Hymnal.Core.Models
{
    public class FavoriteHymn : Hymn
    {
        public DateTime SavedAt { get; set; } = DateTime.Now;
        public HymnalLanguage HymnalLanguage { get; set; }
    }
}
