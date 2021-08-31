using Xamarin.Forms;

namespace Hymnal.XF.Views
{
    public abstract class BaseContentPage<TViewModel> : ContentPage where TViewModel: class
    {
        public TViewModel ViewModel
        {
            get => BindingContext as TViewModel;
            set => BindingContext = value;
        }
    }
}
