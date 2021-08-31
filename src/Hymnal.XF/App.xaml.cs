using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Helpers;
using Hymnal.XF.ViewModels;
using Hymnal.XF.Views;
using Hymnal.XF.Views.Custom;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class App : PrismApplication
    {
        public static new App Current;
        private readonly Setup setup = new();
        public ThemeHelper ThemeHelper;

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            Current = this;
        }

        protected override async void OnInitialized()
        {
            setup.InitializeFirstChance(this);
            InitializeComponent();
            setup.InitializeLastChance(this);

            await NavigationService.NavigateAsync($"{NavRoutes.RootPage}" +
                                                  $"?{KnownNavigationParameters.CreateTab}={NavRoutes.NavPage}|{NavRoutes.NumberPage}" +
                                                  $"&{KnownNavigationParameters.CreateTab}={NavRoutes.NavPage}|{NavRoutes.IndexPage}" +
                                                  $"&{KnownNavigationParameters.CreateTab}={NavRoutes.NavPage}|{NavRoutes.FavoritesPage}" +
                                                  $"&{KnownNavigationParameters.CreateTab}={NavRoutes.NavPage}|{NavRoutes.SettingsPage}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            setup.RegisterDependencies(containerRegistry);

            // Register Pages
            containerRegistry.RegisterForNavigation<CustomNavigationPage>(NavRoutes.NavPage);
            containerRegistry.RegisterForNavigation<FormSheetNavigationPage>(NavRoutes.FormSheetNavPage);

            containerRegistry.RegisterForNavigation<RootPage, RootViewModel>();
            containerRegistry.RegisterForNavigation<NumberPage, NumberViewModel>();
            containerRegistry.RegisterForNavigation<IndexPage, IndexViewModel>();
            containerRegistry.RegisterForNavigation<FavoritesPage, FavoritesViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>();

            containerRegistry.RegisterForNavigation<HymnPage, HymnViewModel>();
            containerRegistry.RegisterForNavigation<MusicSheetPage, MusicSheetViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchViewModel>();
            containerRegistry.RegisterForNavigation<RecordsPage, RecordsViewModel>();
            containerRegistry.RegisterForNavigation<AlphabeticalIndexPage, AlphabeticalIndexViewModel>();
            containerRegistry.RegisterForNavigation<NumericalIndexPage, NumericalIndexViewModel>();
            containerRegistry.RegisterForNavigation<ThematicIndexPage, ThematicIndexViewModel>();
            containerRegistry.RegisterForNavigation<ThematicHymnsListPage, ThematicHymnsListViewModel>();
            containerRegistry.RegisterForNavigation<ThematicSubGroupPage, ThematicSubGroupViewModel>();
        }

        public void PerformPageRequest(string navRoute, bool modal = false, bool animated = true)
        {
            MainThread.InvokeOnMainThreadAsync(async delegate
            {
                await NavigationService.NavigateAsync(navRoute, modal, animated);
            });
        }
    }
}
