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
                InstrumentalMusicUrl = @"https://s3.us-east-2.wasabisys.com/hymnalstorage/english/1985%20version/instrumental/###.mp3",
            },
            new HymnSettingsResponse
            {
                Id = "en-oldVersion",
                TwoLetterISOLanguageName = "en",
                InstrumentalMusicUrl = @"https://s3.us-east-2.wasabisys.com/hymnalstorage/english/1941%20version/instrumental/###.mp3"
            },

            // SPANISH
            // default spanish version
            new HymnSettingsResponse
            {
                Id = "es-newVersion",
                TwoLetterISOLanguageName = "es",
                InstrumentalMusicUrl = @"https://s3.us-east-2.wasabisys.com/hymnalstorage/spanish/2009%20version/instrumental/###.mp3",
                SungMusicUrl = @"https://s3.us-east-2.wasabisys.com/hymnalstorage/spanish/2009%20version/sung/###.mp3",
            },
            new HymnSettingsResponse
            {
                Id = "es-oldVersion",
                TwoLetterISOLanguageName = "es",
                InstrumentalMusicUrl = @"https://s3.us-east-2.wasabisys.com/hymnalstorage/spanish/1962%20version/instrumental/###.mp3"
            },

            // PORTUGUESE
            // default portuguese version
            new HymnSettingsResponse
            {
                Id = "pt-newVersion",
                TwoLetterISOLanguageName = "pt",
                SungMusicUrl = @"https://s3.us-east-2.wasabisys.com/hymnalstorage/portuguese/1996%20version/sung/###.mp3"
            },

            // RUSSIAN
            // default russian version
            new HymnSettingsResponse
            {
                Id = "ru-newVersion",
                TwoLetterISOLanguageName = "ru",
                InstrumentalMusicUrl = @"https://s3.us-east-2.wasabisys.com/hymnalstorage/russian/1997%20version/instrumental/###.mp3"
            }
        };
    }
}
