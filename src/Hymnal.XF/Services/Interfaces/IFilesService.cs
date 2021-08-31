using System.Threading.Tasks;

namespace Hymnal.XF.Services
{
    public interface IFilesService
    {
        Task<string> ReadFileAsync(string fileName);
    }
}
