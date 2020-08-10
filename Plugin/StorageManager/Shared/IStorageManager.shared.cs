using System.Linq;
#if __IOS__ || __ANDROID__
using Realms;
#endif


namespace Plugin.StorageManager
{
    public interface IStorageManager
    {
#if __IOS__ || __ANDROID__
        void Add<T>(T item) where T : RealmObject, IStorageModel;
        IQueryable<T> All<T>() where T : RealmObject, IStorageModel;
        void Remove<T>(T item) where T : RealmObject, IStorageModel;
        void RemoveRange<T>(IQueryable<T> items) where T : RealmObject, IStorageModel;
#else
        void Add<T>(T item) where T : IStorageModel;
        IQueryable<T> All<T>() where T : IStorageModel;
        void Remove<T>(T item) where T : IStorageModel;
        void RemoveRange<T>(IQueryable<T> items) where T : IStorageModel;
#endif
    }
}
