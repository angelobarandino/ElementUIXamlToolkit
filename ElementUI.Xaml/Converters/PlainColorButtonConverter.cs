using ElementUI.Xaml.Utils;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ElementUI.Xaml.Converters
{
    public class PlainColorButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var plain = (bool)values[0];
            var brush = (SolidColorBrush)values[1];

            return new SolidColorBrush(new HslColor(brush.Color)
                .Lighten(plain ? 1.55 : 1).ToRgb());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
