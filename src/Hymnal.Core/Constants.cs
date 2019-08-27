using System.Collections.Generic;
using System.Globalization;
using Hymnal.Core.Models;

namespace Hymnal.Core
{
    public static class Constants
    {
        public static CultureInfo CurrentCultureInfo { get; set; }

        public static List<HymnalLanguage> HymnsLanguages = new List<HymnalLanguage>
        {
            // Default language version
            new HymnalLanguage
            {
                Name = "New Adventist Hymnal",
                Detail = "English",
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "hymnal.en.json"
            },
            new HymnalLanguage
            {
                Name = "Nuevo Himnario Adventista",
                Detail = "Español",
                TwoLetterISOLanguageName = "es",
                HymnsFileName = "hymnal.es.json",
                ThematicHymnsFileName = "thematicList.es.json"
            },
            new HymnalLanguage
            {
                Name = "Гимны Надежды",
                Detail = "Русский",
                TwoLetterISOLanguageName = "ru",
                HymnsFileName = "hymnal.ru.json"
            }
        };


        public const bool USING_SHEETS = false;

        public const int MAXIMUM_RECORDS = 100;

        public const int DEFAULT_HYMNALS_FONT_SIZE = 18;
        public const int MINIMUM_HYMNALS_FONT_SIZE = 12;
        public const int MAXIMUM_HYMNALS_FONT_SIZE = 55;
    }
}
