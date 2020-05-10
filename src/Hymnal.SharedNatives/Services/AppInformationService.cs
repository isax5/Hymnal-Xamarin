using Hymnal.Core.Models;
using Hymnal.Core.Services;
using Xamarin.Essentials;

namespace Hymnal.SharedNatives.Services
{
    public class AppInformationService : IAppInformationService
    {
        public string Name => AppInfo.Name;
        public string PackageName => AppInfo.PackageName;
        public string VersionString => AppInfo.VersionString;
        public string BuildString => AppInfo.BuildString;
        public Theme RequestedTheme
        {
            get
            {
                switch (AppInfo.RequestedTheme)
                {
                    case AppTheme.Light:
                        return Theme.Light;
                    case AppTheme.Dark:
                        return Theme.Dark;
                    default:
                        return Theme.Unspecified;
                }
            }
        }

        /// <summary>
        /// Open the settings menu or page settings for the application.
        /// </summary>
        public void ShowSettingsUI() => AppInfo.ShowSettingsUI();
    }
}
