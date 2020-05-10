using System.Threading.Tasks;

namespace Hymnal.Core.Services
{
    public interface IShareService
    {
        Task Share(string title, string text);
        Task Share(string text);
    }
}
