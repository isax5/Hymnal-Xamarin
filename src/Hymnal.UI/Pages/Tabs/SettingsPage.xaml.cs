using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = true, Title = "Settings", Icon = "TabSettings")]
    public partial class SettingsPage : MvxContentPage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void LetterSize_ValueChanged(object sender, ValueChangedEventArgs e) => LetterSize.Value = Math.Round(e.NewValue, 0);
    }
}
