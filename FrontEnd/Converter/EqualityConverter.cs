using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontEnd.Converter
{
    public class EqualityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return false;
            int output1 = 0;
            int output2 = 0;
            if (int.TryParse(values[0].ToString(),out output1)
                && int.TryParse(values[1].ToString(), out output2))
            {
                return output1 == output2;
            }else
            {
                return values[0].Equals(values[1]);
            }
           
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}