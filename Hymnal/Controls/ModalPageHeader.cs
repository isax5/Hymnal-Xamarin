namespace Hymnal.Controls;

public sealed class ModalPageHeader : ContentView
{
    public static readonly BindableProperty ContentPageProperty =
        BindableProperty.Create(nameof(ContentPage), typeof(ContentPage), typeof(ModalPageHeader), null);

    public ContentPage ContentPage
    {
        get => (ContentPage)GetValue(ContentPageProperty);
        set => SetValue(ContentPageProperty, value);
    }

    public static readonly BindableProperty ShowNavigationBarProperty =
        BindableProperty.Create(nameof(ShowNavigationBar), typeof(bool), typeof(ModalPageHeader), true);

    public bool ShowNavigationBar
    {
        get => (bool)GetValue(ShowNavigationBarProperty);
        set => SetValue(ShowNavigationBarProperty, value);
    }
}
