using Hymnal.Core.Services;
using Hymnal.XF.iOS.Custom;
using Hymnal.XF.UI.Services;
using MediaManager;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using Plugin.StorageManager;
using Realms;
using Xamarin.Forms;

namespace Hymnal.XF.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, UI.App>
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
            Device.SetFlags(new string[] { "CarouselView_Experimental" });
            FormsMaterial.Init();
            CrossMediaManager.Current.Init();
            CrossStorageManager.Current.Init(Realm.GetInstance());
        }

        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            var formsPagePresenter = new CustomFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
            return formsPagePresenter;
        }
    }
}
