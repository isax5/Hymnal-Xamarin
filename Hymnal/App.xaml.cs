namespace Hymnal;

public partial class App : Application
{
    public App(Setup setup)
    {
        InitializeComponent();

        setup.InitializeFirstChance(this);

        MainPage = new AppShell();

        setup.InitializeLastChance();
    }
}
