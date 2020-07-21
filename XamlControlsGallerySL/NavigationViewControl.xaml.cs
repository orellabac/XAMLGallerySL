using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AppUIBasics
{

    public class InvertVisibilityConverter : IValueConverter
    {

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                Visibility vis = (Visibility)value;
                return vis == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new InvalidOperationException("Converter can only convert to value of type Visibility.");
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new Exception("Invalid call - one way only");
        }
    }
    public partial class NavigationViewControl : UserControl
    {
        public NavigationViewControl()
        {
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        { 
          if (this.Items.Visibility == Visibility.Collapsed)
            {
                this.Items.Visibility = Visibility.Visible;
            }  
          else
            {
                this.Items.Visibility = Visibility.Collapsed;
            }
        }

        //  Dependency Property - Begin
        public const string ShowExpandPropertyName = "ShowExpand";
        public Visibility ShowExpand
        {
            get
            {
                var expandValue = (bool) GetValue(ShowExpandProperty);
                if (expandValue)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            set
            {
                if (value == Visibility.Collapsed)
                    SetValue(ShowExpandProperty, false);
                else
                    SetValue(ShowExpandProperty, true);
            }
        }
        public static readonly DependencyProperty ShowExpandProperty = DependencyProperty.Register(
                ShowExpandPropertyName, typeof(bool), typeof(NavigationViewControl), new PropertyMetadata(true));


        public const string ShowCollapsePropertyName = "ShowCollapse";
        public Visibility ShowCollapse
        {
            get
            {
                var expandValue = (bool)GetValue(ShowCollapseProperty);
                if (expandValue)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            set
            {
                if (value == Visibility.Collapsed)
                    SetValue(ShowCollapseProperty, false);
                else
                    SetValue(ShowCollapseProperty, true);
            }
        }
        public static readonly DependencyProperty ShowCollapseProperty = DependencyProperty.Register(
                ShowCollapsePropertyName, typeof(bool), typeof(NavigationViewControl), new PropertyMetadata(false));

        ////  Dependency Property - End
    }
}
