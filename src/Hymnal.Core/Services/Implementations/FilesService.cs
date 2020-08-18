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

        public async Task<string> ReadFileAsync(string fileName) => await Task.FromResult(assets.GetResourceString(fileName));
    }
}
