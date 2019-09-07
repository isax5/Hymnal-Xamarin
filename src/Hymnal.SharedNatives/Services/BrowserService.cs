using System.Drawing;
using System.Threading.Tasks;
using Hymnal.Core.Services;
using Xamarin.Essentials;

namespace Hymnal.SharedNatives.Services
{
    public class BrowserService : IBrowserService
    {
        public async Task OpenBrowserAsync(string url)
        {
            await Browser.OpenAsync(url, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.Black,
                PreferredControlColor = Color.White
            });
        }
    }
}
