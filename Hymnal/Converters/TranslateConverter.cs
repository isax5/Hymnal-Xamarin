using System.Globalization;

namespace Hymnal.Converters;

public sealed class TranslateConverter : IValueConverter
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
