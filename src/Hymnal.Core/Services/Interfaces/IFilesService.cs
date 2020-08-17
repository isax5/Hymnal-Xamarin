using System.Threading.Tasks;

namespace Hymnal.Core.Services
{
    public interface IFilesService
    {
        Task<string> ReadFileAsync(string fileName);

        //string GetPathFile(string fileName);
    }
}
