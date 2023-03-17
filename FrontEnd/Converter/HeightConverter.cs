using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontEnd.Converter
{
    public  class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            //var IsVisible = (bool)value ? Visibility.Visible : Visibility.Collapsed;
            if (parameter!=null && int.Parse(parameter.ToString())  == 2)
            {
                return ((double)value)/57*2;
            } else if (parameter != null && int.Parse(parameter.ToString()) == 4)
            {
                return ((double)value)/82;
            }else if (parameter != null && int.Parse(parameter.ToString()) == 5)
            {
                return ((double)value)/83;
            }else if (parameter != null && int.Parse(parameter.ToString()) == 6)
            {
                return ((double)value)/83*6;
            }
            else
            {
                return ((double)value)/59;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}