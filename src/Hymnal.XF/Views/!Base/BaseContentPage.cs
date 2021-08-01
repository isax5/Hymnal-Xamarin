using Xamarin.Forms;

namespace Hymnal.XF.Views
{
    public class BaseContentPage<TViewModel> : ContentPage where TViewModel: class
    {
        public TViewModel ViewModel
        {
            get => BindingContext as TViewModel;
            set => BindingContext = value;
        }
    }
}
