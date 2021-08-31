using System;
using System.Collections.Generic;
using Hymnal.AzureFunctions.Models;
using Refit;

namespace Hymnal.AzureFunctions.Client
{
#if DEBUG
    [Headers("x-functions-key: yad6c0vLVcvxLfa/47aWkoAvfwOAcz1NJUF2AgEzQPCJiYLp1jxNag==")]
#elif RELEASE
    [Headers("x-functions-key: yad6c0vLVcvxLfa/47aWkoAvfwOAcz1NJUF2AgEzQPCJiYLp1jxNag==")]
#endif
    public interface IMusicApi
    {
        [Post("/v1/music/settings")]
        IObservable<IEnumerable<HymnSettingsResponse>> ObserveSettings();

        //[Post("/v1/music/settings")]
        //Task<IEnumerable<MusicSettingsResponse>> GetMusicSettings();

        //[Post("/v1/music/settings")]
        //Task<string> GetMusicSettingsString();
    }
}
