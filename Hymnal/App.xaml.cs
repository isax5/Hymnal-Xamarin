namespace Hymnal;

public partial class App : Application
{
    public App(Setup setup)
    {
        InitializeComponent();

        setup.InitializeFirstChance();

        MainPage = new AppShell();

        setup.InitializeLastChance();
    }
}
