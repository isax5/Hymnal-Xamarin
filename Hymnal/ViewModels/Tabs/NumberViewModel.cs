using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class NumberViewModel : BaseViewModel
{
    [ObservableProperty]
    private string hymnNumber;


    public NumberViewModel()
    {
#if DEBUG
        hymnNumber = 255.ToString();
#endif
    }


    [RelayCommand]
    private async void OpenHymnAsync(string number)
    {
        if (string.IsNullOrEmpty(number))
            return;

        await Shell.Current.GoToAsync(nameof(HymnPage),
            new HymnIdParameter()
            {
                Number = int.Parse(number),
                SaveInRecords = true,
                HymnalLanguage = InfoConstants.HymnsLanguages.First(),
            }.AsParameter());
    }
}
