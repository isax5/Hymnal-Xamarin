using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hymnal.Core.Services
{
    public interface IDataStorage
    {
        Task<List<T>> TableAsync<T>() where T : new();
    }
}
