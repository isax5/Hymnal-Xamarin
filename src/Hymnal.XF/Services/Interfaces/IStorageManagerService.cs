using System.Linq;
using Hymnal.XF.Models.Realm;
using Realms;

namespace Hymnal.XF.Services
{
    public interface IStorageManagerService
    {
        void Add<T>(T item) where T : RealmObject, IStorageModel;
        IQueryable<T> All<T>() where T : RealmObject, IStorageModel;
        void Remove<T>(T item) where T : RealmObject, IStorageModel;
        void RemoveRange<T>(IQueryable<T> items) where T : RealmObject, IStorageModel;
    }
}
