using System;
using System.Windows;
using System.Windows.Controls;

namespace ElementUI
{
    public class Spacer
    {
        public static int GetAll(DependencyObject obj)
        {
            return (int)obj.GetValue(AllProperty);
        }

        public static void SetAll(DependencyObject obj, int value)
        {
            obj.SetValue(AllProperty, value);
        }

        public static readonly DependencyProperty AllProperty = DependencyProperty
            .RegisterAttached("All",
                typeof(int),
                typeof(Spacer),
                new UIPropertyMetadata(0,
                    HandleChangedCallback
                )
            );

        public static bool GetIgnoreFirstChild(DependencyObject obj)
        {
            return (bool)obj.GetValue(IgnoreFirstChildProperty);
        }

        public static void SetIgnoreFirstChild(DependencyObject obj, bool value)
        {
            obj.SetValue(IgnoreFirstChildProperty, value);
        }

        public static readonly DependencyProperty IgnoreFirstChildProperty = DependencyProperty
            .RegisterAttached("IgnoreFirstChild",
                typeof(bool),
                typeof(Spacer),
                new UIPropertyMetadata(false,
                    HandleChangedCallback
                )
            );

        private static void HandleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Panel panel)
            {
                panel.Loaded -= Panel_Loaded;
                panel.Loaded += Panel_Loaded;
            }
        }

        private static void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Panel panel)
            {
                var ignoreFistChild = GetIgnoreFirstChild(panel);

                for (int i = 0; i < panel.Children.Count; i++)
                {
                    if (ignoreFistChild && i == 0)
                        continue;

                    var child = panel.Children[i];
                    if (child is FrameworkElement childFE)
                    {
                        var all = GetAll(panel);
                        var thickness = new Thickness(all);
                        childFE.Margin = thickness;
                    }
                }
            }
        }

        //public class SpacerOnly
        //{
        //    public int GetLeft(DependencyObject obj)
        //    {
        //        return (int)obj.GetValue(LeftProperty);
        //    }
            
        //    public void SetLeft(DependencyObject obj, int value)
        //    {
        //        obj.SetValue(LeftProperty, value);
        //    }

        //    public static readonly DependencyProperty LeftProperty = DependencyProperty
        //        .RegisterAttached("Left",
        //            typeof(int),
        //            typeof(SpacerOnly),
        //            new UIPropertyMetadata(0)
        //        );
        //}
    }
}
