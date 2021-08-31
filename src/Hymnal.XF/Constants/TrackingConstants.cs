using System;
using System.Globalization;
using Hymnal.XF.Models;

namespace Hymnal.XF.Constants
{
    public static class TrackingConstants
    {
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
        /// Time, Time Zone, iOS Version, App Version, App Build, Account Id (not using yet), AppNamespace, Device model, Country code, etc.
        /// https://docs.microsoft.com/en-us/appcenter/analytics/export#azure-blob-storage
        /// </summary>
        public struct TrackEv
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

                /// <summary>
                /// Use <see cref="InstrumentalMusic"/> and <see cref="SungMusic"/>
                /// </summary>
                public const string TypeOfMusicPlaying = "Type of Music Playing";

                /// <summary>
                /// Instrumental for: <see cref="TypeOfMusicPlaying"/>
                /// </summary>
                public const string InstrumentalMusic = "Instrumental";

                /// <summary>
                /// Sung for: <see cref="TypeOfMusicPlaying"/>
                /// </summary>
                public const string SungMusic = "Sung";
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

            /// <summary>
            /// Hymn founded through <see cref="ViewModels.SearchViewModel"/>
            /// </summary>
            public const string HymnFounded = "Hymn Founded";

            /// <summary>
            /// Hymn founded through <see cref="ViewModels.SearchViewModel"/>
            /// </summary>
            public struct HymnFoundedScheme
            {
                /// <summary>
                /// Query in the search field
                /// </summary>
                public const string Query = "Query";

                /// <summary>
                /// Number of the hymn founded
                /// </summary>
                public const string HymnFounded = "Hymn Founded";

                /// <summary>
                /// Use <see cref="HymnalLanguage.Id"/>
                /// </summary>
                public const string HymnalVersion = "Hymnal Version";

            }
        }
    }
}
