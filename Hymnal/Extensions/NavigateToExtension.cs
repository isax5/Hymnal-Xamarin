using System.Windows.Input;

namespace Hymnal.Extensions;

[ContentProperty(nameof(PageType))]
public sealed class NavigateToExtension : BindableObject, IMarkupExtension<ICommand>, ICommand
{
    public static readonly BindableProperty PageTypeProperty =
        BindableProperty.Create(nameof(PageType), typeof(Type), typeof(NavigateToExtension), null);

    public Type PageType
    {
        get => (Type)GetValue(PageTypeProperty);
        set => SetValue(PageTypeProperty, value);
    }

    private bool IsNavigating { get; set; }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => !IsNavigating;

    public async void Execute(object parameter)
    {
        IsNavigating = true;
        try
        {
            RaiseCanExecuteChanged();

            if (parameter is null)
                await Shell.Current.GoToAsync(PageType.Name, true);
            else
                await Shell.Current.GoToAsync(PageType.Name, true, parameter?.AsParameter());
        }
        catch (Exception ex)
        {
            ex.Report();
        }
        finally
        {
            IsNavigating = false;
            RaiseCanExecuteChanged();
        }
    }

    public ICommand ProvideValue(IServiceProvider serviceProvider) => this;

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => this;

    private void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
