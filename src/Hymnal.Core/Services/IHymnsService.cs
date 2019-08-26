using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.Core.Models;

namespace Hymnal.Core.Services
{
    public interface IHymnsService
    {
        Task<IEnumerable<Hymn>> GetHymnListAsync(HymnalLanguage language);

        Task<Hymn> GetHymnAsync(int number, HymnalLanguage language);

        Task<IEnumerable<Thematic>> GetThematicListAsync(HymnalLanguage language);
    }
}
