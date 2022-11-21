using System.Reflection;

namespace Hymnal.Extensions;

[ContentProperty(nameof(PageType))]
public sealed class NavigateToExtension
    : BindableObject, IMarkupExtension
{
    public static readonly BindableProperty PageTypeProperty =
        BindableProperty.Create(nameof(PageType), typeof(Type), typeof(NavigateToExtension), null);

    public Type PageType
    {
        get => (Type)GetValue(PageTypeProperty);
        set => SetValue(PageTypeProperty, value);
    }

    public object ProvideValue(IServiceProvider serviceProvider)
        => new Command(() =>
        {
            Shell.Current.GoToAsync(PageType.Name);
        });
}
