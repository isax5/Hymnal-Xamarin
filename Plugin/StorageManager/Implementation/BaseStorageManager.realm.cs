using System.Linq;
using Realms;
using Xamarin.Essentials;

namespace Plugin.StorageManager
{
    public class BaseStorageManagerImplementation : IStorageManager
    {
        public Realm RealmInstance;


        public void Add<T>(T item) where T : RealmObject, IStorageModel
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                RealmInstance.Write(() => RealmInstance.Add(item));
            });
        }

        public IQueryable<T> All<T>() where T : RealmObject, IStorageModel
        {
            IQueryable<T> data = null;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                data = RealmInstance.All<T>();
            });

            return data;
        }

        public void Remove<T>(T item) where T : RealmObject, IStorageModel
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                using (Transaction trans = RealmInstance.BeginWrite())
                {
                    RealmInstance.Remove(item);
                    trans.Commit();
                }
            });
        }

        public void RemoveRange<T>(IQueryable<T> items) where T : RealmObject, IStorageModel
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                using (Transaction trans = RealmInstance.BeginWrite())
                {
                    RealmInstance.RemoveRange(items);
                    trans.Commit();
                }
            });
        }
    }
}
