using System;

namespace Hymnal.Core.Models
{
    public class HistoryHymn : Hymn
    {
        public DateTime SavedAt { get; set; } = DateTime.Now;
    }
}
