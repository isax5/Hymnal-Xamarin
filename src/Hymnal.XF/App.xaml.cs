using System.Globalization;
using Hymnal.XF.Extensions.i18n;
using Hymnal.XF.Helpers;
using Hymnal.XF.Services;
using Hymnal.XF.ViewModels;
using Hymnal.XF.Views;
using Hymnal.XF.Views.Custom;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public static new App Current;
        public ThemeHelper ThemeHelper;
        // private AppThemeResources _appThemeResources;

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            Current = this;
        }

        protected override async void OnInitialized()
        {
            TranslateExtension.CurrentCultureInfo = CultureInfo.InstalledUICulture;

            InitializeComponent();

            ThemeHelper = new ThemeHelper(Current);

            // _appThemeResources = new AppThemeResources(this);
            await NavigationService.NavigateAsync($"{nameof(SimplePage)}");

            //await NavigationService.NavigateAsync($"/{nameof(RootPage)}" +
            //    $"?{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(NumberPage)}" +
            //    $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(IndexPage)}" +
            //    $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(FavoritesPage)}" +
            //    $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(SettingsPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterSingleton<IAppInfo, AppInfoImplementation>()
                .RegisterSingleton<IConnectivity, ConnectivityImplementation>()
                .RegisterSingleton<IPreferences, PreferencesImplementation>()

                .RegisterSingleton<IDataStorageService, DataStorageService>()
                .RegisterSingleton<IStorageManagerService, StorageManagerService>()
                .RegisterSingleton<IAssetsService, AssetsService>()
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

        #region System events
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
        #endregion
    }
}
