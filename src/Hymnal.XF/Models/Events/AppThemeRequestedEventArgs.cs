using Xamarin.Forms;

namespace Hymnal.XF.Models.Events
{
    public sealed class AppThemeRequestedEventArgs
    {
        public OSAppTheme RequestedTheme { get; }
        public ResourceDictionary ThemeResources { get; }

        public AppThemeRequestedEventArgs(OSAppTheme requestedTheme, ResourceDictionary themeResources)
        {
            RequestedTheme = requestedTheme;
            ThemeResources = themeResources;
        }
    }
}
