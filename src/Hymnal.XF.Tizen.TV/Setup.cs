using Hymnal.Core.Services;
using Hymnal.StorageModels;
using Hymnal.XF.Tizen.TV.Custom;
using Hymnal.XF.UI.Services;
using MediaManager;
using MvvmCross;
using MvvmCross.Forms.Platforms.Tizen.Core;
using MvvmCross.Forms.Presenters;
using Plugin.StorageManager;

namespace Hymnal.XF.Tizen.TV
{
    public class Setup : MvxFormsTizenSetup<Core.App, UI.App>
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
            CrossStorageManager.Current.Init();
            CrossMediaManager.Current.Init();
            Storage.Init();
        }

        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            var formsPagePresenter = new CustomFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
            return formsPagePresenter;
        }
    }
}
