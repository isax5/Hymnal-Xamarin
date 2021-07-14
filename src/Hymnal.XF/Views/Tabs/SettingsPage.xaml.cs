using System;
using Hymnal.XF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : BaseContentPage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Tizen)
            {
                preferencesSection.Remove(fontSizeCell);

                developerSection.Remove(developerCell);
                developerSection.Remove(appVersionCell);
                developerSection.Remove(appBuildCell);
            }
        }

        // TODO: Se puede crear un converter o algo similar
        private void LetterSize_ValueChanged(object sender, ValueChangedEventArgs e) => LetterSize.Value = Math.Round(e.NewValue, 0);
    }
}
