using Hymnal.Core.Models;
using Hymnal.Core.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Hymnal.SharedNatives.Services
{
    public class PreferencesService : IPreferencesService

    {
        public int HymnalsFontSize
        {
            get => Preferences.Get(nameof(HymnalsFontSize), Core.Constants.DEFAULT_HYMNALS_FONT_SIZE);
            set => Preferences.Set(nameof(HymnalsFontSize), value);
        }

        public HymnalLanguage ConfiguratedHymnalLanguage
        {
            get
            {
                var text = Preferences.Get(nameof(ConfiguratedHymnalLanguage), string.Empty);
                return string.IsNullOrWhiteSpace(text) ? null : JsonConvert.DeserializeObject<HymnalLanguage>(text);
            }
            set
            {
                var text = JsonConvert.SerializeObject(value);
                Preferences.Set(nameof(ConfiguratedHymnalLanguage), text);
            }
        }

        public bool FirstTimeOpening
        {
            get => Preferences.Get(nameof(FirstTimeOpening), true);
            set => Preferences.Set(nameof(FirstTimeOpening), value);
        }
    }
}
