using Hymnal.XF.UI.Pages.Custom;
using MvvmCross.Forms.Presenters;
using Xamarin.Forms;

namespace Hymnal.XF.iOS.Custom
{
    public class CustomFormsPagePresenter : MvxFormsPagePresenter
    {
        public CustomFormsPagePresenter(IMvxFormsViewPresenter platformPresenter) : base(platformPresenter)
        { }

        protected override NavigationPage CreateNavigationPage(Page pageRoot = null)
        {
            return new CustomNavigationPage(pageRoot);
            //return base.CreateNavigationPage(pageRoot);
        }
    }
}
