using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Services;
using SQLite;

namespace Hymnal.SharedNatives.Services
{
    public class DataStorage : IDataStorage
    {
        private readonly IFilesService filesService;
        private static SQLiteAsyncConnection connection;

        public DataStorage(IFilesService filesService)
        {
            this.filesService = filesService;

            Init();
        }


        private void Init()
        {
            if (connection != null)
                return;

            // Connecto to DB file
            var DbPath = filesService.GetPathFile(Constants.DB_NAME);
            connection = new SQLiteAsyncConnection(DbPath);

            // Create tables
            connection.CreateTableAsync<FavoriteHymn>();
            connection.CreateTableAsync<HistoryHymn>();
        }


        public async Task<List<T>> TableAsync<T>() where T : new()
        {
            return await connection.Table<T>().ToListAsync();
        }
    }
}
