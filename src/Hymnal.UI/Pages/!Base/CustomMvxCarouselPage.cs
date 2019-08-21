using System;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;

namespace Hymnal.UI.Pages.Base
{
    public class CustomMvxCarouselPage<TViewModel> : MvxCarouselPage<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public CustomMvxCarouselPage()
        {
            try
            {
                SetDynamicResource(BackgroundImageSourceProperty, nameof(App.Current.BackLightImage));
            }
            catch (Exception) { }
        }
    }
}
