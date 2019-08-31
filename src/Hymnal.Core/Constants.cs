using System.Collections.Generic;
using System.Globalization;
using Hymnal.Core.Models;

namespace Hymnal.Core
{
    public static class Constants
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
                Name = "New Version",
                Detail = "English",
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "NewHymnal.en.json",
                ThematicHymnsFileName = "NewHymnalThematicList.en.json"
            },
            new HymnalLanguage
            {
                Id = "en-oldVersion",
                Name = "Old Version",
                Detail = "English",
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "OldHymnal.en.json"
            },

            // SPANISH
            // default spanish version
            new HymnalLanguage
            {
                Id = "es-newVersion",
                Name = "Nueva Versión 2009",
                Detail = "Español",
                TwoLetterISOLanguageName = "es",
                HymnsFileName = "NewHymnal.es.json",
                ThematicHymnsFileName = "NewHymnalThematicList.es.json",
                SungMusic = "https://storage.googleapis.com/hymnals-music/es/sung/###.mp3",
                InstrumentalMusic = @"https://storage.googleapis.com/hymnals-music/es/instruments/###.mp3"
            },
            new HymnalLanguage
            {
                Id = "es-oldVersion",
                Name = "Antigua Versión",
                Detail = "Español",
                TwoLetterISOLanguageName = "es",
                HymnsFileName = "OldHymnal.es.json",
                ThematicHymnsFileName = "OldHymnalThematicList.es.json"
            },

            // RUSSIAN
            // default russian version
            new HymnalLanguage
            {
                Id = "ru-newVersion",
                Name = "Гимны Надежды",
                Detail = "Русский",
                TwoLetterISOLanguageName = "ru",
                HymnsFileName = "NewHymnal.ru.json"
            }
        };


        public const int MAXIMUM_RECORDS = 100;

        public const int DEFAULT_HYMNALS_FONT_SIZE = 18;
        public const int MINIMUM_HYMNALS_FONT_SIZE = 12;
        public const int MAXIMUM_HYMNALS_FONT_SIZE = 55;
    }
}
