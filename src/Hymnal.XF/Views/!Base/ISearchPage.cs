using System;
using Xamarin.Forms;

namespace Hymnal.XF.Views
{
    public interface ISearchPage
    {
        ISearchDelegate Delegate { get; set; }

        string PlaceholderText { get; }
        Color PlaceHolderColor { get; }
        Color TextColor { get; }
        ISearchPageSettings Settings { get; }

        void OnSearchBarTextChanged(in string text);
        void SearchTapped(in string text);
        void Focused();
        void Unfocused();
        void Canceled();
    }

    public interface ISearchDelegate
    {
        string SearchText { get; set; }
        void BecomeFirstResponder();
        void DismissKeyboard(bool keepSearchControllerActive = true);
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

    public sealed class SearchPageSettings : ISearchPageSettings
    {
        public ISearchDelegate Delegate { get; set; } = null;
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
