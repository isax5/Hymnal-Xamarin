using Android.App;
using Hymnal.Core.Services;
using Hymnal.XF.Droid.Custom;
using Hymnal.XF.UI.Services;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.IoC;
using Serilog;
using Serilog.Extensions.Logging;
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
        #region Logging
        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                // add more sinks here
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
        #endregion

        #region IoC
        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider) => base.InitializeFirstChance(iocProvider);

        protected override void RegisterDefaultSetupDependencies(IMvxIoCProvider iocProvider) => base.RegisterDefaultSetupDependencies(iocProvider);

        // For UI Services
        // https://www.mvvmcross.com/documentation/advanced/customizing-using-App-and-Setup#registering-platform-specific-business-objects-in-setupinitializefirstchance-and-setupinitializelastchance
        protected override void InitializeLastChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeLastChance(iocProvider);
            iocProvider.RegisterType<IDialogService, DialogService>();
        }
        #endregion

        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            var formsPagePresenter = new CustomFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
            return formsPagePresenter;
        }
    }
}
