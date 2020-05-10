using Android.App;
using Hymnal.Core.Services;
using Hymnal.SharedNatives.Services;
using Hymnal.XF.UI.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using Xamarin.Forms;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace Hymnal.XF.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, XF.UI.App>
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
            Mvx.IoCProvider.RegisterType<IMediaService, MediaService>();
            Mvx.IoCProvider.RegisterType<IAppInformationService, AppInformationService>();
            Mvx.IoCProvider.RegisterType<IDeviceInformation, DeviceInformation>();
            Mvx.IoCProvider.RegisterType<IConnectivityService, ConnectivityService>();
            Mvx.IoCProvider.RegisterType<IBrowserService, BrowserService>();
            Mvx.IoCProvider.RegisterType<IShareService, ShareService>();
        }
    }
}
