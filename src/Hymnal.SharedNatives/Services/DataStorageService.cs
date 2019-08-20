using System.Collections.Generic;
using Hymnal.Core.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Hymnal.SharedNatives.Services
{
    public class DataStorageService : IDataStorageService
    {
        private readonly IFilesService filesService;

        public DataStorageService(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        private string identifier<T>() => nameof(T) + typeof(T);

        public List<T> GetItems<T>()
        {
            var text = Preferences.Get(identifier<T>(), string.Empty);

            return string.IsNullOrWhiteSpace(text) ? new List<T>() : JsonConvert.DeserializeObject<List<T>>(text);
        }

        public void ReplaceItems<T>(List<T> items)
        {
            var text = JsonConvert.SerializeObject(items);
            Preferences.Set(identifier<T>(), text);
        }

        public void DeleteItems<T>(List<T> items)
        {
            Preferences.Remove(identifier<T>());
        }

    }
}
