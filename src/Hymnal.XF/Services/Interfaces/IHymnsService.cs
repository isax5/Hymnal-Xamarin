using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.XF.Models;
using Hymnal.StorageModels.Models;

namespace Hymnal.XF.Services
{
    public interface IHymnsService
    {
        Task<IEnumerable<Hymn>> GetHymnListAsync(HymnalLanguage language);

        Task<Hymn> GetHymnAsync(int number, HymnalLanguage language);

        Task<Hymn> GetHymnAsync(IHymnReference hymnReference);

        Task<IEnumerable<Thematic>> GetThematicListAsync(HymnalLanguage language);
    }
}
