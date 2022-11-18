namespace Hymnal.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(HymnPage), typeof(HymnPage));
	}
}
