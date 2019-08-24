using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = true, Title = "Number", Icon = "TabNumber")]
    public partial class NumberPage : MvxContentPage<NumberViewModel>
    {
        public NumberPage()
        {
            InitializeComponent();

            SizeChanged += (s, args) =>
            {
                var visualState = Width > Height ? "Landscape" : "Portrait";
                VisualStateManager.GoToState(MainStack, visualState);
                VisualStateManager.GoToState(ContainerStack, visualState);
            };
        }

        private void OpenButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HymnNumber.Text))
                HymnNumber.Focus();
        }
    }
}
