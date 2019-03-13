using ElementUI.Wpf.Utils;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ElementUI.Wpf.Converters
{
    public class HslColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var plain = (bool)values[0];
            var brush = (SolidColorBrush)values[1];
            var amount = System.Convert.ToDouble(parameter);

            return new SolidColorBrush(new HslColor(brush.Color)
                .Lighten(plain ? amount : 1).ToRgb());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
