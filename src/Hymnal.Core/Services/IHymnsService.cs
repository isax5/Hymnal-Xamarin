using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Plugin.StorageManager.Models;

namespace Hymnal.Core.Services
{
    public interface IHymnsService
    {
        Task<IEnumerable<Hymn>> GetHymnListAsync(HymnalLanguage language);

        Task<Hymn> GetHymnAsync(int number, HymnalLanguage language);

        Task<Hymn> GetHymnAsync(IHymnReference hymnReference);

        Task<IEnumerable<Thematic>> GetThematicListAsync(HymnalLanguage language);
    }
}
