using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxCarouselPagePresentation(CarouselPosition.Carousel, NoHistory = true)]
    public partial class AlphabeticalIndexPage : MvxContentPage<AlphabeticalIndexViewModel>
    {
        public AlphabeticalIndexPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                try
                {
                    SetDynamicResource(BackgroundImageSourceProperty, nameof(App.Current.BackLightImage));
                }
                catch (Exception) { }
            }
        }
    }
}
