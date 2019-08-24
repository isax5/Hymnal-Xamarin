using Hymnal.Core.Services;
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
    }
}
