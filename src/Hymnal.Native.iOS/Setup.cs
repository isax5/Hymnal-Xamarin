using Hymnal.Core;
using Hymnal.Core.Services;
using Hymnal.iOS.Services;
using Hymnal.SharedNatives.Services;
using MediaManager;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using Plugin.StorageManager;
using Realms;

namespace Hymnal.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            // Native services register
            Mvx.IoCProvider.RegisterType<IFilesService, FilesService>();
            Mvx.IoCProvider.RegisterType<IDataStorageService, DataStorageService>();
            Mvx.IoCProvider.RegisterType<IDialogService, DialogService>();
            Mvx.IoCProvider.RegisterType<IMultilingualService, MultilingualService>();
        }

        public override void InitializePrimary()
        {
            base.InitializePrimary();
            CrossMediaManager.Current.Init();
            CrossStorageManager.Current.Init(Realm.GetInstance());
            ;
        }
    }
}
