using System.Windows;
using System.Windows.Controls;

namespace ElementUI.Controls
{
    public partial class ELButton : Button
    {
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
                new PropertyMetadata(ELButtonType.Default));

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