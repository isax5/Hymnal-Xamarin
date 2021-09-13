using System;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Newtonsoft.Json;
using Xamarin.Essentials.Interfaces;

namespace Hymnal.XF.Services
{
    public sealed class PreferencesService : IPreferencesService
    {
        private readonly IPreferences preferences;
        public event EventHandler<HymnalLanguage> HymnalLanguageConfiguredChanged;

        public int HymnalsFontSize
        {
            get => preferences.Get(nameof(HymnalsFontSize), AppConstants.DEFAULT_HYMNALS_FONT_SIZE);
            set => preferences.Set(nameof(HymnalsFontSize), value);
        }

        public HymnalLanguage ConfiguredHymnalLanguage
        {
            get
            {
                var text = preferences.Get(nameof(ConfiguredHymnalLanguage), string.Empty);
                return string.IsNullOrWhiteSpace(text) ? null : JsonConvert.DeserializeObject<HymnalLanguage>(text).Configuration();
            }
            set
            {
                var text = JsonConvert.SerializeObject(value);
                preferences.Set(nameof(ConfiguredHymnalLanguage), text);

                if (HymnalLanguageConfiguredChanged != null)
                    HymnalLanguageConfiguredChanged.Invoke(this, value);
            }
        }

        public int OpeningCounter
        {
            get => preferences.Get(nameof(OpeningCounter), 0);
            set => preferences.Set(nameof(OpeningCounter), value);
        }

        public string LastVersionOpened
        {
            get => preferences.Get(nameof(LastVersionOpened), string.Empty);
            set => preferences.Set(nameof(LastVersionOpened), value);
        }

        public bool KeepScreenOn
        {
            get => preferences.Get(nameof(KeepScreenOn), false);
            set => preferences.Set(nameof(KeepScreenOn), value);
        }

        public bool BackgroundImageAppearance
        {
            get => preferences.Get(nameof(BackgroundImageAppearance), true);
            set => preferences.Set(nameof(BackgroundImageAppearance), value);
        }

        public PreferencesService(IPreferences preferences)
        {
            this.preferences = preferences;
        }
    }
}
