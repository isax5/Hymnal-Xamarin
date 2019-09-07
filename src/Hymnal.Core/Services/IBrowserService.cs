using System.Threading.Tasks;

namespace Hymnal.Core.Services
{
    public interface IBrowserService
    {
        Task OpenBrowserAsync(string url);
    }
}
