using System;
using Hymnal.XF.Resources.Languages;
using Hymnal.XF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class NumberPage : BaseContentPage<NumberViewModel>, ITabbedPage
    {
        public string TabbedPageName => Languages.Number;

        public NumberPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Tizen)
            {
                backgroundImage.Source = new FileImageSource { File = "Background" };
            }

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

        private void HymnNumberEntry_Focused(object sender, FocusEventArgs e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                HymnNumberEntry.CursorPosition = 0;
                HymnNumberEntry.SelectionLength = HymnNumberEntry.Text != null ? HymnNumberEntry.Text.Length : 0;
            });
        }
    }
}
