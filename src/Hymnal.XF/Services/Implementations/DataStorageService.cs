using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Essentials.Interfaces;

namespace Hymnal.XF.Services
{
    public sealed class DataStorageService : IDataStorageService
    {
        private readonly IPreferences preferences;

        private string identifier<T>() => nameof(T) + typeof(T);


        public DataStorageService(IPreferences preferences)
        {
            this.preferences = preferences;
        }

        public List<T> GetItems<T>()
        {
            var text = preferences.Get(identifier<T>(), string.Empty);
            return string.IsNullOrWhiteSpace(text) ? new List<T>() : JsonConvert.DeserializeObject<List<T>>(text);
        }

        public void SetItems<T>(List<T> items)
        {
            var text = JsonConvert.SerializeObject(items);
            preferences.Set(identifier<T>(), text);
        }

        public void DeleteItems<T>(List<T> items)
        {
            preferences.Remove(identifier<T>());
        }
    }
}
