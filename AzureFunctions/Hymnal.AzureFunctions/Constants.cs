using System.Collections.Generic;
using Hymnal.AzureFunctions.Models;

namespace Hymnal.AzureFunctions
{
    public static class Constants
    {
        public static List<HymnSettingsResponse> HYMN_SETTINGS = new List<HymnSettingsResponse>
        {
            // ENGLISH
            // Default language version - default english version
            new HymnSettingsResponse
            {
                Id = "en-newVersion",
                TwoLetterISOLanguageName = "en",
                InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/english/1985%20version/instrumental/###.mp3",
            },
            new HymnSettingsResponse
            {
                Id = "en-oldVersion",
                TwoLetterISOLanguageName = "en",
                InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/english/1941%20version/instrumental/###.mp3"
            },

            // SPANISH
            // default spanish version
            new HymnSettingsResponse
            {
                Id = "es-newVersion",
                TwoLetterISOLanguageName = "es",
                InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/instrumental/###.mp3",
                SungMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/sung/###.mp3",
            },
            new HymnSettingsResponse
            {
                Id = "es-oldVersion",
                TwoLetterISOLanguageName = "es",
                InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/1962%20version/instrumental/###.mp3"
            },

            // PORTUGUESE
            // default portuguese version
            new HymnSettingsResponse
            {
                Id = "pt-newVersion",
                TwoLetterISOLanguageName = "pt",
                SungMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/portuguese/1996%20version/sung/###.mp3"
            },

            // RUSSIAN
            // default russian version
            new HymnSettingsResponse
            {
                Id = "ru-newVersion",
                TwoLetterISOLanguageName = "ru",
                InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/russian/1997%20version/instrumental/###.mp3"
            }
        };
    }
}
