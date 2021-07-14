using System;
using Hymnal.XF.Models;

namespace Hymnal.XF.Services
{
    public interface IPreferencesService
    {
        int HymnalsFontSize { get; set; }

        /// <summary>
        /// Language configurated in the app
        /// </summary>
        HymnalLanguage ConfiguratedHymnalLanguage { get; set; }

        /// <summary>
        /// The language configurated in the app has changed
        /// </summary>
        event EventHandler<HymnalLanguage> HymnalLanguageConfiguratedChanged;

        string LastVersionOpened { get; set; }

        bool KeepScreenOn { get; set; }
    }
}
