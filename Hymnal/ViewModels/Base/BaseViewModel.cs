using CommunityToolkit.Mvvm.ComponentModel;

namespace Hymnal.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NotBusy))]
    private bool busy;

    public bool NotBusy => Busy;


    #region Life Cycle
    public virtual void Initialize()
    { }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// When navigate from this VM
    /// </summary>
    /// <param name="args"></param>
    public virtual void OnNavigatedFrom(NavigatedFromEventArgs args)
    { }

    /// <summary>
    /// When navigate to this VM
    /// </summary>
    /// <param name="args"></param>
    public virtual void OnNavigatedTo(NavigatedToEventArgs args)
    { }

    public virtual void OnNavigatingFrom(NavigatingFromEventArgs args)
    { }

    /// <summary>
    /// On Appearing
    /// </summary>
    public virtual void OnAppearing()
    { }

    /// <summary>
    /// On Disappearing
    /// </summary>
    public virtual void OnDisappearing()
    { }
    #endregion
}
