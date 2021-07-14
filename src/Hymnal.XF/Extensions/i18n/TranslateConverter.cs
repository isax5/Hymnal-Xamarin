using System;
using System.Globalization;
using Xamarin.Forms;

namespace Hymnal.XF.Extensions.i18n
{
    public class TranslateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TranslateExtension.GetTranslation(string.Format(parameter?.ToString(), value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
