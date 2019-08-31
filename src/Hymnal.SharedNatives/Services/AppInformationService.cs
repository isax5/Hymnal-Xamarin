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

        /// <summary>
        /// Open the settings menu or page for the application.
        /// </summary>
        public void ShowSettingsUI() => AppInfo.ShowSettingsUI();
    }
}
