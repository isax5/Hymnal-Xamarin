using System;
using Hymnal.Core.Extensions;
using Hymnal.Core.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Hymnal.Core.Services
{
    public class PreferencesService : IPreferencesService

    {
        public int HymnalsFontSize
        {
            get => Preferences.Get(nameof(HymnalsFontSize), Constants.DEFAULT_HYMNALS_FONT_SIZE);
            set => Preferences.Set(nameof(HymnalsFontSize), value);
        }

        public HymnalLanguage ConfiguratedHymnalLanguage
        {
            get
            {
                var text = Preferences.Get(nameof(ConfiguratedHymnalLanguage), string.Empty);
                return string.IsNullOrWhiteSpace(text) ? null : JsonConvert.DeserializeObject<HymnalLanguage>(text).Configuration();
            }
            set
            {
                var text = JsonConvert.SerializeObject(value);
                Preferences.Set(nameof(ConfiguratedHymnalLanguage), text);

                if (hymnalLanguageConfiguratedChanged != null)
                    hymnalLanguageConfiguratedChanged.Invoke(this, value);
            }
        }

        private static EventHandler<HymnalLanguage> hymnalLanguageConfiguratedChanged;
        public event EventHandler<HymnalLanguage> HymnalLanguageConfiguratedChanged
        {
            add => hymnalLanguageConfiguratedChanged += value;
            remove => hymnalLanguageConfiguratedChanged -= value;
        }

        public string LastVersionOpened
        {
            get => Preferences.Get(nameof(LastVersionOpened), string.Empty);
            set => Preferences.Set(nameof(LastVersionOpened), value);
        }
    }
}
