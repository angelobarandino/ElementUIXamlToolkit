using ElementUI.Xaml.Utils;
using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace ElementUI.Xaml.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(SolidColorBrush))]
    public class LightenExtension : MarkupExtension
    {
        public double Amount { get; set; }

        public SolidColorBrush Source { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new SolidColorBrush(new HslColor(Source.Color).Lighten(Amount).ToRgb ());
        }
    }
}
