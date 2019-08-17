using System.Collections.Generic;
using System.Linq;

namespace Hymnal.Core.Models
{
    public static class HymnExtension
    {
        public static IEnumerable<Hymn> OrderByNumber(this IEnumerable<Hymn> hymns)
        {
            return hymns.OrderBy(h => h.ID);
        }

        public static IEnumerable<Hymn> OrderByTitle(this IEnumerable<Hymn> hymns)
        {
            return hymns.OrderBy(h => h.Title);
        }
    }
}
