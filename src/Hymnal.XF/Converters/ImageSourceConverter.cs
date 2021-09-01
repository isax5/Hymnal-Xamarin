using System;
using System.Globalization;
using Xamarin.Forms;

namespace Hymnal.XF.Converters
{
    public sealed class ImageSourceResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageSource.FromResource(string.Format(parameter?.ToString(), value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
