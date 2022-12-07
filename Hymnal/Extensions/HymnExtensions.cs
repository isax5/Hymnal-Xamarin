using Hymnal.Models.DataBase;

namespace Hymnal.Extensions;

public static class HymnExtensions
{
    public static FavoriteHymn ToFavoriteHymn(this Hymn hymn)
    {
        return new FavoriteHymn
        {
            Number = hymn.Number,
            HymnalLanguageId = hymn.HymnalLanguageId
        };
    }
}
