using System.Collections.Generic;
using System.Globalization;
using Hymnal.Core.Models;
using Hymnal.Core.Services;
using System;

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
                Name = "New Version 1985",
                Detail = "English",
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "NewHymnal.en.json",
                ThematicHymnsFileName = "NewHymnalThematicList.en.json",
                InstrumentalMusic = @"https://storage.googleapis.com/hymn-music/english/1985%20version/instrumental/###.mp3"
            },
            new HymnalLanguage
            {
                Id = "en-oldVersion",
                Name = "Old Version 1941",
                Detail = "English",
                TwoLetterISOLanguageName = "en",
                HymnsFileName = "OldHymnal.en.json",
                ThematicHymnsFileName = "OldHymnalThematicList.en.json",
                InstrumentalMusic = @"https://storage.googleapis.com/hymn-music/english/1941%20version/instrumental/###.mp3"
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
                InstrumentalMusic = @"https://storage.googleapis.com/hymn-music/spanish/2009%20version/instrumental/###.mp3",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_es_###"
            },
            new HymnalLanguage
            {
                Id = "es-oldVersion",
                Name = "Antigua Versión 1962",
                Detail = "Español",
                TwoLetterISOLanguageName = "es",
                HymnsFileName = "OldHymnal.es.json",
                ThematicHymnsFileName = "OldHymnalThematicList.es.json",
                InstrumentalMusic = @"https://storage.googleapis.com/hymn-music/spanish/1962%20version/instrumental/###.mp3"
            },

            // PORTUGUESE
            // default portuguese version
            new HymnalLanguage
            {
                Id = "pt-newVersion",
                Name = "Nova versão 1996",
                Detail = "Português",
                TwoLetterISOLanguageName = "pt",
                HymnsFileName = "NewHymnal.pt.json",
                ThematicHymnsFileName = "NewHymnalThematicList.pt.json",
                SungMusic = @"https://storage.googleapis.com/hymn-music/portuguese/1996%20version/sung/###.mp3"
            },

            // RUSSIAN
            // default russian version
            new HymnalLanguage
            {
                Id = "ru-newVersion",
                Name = "Гимны Надежды 1997",
                Detail = "Русский",
                TwoLetterISOLanguageName = "ru",
                HymnsFileName = "NewHymnal.ru.json",
                ThematicHymnsFileName = "NewHymnalThematicList.ru.json",
                HymnsSheetsFileName = "PianoSheet_NewHymnal_ru_###",
                InstrumentalMusic = @"https://storage.googleapis.com/hymn-music/russian/1997%20version/instrumental/###.mp3"
            }
        };

        public struct WebLinks
        {
            public const string DeveloperWebSite = @"https://storage.googleapis.com/hymn-music/about/index.html";

            /// <summary>
            /// AppStore download Link
            /// </summary>
            public const string AppDownloadLinkIOS = @"https://apps.apple.com/cl/app/adventist-hymnal/id1153114394";

            /// <summary>
            /// PlayStore Download Link
            /// </summary>
            public const string AppDownloadLinkAndroid = @"https://play.google.com/store/apps/details?id=net.ddns.HimnarioAdventistaSPA";
        }

        /// <summary>
        /// App link scheme
        /// </summary>
        public struct AppLink
        {
            public const string Scheme = "adv";
            public const string Host = "hymnal";
            public const string UriBase = "adv://hymnal/";
        }

        /// <summary>
        /// Event key & scheme for AppCenter
        /// </summary>
        public struct TrackEvents
        {
            /// <summary>
            /// App started
            /// Lenguage <see cref="CultureInfo"/> / Version of the <see cref="HymnalLanguage"/> configurated / Dark or Light Theme
            /// </summary>
            public const string AppOpened = "App Opened";
            public struct AppOpenedScheme
            {
                /// <summary>
                /// Use <see cref="CultureInfo.Name"/>
                /// </summary>
                public const string CultureInfo = "Culture Information";

                /// <summary>
                /// Use <see cref="HymnalLanguage.Id"/>
                /// </summary>
                public const string HymnalVersion = "Hymnal Version";

                /// <summary>
                /// Use <see cref="Theme"/> <see cref="IAppInformationService"/>
                /// </summary>
                public const string ThemeConfigurated = "Theme Configurated";
            }

            /// <summary>
            /// Tabs, Search, History, Hymnal Opend from (Index [Numeric, alphabetic, thematic], number, favorites, history)
            /// </summary>
            public const string Navigation = "Navigation";
            public struct NavigationReferenceScheme
            {
                /// <summary>
                /// Use <see cref="nameof(Page)"/>
                /// </summary>
                public const string PageName = "Page Name";

                /// <summary>
                /// Use <see cref="CultureInfo.Name"/>
                /// </summary>
                public const string CultureInfo = "Culture Information";

                /// <summary>
                /// Use <see cref="HymnalLanguage.Id"/>
                /// </summary>
                public const string HymnalVersion = "Hymnal Version";
            }

            /// <summary>
            /// Hymn reference for event tracking
            /// </summary>
            public struct HymnReferenceScheme
            {
                public const string Number = "Number";

                /// <summary>
                /// Use <see cref="HymnalLanguage.Id"/>
                /// </summary>
                public const string HymnalVersion = "Hymnal Version";

                /// <summary>
                /// Use <see cref="CultureInfo.Name"/>
                /// </summary>
                public const string CultureInfo = "Culture Information";

                /// <summary>
                /// Use <see cref="DateTime.Now.ToLocalTime()"/>
                /// </summary>
                public const string Time = "Time";
            }

            /// <summary>
            /// Hymn opened
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnOpened = "Hymn Opened";

            /// <summary>
            /// Hymn Music Sheet Opened
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnMusicSheetOpened = "Hymn Music Sheet Opened";

            /// <summary>
            /// Hymn added to favorites
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnAddedToFavorites = "Hymn Added To Favorites";

            /// <summary>
            /// Hymn remove favorites
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnRemoveFromFavorites = "Hymn Remove From Favorites";

            /// <summary>
            /// Music played
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnMusicPlayed = "Music Played";

            /// <summary>
            /// Hymn shared
            /// Use <see cref="HymnReferenceScheme"/>
            /// </summary>
            public const string HymnShared = "Hymn Shared";
        }

        public const int MAXIMUM_RECORDS = 100;

        public const int DEFAULT_HYMNALS_FONT_SIZE = 18;
        public const int MINIMUM_HYMNALS_FONT_SIZE = 12;
        public const int MAXIMUM_HYMNALS_FONT_SIZE = 55;
    }
}
