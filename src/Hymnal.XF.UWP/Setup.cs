using Hymnal.Core.Services;
using Hymnal.SharedNatives.Services;
using Hymnal.XF.UI.Services;
using MediaManager;
using MvvmCross;
using MvvmCross.Forms.Platforms.Uap.Core;

namespace Hymnal.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.App, XF.UI.App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            // Native services register
            Mvx.IoCProvider.RegisterType<IFilesService, FilesService>();
            Mvx.IoCProvider.RegisterType<IDataStorageService, DataStorageService>();
            Mvx.IoCProvider.RegisterType<IDialogService, DialogService>();
            Mvx.IoCProvider.RegisterType<IMultilingualService, MultilingualService>();
            Mvx.IoCProvider.RegisterType<IPreferencesService, PreferencesService>();
            Mvx.IoCProvider.RegisterType<IAppInformationService, AppInformationService>();
            Mvx.IoCProvider.RegisterType<IDeviceInformation, DeviceInformation>();
            Mvx.IoCProvider.RegisterType<IConnectivityService, ConnectivityService>();
            Mvx.IoCProvider.RegisterType<IBrowserService, BrowserService>();
            Mvx.IoCProvider.RegisterType<IShareService, ShareService>();
        }

        public override void InitializePrimary()
        {
            base.InitializePrimary();
            CrossMediaManager.Current.Init();
        }
    }
}
