using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontEnd.Converter
{
    public class BooleanToVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var IsVisible = (bool)value ? Visibility.Visible : Visibility.Collapsed;
            return IsVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}