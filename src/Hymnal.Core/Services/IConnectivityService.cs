namespace Hymnal.Core.Services
{
    /// <summary>
    /// Connection Properties
    /// </summary>
    public interface IConnectivityService
    {
        bool InternetAccess { get; }
    }
}
