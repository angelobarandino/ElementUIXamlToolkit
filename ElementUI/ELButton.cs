using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ElementUI
{
    public class ELButton : ButtonBase
    {
        public ELButton() : base()
        {
            this.SetValue(
                ELButton.TypeBackgroundKey,
                FindResource("ElementUI.DefaultColorBrush")
            );
        }

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(double),
            typeof(ELButton),
            new FrameworkPropertyMetadata(4.0,
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnCornerRadiusChanged
            )
        );

        public CornerRadius UniformCornerRadius
        {
            get => (CornerRadius)GetValue(UniformCornerRadiusProperty);
        }

        private static readonly DependencyPropertyKey UniformCornerRadiusKey = DependencyProperty.RegisterReadOnly(
            nameof(UniformCornerRadius),
            typeof(CornerRadius),
            typeof(ELButton),
            new UIPropertyMetadata(
                new CornerRadius(4)
            )
        );

        public static readonly DependencyProperty UniformCornerRadiusProperty = UniformCornerRadiusKey.DependencyProperty;

        public ElementType Type
        {
            get { return (ElementType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            nameof(Type),
            typeof(ElementType),
            typeof(ELButton),
            new FrameworkPropertyMetadata(
                ElementType.Default,
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnElementTypeIsChanged
            )
        );

        public Brush TypeBackground
        {
            get => (Brush)GetValue(TypeBackgroundProperty);
        }

        private static readonly DependencyPropertyKey TypeBackgroundKey = DependencyProperty.RegisterReadOnly(
            nameof(TypeBackground),
            typeof(Brush),
            typeof(ELButton),
            new UIPropertyMetadata()
        );

        public static readonly DependencyProperty TypeBackgroundProperty = TypeBackgroundKey.DependencyProperty;

        public ElementSize Size
        {
            get { return (ElementSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            nameof(Size),
            typeof(ElementSize),
            typeof(ELButton),
            new FrameworkPropertyMetadata(
                ElementSize.Default,
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnElementTypeIsChanged
            )
        );

        public bool Plain
        {
            get { return (bool)GetValue(PlainProperty); }
            set { SetValue(PlainProperty, value); }
        }

        public static readonly DependencyProperty PlainProperty = DependencyProperty.Register(
            nameof(Plain),
            typeof(bool),
            typeof(ELButton),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.AffectsRender,
                null,
                PlainCoerceCallback
            )
        );

        public bool Rounded
        {
            get { return (bool)GetValue(RoundedProperty); }
            set { SetValue(RoundedProperty, value); }
        }

        public static readonly DependencyProperty RoundedProperty = DependencyProperty.Register(
            nameof(Rounded), 
            typeof(bool), 
            typeof(ELButton), 
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.AffectsRender,
                null,
                RoundedCoerceCallback
            )
        );


        private static void OnElementTypeIsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ELButton elButton)
            {
                if (e.NewValue is ElementType type)
                {
                    elButton.SetValue(
                        ELButton.TypeBackgroundKey,
                        elButton.GetButtonBrushType()
                    );

                    elButton.CoerceValue(ELButton.PlainProperty);
                }

                if (e.NewValue is ElementSize)
                {
                    elButton.CoerceValue(ELButton.RoundedProperty);
                }
            }
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ELButton elButton)
            {
                elButton.SetValue(
                    ELButton.UniformCornerRadiusKey,
                    new CornerRadius((double)e.NewValue)
                );
            }
        }

        private static object RoundedCoerceCallback(DependencyObject d, object baseValue)
        {
            if (d is ELButton elButton)
            {
                var radiusSize =
                    elButton.Size == ElementSize.Tiny ? 14 :
                    elButton.Size == ElementSize.Small ? 16 :
                    elButton.Size == ElementSize.Medium ? 18 : 20;

                elButton.SetValue(
                    ELButton.CornerRadiusProperty,
                    Convert.ToDouble(radiusSize)
                );
            }

            return baseValue;
        }

        private static object PlainCoerceCallback(DependencyObject d, object baseValue)
        {
            if (d is ELButton elButton)
            {
                if (baseValue is bool isPlain && isPlain)
                {
                    var type = elButton.Type;
                    var brush = elButton.GetButtonBrushType();

                    var color = AdjustColorBrightness(brush.Color, 0.9);
                    var backgroundBrush = new SolidColorBrush(color);
                    
                    elButton.SetValue(ELButton.TypeBackgroundKey, backgroundBrush);

                    if (type != ElementType.Default)
                    {
                        elButton.SetValue(ELButton.ForegroundProperty, brush);
                        elButton.SetValue(ELButton.BorderBrushProperty, brush);
                    }
                }
            }

            return baseValue;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            if (Plain)
            {
                if (Type == ElementType.Default)
                {
                    var primaryBrush = FindResource("ElementUI.PrimaryColorBrush") as SolidColorBrush;
                    SetValue(ELButton.BorderBrushProperty, primaryBrush);
                    SetValue(ELButton.ForegroundProperty, primaryBrush);
                }
                else
                {
                    var brush = GetButtonBrushType();
                    SetValue(ELButton.TypeBackgroundKey, brush);
                    SetValue(ELButton.ForegroundProperty, new SolidColorBrush(Colors.White));
                }
            }
            else
            {
                Opacity = 0.7;
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (Plain)
            {
                if (Type == ElementType.Default)
                {
                    SetValue(ELButton.BorderBrushProperty, FindResource("ElementUI.BaseBorderBrush"));
                    SetValue(ELButton.ForegroundProperty, FindResource("ElementUI.RegularTextBrush"));
                }
                else
                {
                    var br = TypeBackground;
                    byte a = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).A;
                    byte r = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).R;
                    byte b = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).B;
                    byte g = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).G;

                    var color = AdjustColorBrightness(
                        Color.FromArgb(a, r, g, b), 0.9
                    );

                    SetValue(ELButton.ForegroundProperty, TypeBackground);
                    SetValue(ELButton.TypeBackgroundKey, new SolidColorBrush(color));
                }
            }
            else
            {
                Opacity = 1;
            }
        }

        private SolidColorBrush GetButtonBrushType()
        {
            return FindResource($"ElementUI.{Type}ColorBrush") as SolidColorBrush; ;
        }

        private static Color AdjustColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }

    public enum ElementType
    {
        Default,
        Primary,
        Success,
        Warning,
        Danger
    }

    public enum ElementSize
    {
        Default,
        Medium,
        Small,
        Tiny
    }
}
