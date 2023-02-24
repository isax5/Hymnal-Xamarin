using System.Globalization;

namespace Hymnal.Converters
{
    public sealed class HymnImageNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is HymnIdParameter hymnIdParameter
                ? $"{hymnIdParameter.HymnalLanguage.GetMusicSheetSource(hymnIdParameter.Number, parameter?.ToString())}"
                : string.Empty;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
