using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.Core.Models;

namespace Hymnal.Core.Services
{
    public interface IHymnsService
    {
        Task<IEnumerable<Hymn>> GetHymnListAsync();

        Task<Hymn> GetHymnAsync(int number);

        Task<IEnumerable<Thematic>> GetThematicListAsync();
    }
}
