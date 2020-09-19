using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.AzureFunctions.Models;
using Refit;

namespace Hymnal.AzureFunctions.Client
{
    [Headers("x-functions-key: ")]
    public interface IMusicAPI
    {
        [Post("/v1/music/settings")]
        Task<IEnumerable<MusicSettingsResponse>> GetMusicSettings();

        [Post("/v1/music/settings")]
        Task<string> GetMusicSettingsString();
    }
}
