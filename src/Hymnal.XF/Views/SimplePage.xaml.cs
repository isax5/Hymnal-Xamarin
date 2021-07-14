using Hymnal.XF.ViewModels;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimplePage : BaseContentPage<SimpleViewModel>
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
