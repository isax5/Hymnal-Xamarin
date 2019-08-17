using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Newtonsoft.Json;

namespace Hymnal.Core.Services
{
    public class HymnsService : IHymnsService
    {
        private readonly IFilesService filesService;

        /// <summary>
        /// <see cref="Hymn"/> cache
        /// </summary>
        private static IEnumerable<Hymn> Hymns;

        public HymnsService(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public async Task<IEnumerable<Hymn>> GetHymnsAsync()
        {
            if (Hymns == null || Hymns.Count() == 0)
            {
                var file = await filesService.ReadFileAsync(Constants.HYMNS_FILE_SPANISH);
                Hymns = JsonConvert.DeserializeObject<List<Hymn>>(file);
            }

            return Hymns;
        }

        public async Task<Hymn> GetHymnAsync(int number)
        {
            IEnumerable<Hymn> hymns = await GetHymnsAsync();

            return hymns.First(h => h.ID == number);
        }
    }
}
