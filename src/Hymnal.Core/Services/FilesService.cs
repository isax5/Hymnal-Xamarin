using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Hymnal.Core.Services
{
    public class FilesService : IFilesService
    {
        public async Task<string> ReadFileAsync(string fileName)
        {
            using (Stream stream = await FileSystem.OpenAppPackageFileAsync(fileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
