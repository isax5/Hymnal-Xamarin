using Hymnal.Core;
using Hymnal.Core.Services;
using Hymnal.Native.TvOS.Services;
using MediaManager;
using MvvmCross;
using MvvmCross.Platforms.Tvos.Core;
using Plugin.StorageManager;

namespace Hymnal.Native.TvOS
{
    public class Setup : MvxTvosSetup<App>
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
            CrossStorageManager.Current.Init();
        }
    }
}
