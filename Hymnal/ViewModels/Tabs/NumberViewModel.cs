using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class NumberViewModel : BaseViewModel
{
    [ObservableProperty]
    private string hymnNumber;

    [RelayCommand]
    private async void OpenHymnAsync(string number)
    {
        await Shell.Current.DisplayAlert("Numero seleccionado", number ?? "NADA", "Ok");
    }

    public NumberViewModel()
    {
#if DEBUG
        hymnNumber = 255.ToString();
#endif
    }
}
