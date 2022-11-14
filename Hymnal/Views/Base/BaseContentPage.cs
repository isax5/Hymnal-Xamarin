namespace Hymnal.Views;

public class BaseContentPage<TViewModel> : BaseContentPage where TViewModel : class
{
    public TViewModel ViewModel
    {
        get => BindingContext as TViewModel;
        set => BindingContext = value;
    }
}
