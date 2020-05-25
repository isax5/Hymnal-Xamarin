using System.Linq;

namespace Plugin.StorageManager
{
    public class BaseStorageManagerImplementation : IStorageManager
    {
        public void Add<T>(T item) where T : IStorageModel
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> All<T>() where T : IStorageModel, new()
        {
            throw new System.NotImplementedException();
        }

        public void Remove<T>(T item) where T : IStorageModel
        {
            throw new System.NotImplementedException();
        }

        public void RemoveRange<T>(IQueryable<T> items) where T : IStorageModel
        {
            throw new System.NotImplementedException();
        }
    }
}
