using System;
using System.Threading.Tasks;
using Hymnal.XF.Resources.Languages;
using Hymnal.XF.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPage : CarouselPage, ITabbedPage, IInitialize, IInitializeAsync
    {
        public string TabbedPageName => Languages.Index;

        public IndexPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Manual initilization for pages inside the CarouselPage
        /// </summary>
        /// <param name="parameters"></param>
        public void Initialize(INavigationParameters parameters)
        {
            foreach (ContentPage page in Children)
            {
                if (page.BindingContext is BaseViewModel viewModel)
                    viewModel.Initialize(parameters);
            }
        }

        /// <summary>
        /// Manual initilization for pages inside the CarouselPage
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            foreach (ContentPage page in Children)
            {
                if (page.BindingContext is BaseViewModel viewModel)
                    await viewModel.InitializeAsync(parameters);
            }
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            var index = Children.IndexOf(CurrentPage);

            switch (index)
            {
                case 0:
                    AlphabeticalListToolbar.IsEnabled = false;
                    NumericalListToolbar.IsEnabled = true;
                    ThematicListToolbar.IsEnabled = true;
                    break;

                case 1:
                    AlphabeticalListToolbar.IsEnabled = true;
                    NumericalListToolbar.IsEnabled = false;
                    ThematicListToolbar.IsEnabled = true;
                    break;

                case 2:
                    AlphabeticalListToolbar.IsEnabled = true;
                    NumericalListToolbar.IsEnabled = true;
                    ThematicListToolbar.IsEnabled = false;
                    break;

                default:
                    break;
            }
        }

        private void AlphabeticalListToolbar_Clicked(object sender, EventArgs e)
        {
            var bxt = BindingContext;
            CurrentPage = Children[0];
        }

        private void NumericalListToolbar_Clicked(object sender, EventArgs e)
        {
            CurrentPage = Children[1];
        }

        private void ThematicListToolbar_Clicked(object sender, EventArgs e)
        {
            CurrentPage = Children[2];
        }
    }
}
