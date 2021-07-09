using Hymnal.Core.Services;
using Hymnal.StorageModels;
using Hymnal.XF.iOS.Custom;
using Hymnal.XF.UI.Services;
using MediaManager;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using Plugin.StorageManager;
using Realms;
using Xamarin.Forms;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using MvvmCross.IoC;

namespace Hymnal.XF.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, UI.App>
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

        public override void InitializePrimary()
        {
            base.InitializePrimary();
            FormsMaterial.Init();
            CrossMediaManager.Current.Init();
            CrossStorageManager.Current.Init(Realm.GetInstance());
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
