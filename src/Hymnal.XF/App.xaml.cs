using Hymnal.XF.Helpers;
using Hymnal.XF.ViewModels;
using Hymnal.XF.Views;
using Hymnal.XF.Views.Custom;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public static new App Current;
        public Setup Setup = new Setup();
        public ThemeHelper ThemeHelper;

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            Current = this;
        }

        protected override async void OnInitialized()
        {
            Setup.InitializeFirstChance(this);
            InitializeComponent();

            Setup.InitializeLastChance(this);

            await NavigationService.NavigateAsync($"{nameof(RootPage)}" +
                $"?{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(NumberPage)}" +
                $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(SimplePage)}");

            //await NavigationService.NavigateAsync($"/{nameof(RootPage)}" +
            //    $"?{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(NumberPage)}" +
            //    $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(IndexPage)}" +
            //    $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(FavoritesPage)}" +
            //    $"&{KnownNavigationParameters.CreateTab}={nameof(NavigationPage)}|{nameof(SettingsPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Setup.RegisterDependencies(containerRegistry);

            // Register Pages
            containerRegistry.RegisterForNavigation<CustomNavigationPage>(nameof(NavigationPage));
            containerRegistry.RegisterForNavigation<SimplePage, SimpleViewModel>();
            containerRegistry.RegisterForNavigation<RootPage, RootViewModel>();
            containerRegistry.RegisterForNavigation<NumberPage, NumberViewModel>();
            containerRegistry.RegisterForNavigation<IndexPage, IndexViewModel>();
            containerRegistry.RegisterForNavigation<FavoritesPage, FavoritesViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>();
        }
    }
}
