using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class NumberViewModel : BaseViewModel
{
    private readonly PreferencesService preferencesService;

    [ObservableProperty]
    private string hymnNumber;


    public NumberViewModel(PreferencesService preferencesService)
    {
#if DEBUG
        hymnNumber = 255.ToString();
        this.preferencesService = preferencesService;
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
                HymnalLanguage = preferencesService.ConfiguredHymnalLanguage,
            }.AsParameter());
    }
}
