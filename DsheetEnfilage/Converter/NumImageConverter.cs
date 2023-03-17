using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DSheetEnfilage.Converter
{
    public class NumImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            if (val.Equals("1"))
                return "/Asset/Comp1.png";
            if (val.Equals("2"))
                return "/Asset/Comp2.png";
            if (val.Equals("3"))
                return "/Asset/Comp3.png";
            if (val.Equals("4"))
                return "/Asset/Comp4.png";
            if (val.Equals("5"))
                return "/Asset/Comp5.png";
            if (val.Equals("6"))
                return "/Asset/Comp6.png";
            if (val.Equals("7"))
                return "/Asset/Comp7.png";
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}