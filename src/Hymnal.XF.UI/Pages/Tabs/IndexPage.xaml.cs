using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = false)]
    [MvxCarouselPagePresentation(CarouselPosition.Root, NoHistory = true)]
    public partial class IndexPage : MvxCarouselPage<IndexViewModel>
    {
        public IndexPage()
        {
            InitializeComponent();
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
