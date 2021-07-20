using System.Linq;
using Hymnal.XF.Models.Realm;
using Realms;

namespace Hymnal.XF.Services
{
    /// <summary>
    /// DB implementation using Realm
    /// </summary>
    public class StorageManagerService : IStorageManagerService
    {
        private readonly Realm realmInstance;

        public StorageManagerService()
        {
            realmInstance = Realm.GetInstance();
        }

        /// <summary>
        /// Add item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Add<T>(T item) where T : RealmObject, IStorageModel
        {
            realmInstance.Write(() => realmInstance.Add(item));
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> All<T>() where T : RealmObject, IStorageModel
        {
            return realmInstance.All<T>();
        }

        /// <summary>
        /// Remove item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Remove<T>(T item) where T : RealmObject, IStorageModel
        {
            using (Transaction trans = realmInstance.BeginWrite())
            {
                realmInstance.Remove(item);
                trans.Commit();
            }
        }

        /// <summary>
        /// Remove items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public void RemoveRange<T>(IQueryable<T> items) where T : RealmObject, IStorageModel
        {
            using (Transaction trans = realmInstance.BeginWrite())
            {
                realmInstance.RemoveRange(items);
                trans.Commit();
            }
        }
    }
}
