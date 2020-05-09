using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    //[MvxTabbedPagePresentation(WrapInNavigationPage = true)]
    public partial class SimplePage : MvxContentPage<SimpleViewModel>
    {
        public SimplePage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Welcome", "Message for testing", "Oki");
        }

        private void Button_Clicked_1(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(exampleEntry.Text))
            {
                exampleEntry.Focus();
            }
            else
            {
                DisplayAlert("Text entered", exampleEntry.Text, "Awesome");
            }
        }
    }
}
