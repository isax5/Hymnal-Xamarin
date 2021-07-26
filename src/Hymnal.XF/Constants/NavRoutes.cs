namespace Hymnal.XF.Constants
{
    public static class NavRoutes
    {
#if DEBUG
        public const string SimplePage = "SimplePage";
#endif

        public const string NavPage = "NavigationPage";
        public const string FormSheetNavPage = "FormSheetNavigationPage";

        public const string RootPage = "RootPage";
        public const string NumberPage = "NumberPage";
        public const string IndexPage = "IndexPage";
        public const string FavoritesPage = "FavoritesPage";
        public const string SettingsPage = "SettingsPage";
        public const string HymnPage = "HymnPage";
        public const string MusicSheetPage = "MusicSheetPage";
        public const string SearchPage = "SearchPage";
        public const string RecordsPage = "RecordsPage";
        public const string AlphabeticalIndexPage = "AlphabeticalIndexPage";
        public const string NumericalIndexPage = "NumericalIndexPage";
        public const string ThematicIndexPage = "ThematicIndexPage";
        public const string ThematicHymnsListPage = "ThematicHymnsListPage";
        public const string ThematicSubGroupPage = "ThematicSubGroupPage";

        public static string HymnViewerAsModal = $"{NavPage}/{HymnPage}";
        public static string HymnViewerAsFormSheetModal = $"{FormSheetNavPage}/{HymnPage}";
        public static string RecordsPageAsFormSheetModal = $"{FormSheetNavPage}/{RecordsPage}";
    }
}
