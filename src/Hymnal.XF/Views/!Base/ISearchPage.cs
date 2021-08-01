using System;
using Xamarin.Forms;

namespace Hymnal.XF.Views
{
    public interface ISearchPage
    {
        string PlaceholderText { get; }
        Color PlaceHolderColor { get; }
        Color TextColor { get; }
        IObservable<OSAppTheme> ObservableThemeChange { get; }
        ISearchPageSettings Settings { get; }

        void OnSearchBarTextChanged(in string text);
    }

    public interface ISearchPageSettings
    {
        bool InitialDisplay { get; }
        bool InitiallyFocus { get; }

        bool HideWhenPageDisappear { get; }

        /// <summary>
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
        public bool InitialDisplay { get; set; } = true;
        public bool InitiallyFocus { get; set; } = false;
        public bool HideWhenPageDisappear { get; set; } = false;

        /// <summary>
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
