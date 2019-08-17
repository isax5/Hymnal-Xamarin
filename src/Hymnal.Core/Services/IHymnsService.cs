using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.Core.Models;

namespace Hymnal.Core.Services
{
    public interface IHymnsService
    {
        Task<IEnumerable<Hymn>> GetHymnsAsync();
        Task<Hymn> GetHymnAsync(int number);
    }
}
