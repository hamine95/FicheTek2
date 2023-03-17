using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontEnd.Converter
{
    public class BooleanToCheckedConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "X" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}