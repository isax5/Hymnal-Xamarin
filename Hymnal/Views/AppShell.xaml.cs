namespace Hymnal.Views;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ModalNavPage), typeof(ModalNavPage));
        Routing.RegisterRoute(nameof(ModalHymnNavPage), typeof(ModalHymnNavPage));
        //Routing.RegisterRoute(nameof)

        Routing.RegisterRoute(nameof(RecordsPage), typeof(RecordsPage));

        Routing.RegisterRoute(nameof(HymnPage), typeof(HymnPage));
        Routing.RegisterRoute(nameof(MusicSheetPage), typeof(MusicSheetPage));
        Routing.RegisterRoute(nameof(ThematicHymnsListPage), typeof(ThematicHymnsListPage));
        Routing.RegisterRoute(nameof(ThematicSubGroupPage), typeof(ThematicSubGroupPage));
    }
}
