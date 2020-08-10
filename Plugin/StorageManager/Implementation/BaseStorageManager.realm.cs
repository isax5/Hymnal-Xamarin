using System.Linq;
using Realms;

namespace Plugin.StorageManager
{
    /// <summary>
    /// DB implementation using Realm
    /// </summary>
    public class BaseStorageManagerImplementation : IStorageManager
    {
        public Realm RealmInstance;


        public void Add<T>(T item) where T : RealmObject, IStorageModel
        {
            RealmInstance.Write(() => RealmInstance.Add(item));
        }

        public IQueryable<T> All<T>() where T : RealmObject, IStorageModel
        {
            return RealmInstance.All<T>();
        }

        public void Remove<T>(T item) where T : RealmObject, IStorageModel
        {
            using (Transaction trans = RealmInstance.BeginWrite())
            {
                RealmInstance.Remove(item);
                trans.Commit();
            }
        }

        public void RemoveRange<T>(IQueryable<T> items) where T : RealmObject, IStorageModel
        {
            using (Transaction trans = RealmInstance.BeginWrite())
            {
                RealmInstance.RemoveRange(items);
                trans.Commit();
            }
        }
    }
}
