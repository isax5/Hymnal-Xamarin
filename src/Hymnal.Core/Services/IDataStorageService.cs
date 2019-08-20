using System.Collections.Generic;

namespace Hymnal.Core.Services
{
    public interface IDataStorageService
    {
        void DeleteItems<T>(List<T> items);
        List<T> GetItems<T>();
        void ReplaceItems<T>(List<T> items);
    }
}
