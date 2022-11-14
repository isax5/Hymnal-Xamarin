using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;
public sealed partial class NumberViewModel : BaseViewModel
{
    [RelayCommand]
    private void OpenHymnAsync(string number)
    {
    }
}
