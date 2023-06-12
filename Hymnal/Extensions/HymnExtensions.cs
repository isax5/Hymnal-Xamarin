using System.Globalization;
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

    public static RecordHymn ToRecordHymn(this Hymn hymn)
    {
        return new RecordHymn
        {
            Number = hymn.Number,
            HymnalLanguageId = hymn.HymnalLanguageId
        };
    }

    public static RecordHymn ToRecordHymn(this HymnIdParameter hymnIdParameter)
    {
        return new RecordHymn
        {
            Number = hymnIdParameter.Number,
            HymnalLanguageId = hymnIdParameter.HymnalLanguage.Id
        };
    }

    #region Order
    public static IEnumerable<Hymn> OrderByNumber(this IEnumerable<Hymn> hymns)
    {
        return hymns.OrderBy(h => h.Number);
    }

    public static IEnumerable<Hymn> OrderByTitle(this IEnumerable<Hymn> hymns)
    {
        return hymns.OrderBy(h =>
        {
            if (!char.IsLetter(h.Title.First()))
            {
                var ss = h.Title.Substring(1);
                return ss;
            }
            return h.Title;
        });
    }

    public static IEnumerable<IGrouping<string, Hymn>> GroupByNumber(this IEnumerable<Hymn> hymns)
    {
        return hymns.GroupBy(h =>
        {
            switch (h.Number / 10)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    return "1";

                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return "50";

                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    return "100";

                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                    return "150";

                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                    return "200";

                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                    return "250";

                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                    return "300";

                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                    return "350";

                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                    return "400";

                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    return "450";

                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                    return "500";

                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                    return "550";

                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                    return "600";

                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                    return "650";

                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                    return "700";

                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                    return "750";

                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                    return "800";

                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                    return "850";

                default:
                    break;
            }

            return "";
        });
    }

    public static IEnumerable<IGrouping<string, Hymn>> GroupByTitle(this IEnumerable<Hymn> hymns)
    {
        return hymns.GroupBy(h =>
        {
            var letterToUse = 0;

            while (!char.IsLetter(h.Title[letterToUse]))
                letterToUse++;

            return h.Title[letterToUse].ToString().StringRender();
        });
    }

    public static IEnumerable<Hymn> GetRange(this IEnumerable<Hymn> hymns, int start, int end)
    {
        return hymns.OrderByNumber().Where(h => h.Number >= start && h.Number <= end);
    }

    /// <summary>
    /// Search query
    /// </summary>
    /// <param name="hymns"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static IEnumerable<Hymn> SearchQuery(this IEnumerable<Hymn> hymns, string query)
    {
        // TODO: Se puede optimizar la busqueda por termino
        return hymns.Where(h =>
        {
            var queryRendered = query.StringRender().Trim();

            var titleRendered = h.Title.StringRender();
            var contentRendered = h.Content.StringRender();

            return
            h.Number.ToString().ToUpper(CultureInfo.InvariantCulture).Contains(queryRendered)
            || h.Title.ToUpper(CultureInfo.InvariantCulture).Contains(queryRendered)
            || titleRendered.Contains(queryRendered)
            || h.Content.ToUpper(CultureInfo.InvariantCulture).Contains(queryRendered)
            || contentRendered.Contains(queryRendered);
        });
    }
    #endregion

    /// <summary>
    /// Render string for search, lists, order, etc.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static string StringRender(this string query)
    {
        return query
            .ToUpper()
            .Replace('Á', 'A')
            .Replace('À', 'A')
            .Replace('Ã', 'A')
            .Replace('Â', 'A')
            .Replace('Ă', 'A')
            .Replace('É', 'E')
            .Replace('È', 'E')
            .Replace('Ê', 'E')
            .Replace('Í', 'I')
            .Replace('Ì', 'I')
            .Replace('Ó', 'O')
            .Replace('Ò', 'O')
            .Replace('Õ', 'O')
            .Replace('Ô', 'O')
            .Replace('Ú', 'U')
            .Replace('Ù', 'U')
            .Replace('Ü', 'U')
            .Replace('Ñ', 'N')
            .Replace('Ç', 'C')
            .Replace("-", string.Empty)
            .Replace("–", string.Empty)
            .Replace("—", string.Empty)
            .Replace("”", string.Empty)
            .Replace("“", string.Empty)
            .Replace("’", string.Empty)
            .Replace("'", string.Empty)
            .Replace("¡", string.Empty)
            .Replace("!", string.Empty)
            .Replace("¿", string.Empty)
            .Replace("?", string.Empty)
            .Replace(",", string.Empty)
            .Replace(".", string.Empty)
            .Replace(";", string.Empty)
            .Replace(":", string.Empty);
    }
}

/// <summary>
/// Collection
/// </summary>
/// <typeparam name="S"></typeparam>
/// <typeparam name="T"></typeparam>
public sealed class ObservableGroupCollection<S, T> : ObservableRangeCollection<T>
{
    public S Key { get; private set; }

    public ObservableGroupCollection(IGrouping<S, T> group)
        : base(group)
    {
        Key = group.Key;
    }
}
