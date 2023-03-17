using System;
using System.Globalization;
using System.Windows.Data;

namespace DSheetEnfilage.Converter
{
    public class MultipleRadioButtonConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Int32.Parse(parameter.ToString()) ==(int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : null;    
        }
    }
}