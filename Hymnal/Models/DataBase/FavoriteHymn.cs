namespace Hymnal.Models.DataBase;

public sealed class FavoriteHymn : HymnReference
{
    /// <summary>
    /// Order in list
    /// </summary>
    public int Order { get; set; }
}
