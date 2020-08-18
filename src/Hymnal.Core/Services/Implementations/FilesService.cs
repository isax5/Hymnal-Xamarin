using System.Threading.Tasks;

namespace Hymnal.Core.Services
{
    public class FilesService : IFilesService
    {
        private readonly IAssetsService assetsService;

        public FilesService(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }

        public async Task<string> ReadFileAsync(string fileName) => await Task.FromResult(assetsService.GetResourceString(fileName));
    }
}
