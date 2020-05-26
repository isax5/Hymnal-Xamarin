using System.Globalization;

namespace Hymnal.Core.Services
{
    public interface IMultilingualService
    {
        CultureInfo CurrentCultureInfo { get; set; }
        CultureInfo DeviceCultureInfo { get; }
        CultureInfo[] CultureInfoList { get; }
        CultureInfo[] NeutralCultureInfoList { get; }

        CultureInfo GetCultureInfo(string name);
    }
}
