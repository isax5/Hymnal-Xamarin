using System;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;

namespace Hymnal.UI.Pages.Base
{
    public class CustomMvxContentPage<TViewModel> : MvxContentPage<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public CustomMvxContentPage()
        {
            try
            {
                SetDynamicResource(BackgroundImageSourceProperty, nameof(App.Current.BackImage));
            }
            catch (Exception) { }
        }
    }
}
