using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontEnd.Converter
{
    public class StringFormatConverter : IValueConverter
    {
        private static readonly StringFormatConverter instance = new StringFormatConverter();
        public static StringFormatConverter Instance
        {
            get
            {
                return instance;
            }
        }

        private StringFormatConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(new CultureInfo("fr-FR"), "{0:dd MMMM yyyy}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}