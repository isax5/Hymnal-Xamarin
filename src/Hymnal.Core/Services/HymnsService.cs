using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Extensions;
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
        private static Dictionary<string, IEnumerable<Hymn>> HymnsDictionary = new Dictionary<string, IEnumerable<Hymn>>();

        /// <summary>
        /// <see cref="Thematic"/> cache
        /// </summary>
        private static Dictionary<string, IEnumerable<Thematic>> ThematicDictionary = new Dictionary<string, IEnumerable<Thematic>>();

        public HymnsService(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public async Task<IEnumerable<Hymn>> GetHymnListAsync(HymnalLanguage language)
        {
            if (!HymnsDictionary.ContainsKey(language.Id))
            {
                var file = await filesService.ReadFileAsync(language.Configuration().HymnsFileName);
                List<Hymn> hymns = JsonConvert.DeserializeObject<List<Hymn>>(file);
                HymnsDictionary.Add(language.Id, hymns);
            }

            return HymnsDictionary[language.Id];
        }

        public async Task<Hymn> GetHymnAsync(int number, HymnalLanguage language)
        {
            IEnumerable<Hymn> hymns = await GetHymnListAsync(language);

            return hymns.First(h => h.Number == number);
        }

        public async Task<IEnumerable<Thematic>> GetThematicListAsync(HymnalLanguage language)
        {
            if (!language.SupportThematicList)
            {
                return new List<Thematic>();
            }

            if (!ThematicDictionary.ContainsKey(language.Id))
            {
                var file = await filesService.ReadFileAsync(language.Configuration().ThematicHymnsFileName);
                List<Thematic> thematicList = JsonConvert.DeserializeObject<List<Thematic>>(file);
                ThematicDictionary.Add(language.Id, thematicList);
            }

            return ThematicDictionary[language.Id];
        }
    }
}
