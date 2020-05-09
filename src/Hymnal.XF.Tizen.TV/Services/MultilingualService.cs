using System.Globalization;
using Hymnal.Core.Services;

namespace Hymnal.XF.Tizen.Tv.Services
{
    public class MultilingualService : IMultilingualService
    {
        public MultilingualService()
        {
            currentCultureInfo = new CultureInfo("en-US");
        }

        private CultureInfo currentCultureInfo;
        public CultureInfo CurrentCultureInfo
        {
            get
            {
                //if (currentCultureInfo == null)
                //    currentCultureInfo = new CultureInfo("en-US");
                return currentCultureInfo;
            }
            set => currentCultureInfo = value;
        }

        public CultureInfo DeviceCultureInfo => currentCultureInfo;

        public CultureInfo[] CultureInfoList =>  new[] { currentCultureInfo };

        public CultureInfo[] NeutralCultureInfoList => new[] { currentCultureInfo };

        public CultureInfo GetCultureInfo(string name) => currentCultureInfo;
    }
}
