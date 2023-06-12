namespace Hymnal.Models.DataBase
{
    public sealed class RecordHymn : HymnReference
    {
        public RecordHymn()
        { }

        public RecordHymn(RecordHymn record)
            : this()
        {
            Id = record.Id;
            Number = record.Number;
            SavedAt = record.SavedAt;
            HymnalLanguageId = record.HymnalLanguageId;
        }
    }
}
