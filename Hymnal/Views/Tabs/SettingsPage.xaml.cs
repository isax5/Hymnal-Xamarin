namespace Hymnal.Views;

public sealed partial class SettingsPage : BaseContentPage<SettingsViewModel>
{
    public SettingsPage(SettingsViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}
