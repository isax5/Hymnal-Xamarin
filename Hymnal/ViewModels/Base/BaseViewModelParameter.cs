namespace Hymnal.ViewModels;

[QueryProperty(nameof(Parameter), nameof(Parameter))]
public partial class BaseViewModelParameter<T> : BaseViewModel
{
    [ObservableProperty]
    private T parameter;
}
