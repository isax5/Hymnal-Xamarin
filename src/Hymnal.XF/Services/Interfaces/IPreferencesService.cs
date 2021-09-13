using System;
using Hymnal.XF.Models;

namespace Hymnal.XF.Services
{
    public interface IPreferencesService
    {
        int HymnalsFontSize { get; set; }

        /// <summary>
        /// Language configured in the app
        /// </summary>
        HymnalLanguage ConfiguredHymnalLanguage { get; set; }

        /// <summary>
        /// The language configured in the app has changed
        /// </summary>
        event EventHandler<HymnalLanguage> HymnalLanguageConfiguredChanged;

        string LastVersionOpened { get; set; }

        int OpeningCounter { get; set; }

        bool KeepScreenOn { get; set; }

        bool BackgroundImageAppearance { get; set; }
    }
}
