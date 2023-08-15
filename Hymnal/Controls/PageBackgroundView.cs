namespace Hymnal.Controls;

public sealed class PageBackgroundView : ContentView
{
    public static readonly BindableProperty ImageVisibleProperty =
        BindableProperty.Create(nameof(ImageVisible), typeof(bool), typeof(PageBackgroundView), true);

    public bool ImageVisible
    {
        get => (bool)GetValue(ImageVisibleProperty);
        set => SetValue(ImageVisibleProperty, value);
    }
}
