using Hymnal.Core.Services;
using Hymnal.SharedNatives.Services;
using Hymnal.XF.UI.Services;
using MediaManager;
using MvvmCross;
using MvvmCross.Forms.Platforms.Tizen.Core;
using Plugin.StorageManager;

namespace Hymnal.XF.Tizen.TV
{
    public class Setup : MvxFormsTizenSetup<Core.App, UI.App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            // Native services register
            Mvx.IoCProvider.RegisterType<IFilesService, FilesService>();
            Mvx.IoCProvider.RegisterType<IDataStorageService, DataStorageService>();
            Mvx.IoCProvider.RegisterType<IDialogService, DialogService>();
            Mvx.IoCProvider.RegisterType<IMultilingualService, Tv.Services.MultilingualService>();
            Mvx.IoCProvider.RegisterType<IPreferencesService, PreferencesService>();
        }

        public override void InitializePrimary()
        {
            base.InitializePrimary();
            CrossStorageManager.Current.Init();
            CrossMediaManager.Current.Init();
        }
    }
}
