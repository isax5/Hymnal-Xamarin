using System.Globalization;
using Hymnal.Core.Services;
using Plugin.Multilingual;

namespace Hymnal.SharedNatives.Services
{
    public class MultilingualService : IMultilingualService
    {
        public CultureInfo CurrentCultureInfo
        {
            get => CrossMultilingual.Current.CurrentCultureInfo;
            set => CrossMultilingual.Current.CurrentCultureInfo = value;
        }


        public CultureInfo DeviceCultureInfo => CrossMultilingual.Current.DeviceCultureInfo;
        public CultureInfo[] CultureInfoList => CrossMultilingual.Current.CultureInfoList;
        public CultureInfo[] NeutralCultureInfoList => CrossMultilingual.Current.NeutralCultureInfoList;

        public CultureInfo GetCultureInfo(string name) => CrossMultilingual.Current.GetCultureInfo(name);
    }
}
