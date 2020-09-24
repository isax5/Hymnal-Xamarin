using System.Threading.Tasks;
using Hymnal.Core.Services;
using Xamarin.Forms;

namespace Hymnal.XF.UI.Services
{
    public class DialogService : IDialogService
    {
        public Page MainPage { get; set; } = App.Current.MainPage;

        public Task Alert(string title, string cancel)
        {
            return MainPage.DisplayAlert(title, null, cancel);
        }

        public Task Alert(string title, string message, string cancel)
        {
            return MainPage.DisplayAlert(title, message, cancel);
        }

        public Task Alert(string title, string message, string accept, string cancel)
        {
            return MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }
    }
}
