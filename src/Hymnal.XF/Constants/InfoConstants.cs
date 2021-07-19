using System.Collections.Generic;
using System.Globalization;
using Hymnal.XF.Models;

namespace Hymnal.XF.Constants
{
    public static class InfoConstants
    {
        public static CultureInfo CurrentCultureInfo { get; set; }

        /// <summary>
        /// Configuration items
        /// </summary>
        public static List<HymnalLanguage> HymnsLanguages = new List<HymnalLanguage>
        {
            // ENGLISH
            // Default language version - default english version
            new HymnalLanguage
            {
                Id = "en-newVersion",
                Name = "New Version 1985",
                Detail = "English",
                Year = 1985,
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "new-hymnal-en.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-en.json",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/english/1985%20version/instrumental/###.mp3",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_en_###"
            },
            new HymnalLanguage
            {
                Id = "en-oldVersion",
                Name = "Old Version 1941",
                Detail = "English",
                Year = 1941,
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "old-hymnal-en.json",
                ThematicHymnsFileName = "old-hymnal-thematic-list-en.json",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/english/1941%20version/instrumental/###.mp3"
            },

            // SPANISH
            // default spanish version
            new HymnalLanguage
            {
                Id = "es-newVersion",
                Name = "Nueva Versión 2009",
                Detail = "Español",
                Year = 2009,
                TwoLetterISOLanguageName = "es",
                HymnsFileName = "new-hymnal-es.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-es.json",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/instrumental/###.mp3",
                SungMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/sung/###.mp3",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_es_###"
            },
            new HymnalLanguage
            {
                Id = "es-oldVersion",
                Name = "Antigua Versión 1962",
                Detail = "Español",
                Year = 1962,
                TwoLetterISOLanguageName = "es",
                HymnsFileName = "old-hymnal-es.json",
                ThematicHymnsFileName = "old-hymnal-thematic-list-es.json",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/1962%20version/instrumental/###.mp3"
            },

            // PORTUGUESE
            // default portuguese version
            new HymnalLanguage
            {
                Id = "pt-newVersion",
                Name = "Nova versão 1996",
                Detail = "Português",
                Year = 1996,
                TwoLetterISOLanguageName = "pt",
                HymnsFileName = "new-hymnal-pt.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-pt.json",
                SungMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/portuguese/1996%20version/sung/###.mp3"
            },

            // RUSSIAN
            // default russian version
            new HymnalLanguage
            {
                Id = "ru-newVersion",
                Name = "Гимны Надежды 1997",
                Detail = "Русский",
                Year = 1997,
                TwoLetterISOLanguageName = "ru",
                HymnsFileName = "new-hymnal-ru.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-ru.json",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_ru_###",
                InstrumentalMusic = @"https://hymnalstorage.blob.core.windows.net/hymn-music/russian/1997%20version/instrumental/###.mp3"
            }
        };
    }
}
