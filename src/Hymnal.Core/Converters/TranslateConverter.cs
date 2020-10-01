using System;
using System.Globalization;
using Hymnal.Core.Helpers;

namespace Hymnal.Core.Converters
{
    public class TranslateConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TranslateExtension.GetTranslation(string.Format(parameter as string, value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
