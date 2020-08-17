using System.Threading.Tasks;
using Hymnal.Resources;

namespace Hymnal.Core.Services
{
    public class FilesService : IFilesService
    {
        private readonly IAssets assets;

        public FilesService(IAssets assets)
        {
            this.assets = assets;
        }
        //public async Task<string> ReadFileAsync(string fileName)
        //{
        //    using (Stream stream = await FileSystem.OpenAppPackageFileAsync(fileName))
        //    {
        //        using (var reader = new StreamReader(stream))
        //        {
        //            return await reader.ReadToEndAsync();
        //        }
        //    }
        //}

        //public string GetPathFile(string fileName)
        //{
        //    var libraryPath = FileSystem.AppDataDirectory;
        //    return Path.Combine(libraryPath, fileName);
        //}
        public async Task<string> ReadFileAsync(string fileName)
        {
            return assets.GetResourceString(fileName);
        }
    }
}
