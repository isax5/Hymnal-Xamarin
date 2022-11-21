namespace Hymnal.Extensions;

[ContentProperty(nameof(GoBackType))]
public sealed class GoBackExtension : BindableObject, IMarkupExtension
{
    public static readonly BindableProperty GoBackTypeProperty =
        BindableProperty.Create(nameof(GoBackType), typeof(GoBackType), typeof(GoBackExtension), GoBackType.Default);

    public GoBackType GoBackType
    {
        get => (GoBackType)GetValue(GoBackTypeProperty);
        set => SetValue(GoBackTypeProperty, value);
    }

    public object ProvideValue(IServiceProvider serviceProvider)
        => new Command(() =>
        {
            Shell.Current.GoToAsync("..", true);
        });
}


public enum GoBackType
{
    Default,
    ToRoot
}
