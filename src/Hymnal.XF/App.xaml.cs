using System.Globalization;
using Hymnal.XF.Extensions.i18n;
using Hymnal.XF.Services;
using Hymnal.XF.ViewModels;
using Hymnal.XF.Views;
using Hymnal.XF.Views.Custom;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        // private AppThemeResources _appThemeResources;

        public App(IPlatformInitializer initializer)
            : base(initializer)
        { }

        protected override async void OnInitialized()
        {
            TranslateExtension.CurrentCultureInfo = CultureInfo.InstalledUICulture;

            InitializeComponent();

            // _appThemeResources = new AppThemeResources(this);
            //await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SimplePage)}");

            await NavigationService.NavigateAsync($"/{nameof(RootPage)}" +
                $"?{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(NumberPage)}" +
                $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(IndexPage)}" +
                $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(FavoritesPage)}" +
                $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(SettingsPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterSingleton<IAppInfo, AppInfoImplementation>()
                .RegisterSingleton<IConnectivity, ConnectivityImplementation>()
                .RegisterSingleton<IPreferences, PreferencesImplementation>()

                .RegisterSingleton<IAssetsService, AssetsService>()
                .RegisterSingleton<IDataStorageService, DataStorageService>()
                .RegisterSingleton<IFilesService, FilesService>()
                .RegisterSingleton<IHymnsService, HymnsService>()
                .RegisterSingleton<IPreferencesService, PreferencesService>();

            containerRegistry.RegisterForNavigation<CustomNavigationPage>("NavigationPage");
            containerRegistry.RegisterForNavigation<SimplePage, SimpleViewModel>();
            containerRegistry.RegisterForNavigation<RootPage, RootViewModel>();
            containerRegistry.RegisterForNavigation<NumberPage, NumberViewModel>();
            containerRegistry.RegisterForNavigation<IndexPage, IndexViewModel>();
            containerRegistry.RegisterForNavigation<FavoritesPage, FavoritesViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>();
        }

        //#region System events

        //protected override void OnStart()
        //{
        //    base.OnStart();
        //    ThemeHelper.CheckTheme();
        //}

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    ThemeHelper.CheckTheme();
        //}

        //protected override void OnAppLinkRequestReceived(Uri uri)
        //{
        //    base.OnAppLinkRequestReceived(uri);
        //}
        //#endregion
    }
}
