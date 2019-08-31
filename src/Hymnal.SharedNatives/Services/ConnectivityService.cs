using Hymnal.Core.Services;
using Xamarin.Essentials;

namespace Hymnal.SharedNatives.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public bool InternetAccess => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
