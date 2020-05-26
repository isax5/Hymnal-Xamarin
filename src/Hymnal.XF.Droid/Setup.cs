using Android.App;
using Hymnal.Core.Services;
using Hymnal.SharedNatives.Services;
using Hymnal.XF.Droid.Custom;
using Hymnal.XF.UI.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using Xamarin.Forms;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace Hymnal.XF.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.App>
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

        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            //var formsPagePresenter = new MvxFormsPagePresenter(viewPresenter);
            var formsPagePresenter = new CustomFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
            return formsPagePresenter;
        }
    }
}
