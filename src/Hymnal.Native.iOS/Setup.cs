using Hymnal.Core;
using Hymnal.Core.Services;
using Hymnal.Native.iOS.Services;
using MediaManager;
using MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using Plugin.StorageManager;
using Realms;

namespace Hymnal.Native.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            // Native services register
            Mvx.IoCProvider.RegisterType<IDialogService, DialogService>();
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
