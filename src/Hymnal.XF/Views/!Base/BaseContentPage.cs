namespace Hymnal.XF.Views
{
    public abstract class BaseContentPage<TViewModel> : BaseContentPage where TViewModel: class
    {
        public TViewModel ViewModel
        {
            get => BindingContext as TViewModel;
            set => BindingContext = value;
        }
    }
}
