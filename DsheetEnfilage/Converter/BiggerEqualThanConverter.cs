using System;
using System.Globalization;
using System.Windows.Data;

namespace DSheetEnfilage.Converter
{
    public class BiggerEqualThanConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsEqualOrGreater = (int)value >= Int32.Parse(parameter.ToString()) ;
            return IsEqualOrGreater;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}