using System.Linq.Expressions;
using Hymnal.Models.DataBase;
using SQLite;

namespace Hymnal.Services
{
    public sealed class DatabaseService
    {
        private readonly SQLiteAsyncConnection databaseConnection;

        public DatabaseService()
        {
            databaseConnection = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "hymn.db3"));
            databaseConnection.CreateTableAsync<FavoriteHymn>();
        }

        public Task<T> FindAsync<T>(Expression<Func<T, bool>> predicate) where T : StorageModel, new()
        {
            return databaseConnection.FindAsync<T>(predicate);
        }

        public AsyncTableQuery<T> GetTable<T>() where T : StorageModel, new()
        {
            return databaseConnection.Table<T>();
        }

        public Task<int> InserAsync<T>(T item) where T : StorageModel, new()
        {
            return databaseConnection.InsertAsync(item);
        }
    }
}


