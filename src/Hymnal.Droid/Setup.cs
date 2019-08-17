using Android.App;
using Hymnal.Core.Services;
using Hymnal.SharedNatives.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using Xamarin.Forms;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace Hymnal.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            // Native services register
            Mvx.IoCProvider.RegisterType<IFilesService, FilesService>();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }
    }
}
