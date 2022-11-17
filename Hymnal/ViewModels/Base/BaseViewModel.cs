using CommunityToolkit.Mvvm.ComponentModel;

namespace Hymnal.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NotBusy))]
    private bool busy;

    public bool NotBusy => Busy;
}
