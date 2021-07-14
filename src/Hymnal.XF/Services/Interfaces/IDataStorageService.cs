using System.Collections.Generic;

namespace Hymnal.XF.Services
{
    /// <summary>
    /// Storage objects using Native Preferences
    /// </summary>
    public interface IDataStorageService
    {
        void DeleteItems<T>(List<T> items);
        List<T> GetItems<T>();
        void ReplaceItems<T>(List<T> items);
    }
}
