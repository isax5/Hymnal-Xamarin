using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Realm;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;

namespace Hymnal.XF.Services
{
    public class HymnsService : IHymnsService
    {
        private readonly IFilesService filesService;

        /// <summary>
        /// <see cref="Hymn"/> cache
        /// </summary>
        private static readonly Dictionary<string, IEnumerable<Hymn>> HymnsDictionary = new();

        /// <summary>
        /// <see cref="Thematic"/> cache
        /// </summary>
        private static readonly Dictionary<string, IEnumerable<Thematic>> ThematicDictionary = new();

        public HymnsService(
            IFilesService filesService
            )
        {
            this.filesService = filesService;
        }

        /// <summary>
        /// Get list of hymns
        /// Here is where you read all the hymns
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Hymn>> GetHymnListAsync(HymnalLanguage language)
        {
            if (!HymnsDictionary.ContainsKey(language.Id))
            {
                try
                {
                    var file = await filesService.ReadFileAsync(language.HymnsFileName).ConfigureAwait(false);
                    List<Hymn> hymns = JsonConvert.DeserializeObject<List<Hymn>>(file);

                    // Set Id of the language to know allways where it is from
                    hymns.ForEach(h => h.HymnalLanguageId = language.Id);

                    lock (HymnsDictionary)
                    {
                        if (!HymnsDictionary.ContainsKey(language.Id))
                            HymnsDictionary.Add(language.Id, hymns);
                    }
                }
                catch (Exception ex)
                {
                    ex.Report();
                    //var properties = new Dictionary<string, string>()
                    //{
                    //    { "File", nameof(HymnsService) },
                    //    { "Hymnal Version", language.Id }
                    //};

                    //Debug.WriteLine("Exception reading hymnbook", ex, properties);
                    //Crashes.TrackError(ex, properties);

                    return new List<Hymn>();
                }
            }

            return HymnsDictionary[language.Id];
        }

        public async Task<Hymn> GetHymnAsync(int number, HymnalLanguage language)
        {
            IEnumerable<Hymn> hymns = await GetHymnListAsync(language).ConfigureAwait(false);

            return hymns.First(h => h.Number == number);
        }

        /// <summary>
        /// Get Hymn from a storage reference of a hymn
        /// </summary>
        /// <param name="hymnReference"></param>
        /// <returns></returns>
        public Task<Hymn> GetHymnAsync(IHymnReference hymnReference)
        {
            return GetHymnAsync(hymnReference.Number, HymnalLanguage.GetHymnalLanguageWithId(hymnReference.HymnalLanguageId));
        }

        public async Task<IEnumerable<Thematic>> GetThematicListAsync(HymnalLanguage language)
        {
            if (!language.SupportThematicList)
            {
                return new List<Thematic>();
            }

            if (!ThematicDictionary.ContainsKey(language.Id))
            {
                var file = await filesService.ReadFileAsync(language.ThematicHymnsFileName).ConfigureAwait(false);
                List<Thematic> thematicList = JsonConvert.DeserializeObject<List<Thematic>>(file);

                lock (ThematicDictionary)
                {
                    if (!ThematicDictionary.ContainsKey(language.Id))
                        ThematicDictionary.Add(language.Id, thematicList);
                }
            }

            return ThematicDictionary[language.Id];
        }
    }
}
