using System.Linq;
using Realms;

namespace Hymnal.XF.Services
{
    public interface IStorageManager
    {
        void Add<T>(T item) where T : RealmObject;
        IQueryable<T> All<T>() where T : RealmObject;
        void Remove<T>(T item) where T : RealmObject;
        void RemoveRange<T>(IQueryable<T> items) where T : RealmObject;
    }
}
