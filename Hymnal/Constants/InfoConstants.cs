using System.Globalization;

namespace Hymnal.Constants;

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
                TwoLetterIsoLanguageName = "en",
                HymnsFileName = "new-hymnal-en.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-en.json",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_en_###",
            },
            new HymnalLanguage
            {
                Id = "en-oldVersion",
                Name = "Old Version 1941",
                Detail = "English",
                Year = 1941,
                TwoLetterIsoLanguageName = "en",
                HymnsFileName = "old-hymnal-en.json",
                ThematicHymnsFileName = "old-hymnal-thematic-list-en.json",
            },

            // SPANISH
            // default spanish version
            new HymnalLanguage
            {
                Id = "es-newVersion",
                Name = "Nueva Versión 2009",
                Detail = "Español",
                Year = 2009,
                TwoLetterIsoLanguageName = "es",
                HymnsFileName = "new-hymnal-es.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-es.json",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_es_###"
            },
            new HymnalLanguage
            {
                Id = "es-oldVersion",
                Name = "Antigua Versión 1962",
                Detail = "Español",
                Year = 1962,
                TwoLetterIsoLanguageName = "es",
                HymnsFileName = "old-hymnal-es.json",
                ThematicHymnsFileName = "old-hymnal-thematic-list-es.json",
            },

            // PORTUGUESE
            // default portuguese version
            new HymnalLanguage
            {
                Id = "pt-newVersion",
                Name = "Nova versão 1996",
                Detail = "Português",
                Year = 1996,
                TwoLetterIsoLanguageName = "pt",
                HymnsFileName = "new-hymnal-pt.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-pt.json",
            },

            // RUSSIAN
            // default russian version
            new HymnalLanguage
            {
                Id = "ru-newVersion",
                Name = "Гимны Надежды 1997",
                Detail = "Русский",
                Year = 1997,
                TwoLetterIsoLanguageName = "ru",
                HymnsFileName = "new-hymnal-ru.json",
                ThematicHymnsFileName = "new-hymnal-thematic-list-ru.json",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_ru_###",
            }
        };
}
