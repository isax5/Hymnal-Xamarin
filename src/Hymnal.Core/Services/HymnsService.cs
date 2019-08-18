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
        private static IEnumerable<Hymn> hymnList;

        /// <summary>
        /// Thematic cache
        /// </summary>
        private static IEnumerable<Thematic> thematicList;

        public HymnsService(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public async Task<IEnumerable<Hymn>> GetHymnListAsync()
        {
            if (hymnList == null || hymnList.Count() == 0)
            {
                var file = await filesService.ReadFileAsync(Constants.HYMNS_FILE_SPANISH);
                hymnList = JsonConvert.DeserializeObject<List<Hymn>>(file);
            }

            return hymnList;
        }

        public async Task<Hymn> GetHymnAsync(int number)
        {
            IEnumerable<Hymn> hymns = await GetHymnListAsync();

            return hymns.First(h => h.ID == number);
        }

        public async Task<IEnumerable<Thematic>> GetThematicListAsync()
        {
            if (thematicList == null || thematicList.Count() == 0)
            {
                var file = await filesService.ReadFileAsync(Constants.THEMATIC_LIST_FILE_SPANISH);
                thematicList = JsonConvert.DeserializeObject<List<Thematic>>(file);
            }

            return thematicList;
        }
    }
}
