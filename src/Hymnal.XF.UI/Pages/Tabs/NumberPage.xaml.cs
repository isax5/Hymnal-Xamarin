using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = true)]
    public partial class NumberPage : MvxContentPage<NumberViewModel>
    {
        public NumberPage()
        {
            InitializeComponent();
#if TIZEN
            backgroundImage.Source = new FileImageSource { File = "Background.png" };
#endif

            SizeChanged += (s, args) =>
            {
                var visualState = Width > Height ? "Landscape" : "Portrait";
                VisualStateManager.GoToState(MainStack, visualState);
            };
        }

        private void OpenButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HymnNumberEntry.Text))
                HymnNumberEntry.Focus();
        }
    }
}
