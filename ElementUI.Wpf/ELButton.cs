using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using ElementUI.Wpf.Utils;

namespace ElementUI.Wpf
{
    public class ELButton : ButtonBase
    {
        private SolidColorBrush _typeColorBrush;

        public ELButton()
        {
            AddHandler(ELButton.MouseEnterEvent, new MouseEventHandler(OnMouseEnter));
            AddHandler(ELButton.MouseLeaveEvent, new MouseEventHandler(OnMouseLeave));
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!(GetTemplateChild("border") is Border border))
                return;

            if (Type == ELButtonType.Default)
                return;

            if (Plain)
            {
                SetValue(ELButton.ForegroundProperty, Brushes.White);

                border.SetValue(Border.BackgroundProperty, _typeColorBrush);
            }
            else
            {
                border.SetValue(Border.BackgroundProperty, new SolidColorBrush(new HslColor(_typeColorBrush.Color).Lighten(1.17).ToRgb()));
            }

        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!(GetTemplateChild("border") is Border border))
                return;

            if (Type == ELButtonType.Default)
                return;

            if (Plain)
            {
                SetValue(ELButton.ForegroundProperty, _typeColorBrush);

                border.SetValue(Border.BackgroundProperty, new SolidColorBrush(new HslColor(_typeColorBrush.Color).Lighten(1.55).ToRgb()));
            }
            else
            {
                border.SetValue(Border.BackgroundProperty, _typeColorBrush);
            }
        }

        public override void OnApplyTemplate()
        {
            GetCurrentBackground();

            TypePropertyChangedCallback(this, new DependencyPropertyChangedEventArgs());

            base.OnApplyTemplate();
        }

        private void GetCurrentBackground()
        {
            if (!(GetTemplateChild("border") is Border border))
                return;

            _typeColorBrush = border.Background as SolidColorBrush;
        }

        public ELButtonType Type
        {
            get { return (ELButtonType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register(
                nameof(Type),
                typeof(ELButtonType),
                typeof(ELButton),
                new PropertyMetadata(ELButtonType.Default, TypePropertyChangedCallback));

        private static void TypePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var elButton = (ELButton)dependencyObject;
            if (!(elButton.GetTemplateChild("border") is Border border))
                return;

            if (elButton.Type == ELButtonType.Default)
                return;

            if (!elButton.Plain)
                return;

            var curBgColor = border.Background as SolidColorBrush;
            var newBgColor = new HslColor(curBgColor.Color)
                .Lighten(1.55)
                .ToRgb();

            var colorBrush = new SolidColorBrush(newBgColor);

            border.SetValue(Border.BackgroundProperty, colorBrush);

            elButton.SetValue(Border.BackgroundProperty, curBgColor);
            elButton.SetValue(ELButton.ForegroundProperty, curBgColor);
        }

        public ELButtonSize Size
        {
            get { return (ELButtonSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(
                nameof(Size),
                typeof(ELButtonSize),
                typeof(ELButton),
                new PropertyMetadata(ELButtonSize.Default));

        public ELButtonCorners Corners
        {
            get { return (ELButtonCorners)GetValue(CornersProperty); }
            set { SetValue(CornersProperty, value); }
        }

        public static readonly DependencyProperty CornersProperty =
            DependencyProperty.Register(
                nameof(Corners),
                typeof(ELButtonCorners),
                typeof(ELButton),
                new PropertyMetadata(ELButtonCorners.Default));
        public bool Plain
        {
            get { return (bool)GetValue(PlainProperty); }
            set { SetValue(PlainProperty, value); }
        }

        public static readonly DependencyProperty PlainProperty =
            DependencyProperty.Register(
                nameof(Plain),
                typeof(bool),
                typeof(ELButton),
                new PropertyMetadata(false));
    }

    public enum ELButtonType
    {
        Default,
        Primary,
        Success,
        Info,
        Warning,
        Danger
    }

    public enum ELButtonSize
    {
        Default,
        Medium,
        Small,
        Mini
    }

    public enum ELButtonCorners
    {
        Default,
        Round,
        Circle
    }
}
