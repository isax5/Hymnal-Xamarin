using System;
using System.Collections.Generic;
using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = true)]
    public partial class SettingsPage : MvxContentPage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();

#if TIZEN
            preferencesSection.Remove(fontSizeCell);

            developerSection.Remove(developerCell);
            developerSection.Remove(appVersionCell);
            developerSection.Remove(appBuildCell);
#endif
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            carousel.ItemsSource = new List<string>
            {
                "Hola",
                "Mundo",
                "Como estas"
            };
        }

        private void LetterSize_ValueChanged(object sender, ValueChangedEventArgs e) => LetterSize.Value = Math.Round(e.NewValue, 0);
    }
}
