using System.Collections.Generic;

namespace Hymnal.XF.Services
{
    /// <summary>
    /// Storage objects using Native Preferences
    /// </summary>
    public interface IDataStorageService
    {
        List<T> GetItems<T>();
        void SetItems<T>(List<T> items);
        void DeleteItems<T>(List<T> items);
    }
}
