using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Plugin.StorageManager
{
    /// <summary>
    /// DB implementation using native Preferences
    /// </summary>
    public class BaseStorageManagerImplementation : IStorageManager
    {
        public void Add<T>(T item) where T : IStorageModel
        {
            List<T> items = GetItems<T>();
            items.Add(item);
            ReplaceItems(items);
        }

        public IQueryable<T> All<T>() where T : IStorageModel
        {
            return GetItems<T>().AsQueryable();
        }

        public void Remove<T>(T item) where T : IStorageModel
        {
            var items = GetItems<T>();
            items.Remove(item);
            ReplaceItems(items);
        }

        public void RemoveRange<T>(IQueryable<T> items) where T : IStorageModel
        {
            var originalItems = GetItems<T>();
            foreach (var item in items)
            {
                originalItems.Remove(item);
            }

            ReplaceItems(originalItems);
        }

        #region Implementation details
        private string identifier<T>() => nameof(T) + typeof(T);

        private List<T> GetItems<T>()
        {
            var text = Preferences.Get(identifier<T>(), string.Empty);
            return string.IsNullOrWhiteSpace(text) ? new List<T>() : JsonConvert.DeserializeObject<List<T>>(text);
        }

        private void ReplaceItems<T>(List<T> items)
        {
            var text = JsonConvert.SerializeObject(items);
            Preferences.Set(identifier<T>(), text);
        }

        private void DeleteItems<T>(List<T> items)
        {
            Preferences.Remove(identifier<T>());
        }
        #endregion
    }
}
