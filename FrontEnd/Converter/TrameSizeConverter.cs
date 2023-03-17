using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontEnd.Converter
{
    public class TrameSizeConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value/83 * double.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}