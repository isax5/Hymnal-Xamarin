using Hymnal.Core.Models;

namespace Hymnal.Core.Services
{
    public interface IAppInformationService
    {
        string Name { get; }
        string PackageName { get; }
        string VersionString { get; }
        string BuildString { get; }
        Theme RequestedTheme { get; }

        void ShowSettingsUI();
    }
}
