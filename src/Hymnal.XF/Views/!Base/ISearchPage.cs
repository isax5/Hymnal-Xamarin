namespace Hymnal.XF.Views
{
    public interface ISearchPage
    {
        void OnSearchBarTextChanged(in string text);
        ISearchPageSettings Settings { get; }
    }

    public interface ISearchPageSettings
    {
        string PlaceHolder { get; }
        bool InitialDisplay { get; }
        bool InitiallyFocus { get; }

        bool HideWhenPageDisappear { get; }

        /// <para>
        /// En caso de true: Oculta titue, toolbars, back, etc
        /// </para>
        /// <para>
        /// En caso de false: Oculta solo title
        /// </para>
        /// <para>
        /// Produce problemas si se configura false en una pagina que debe retornar luego de realizar una busqueda en iOS
        /// </para>
        /// </summary>
        bool HideNavBarWhenSearch { get; }
    }

    public class SearchPageSettings : ISearchPageSettings
    {
        public string PlaceHolder { get; set; }
        public bool InitialDisplay { get; set; } = true;
        public bool InitiallyFocus { get; set; } = false;
        public bool HideWhenPageDisappear { get; set; } = false;

        /// <para>
        /// En caso de true: Oculta titue, toolbars, back, etc
        /// </para>
        /// <para>
        /// En caso de false: Oculta solo title
        /// </para>
        /// <para>
        /// Produce problemas si se configura false en una pagina que debe retornar luego de realizar una busqueda en iOS
        /// </para>
        /// </summary>
        public bool HideNavBarWhenSearch { get; set; } = true;
    }
}
