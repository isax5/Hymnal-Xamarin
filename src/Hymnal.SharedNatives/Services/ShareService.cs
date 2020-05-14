using System.Threading.Tasks;
using Hymnal.Core.Services;

namespace Hymnal.SharedNatives.Services
{
    public class ShareService : IShareService
    {
        public Task Share(string title, string text) => Xamarin.Essentials.Share.RequestAsync(text: text, title: title);
        public Task Share(string text) => Xamarin.Essentials.Share.RequestAsync(text: text);
    }
}
