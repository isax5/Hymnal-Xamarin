using SQLite;

namespace Hymnal.Models.DataBase;

public abstract class StorageModel
{
    /// <summary>
    /// Database Id
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// Saved At
    /// </summary>
    public DateTime SavedAt { get; set; } = DateTime.Now;
}
