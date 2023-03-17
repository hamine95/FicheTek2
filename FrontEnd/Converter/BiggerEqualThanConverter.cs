using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontEnd.Converter
{
    public class BiggerEqualThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsEqualOrGreater = (int)value >= (int)parameter;
            return IsEqualOrGreater;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}