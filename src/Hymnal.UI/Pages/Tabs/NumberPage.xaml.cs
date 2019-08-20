using System;
using Hymnal.Core.ViewModels;
using Hymnal.UI.Pages.Base;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = true, Title = "Number", Icon = "TabNumber")]
    public partial class NumberPage : CustomMvxContentPage<NumberViewModel>
    {
        public NumberPage()
        {
            InitializeComponent();
        }

        private void OpenButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HymnNumber.Text))
                HymnNumber.Focus();
        }
    }
}
