using System;
using System.Globalization;
using System.Windows.Data;

namespace SignPDF.Converters
{
    internal class BoolInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue;
            }
            throw new ArgumentException("value is not bool");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue;
            }
            throw new ArgumentException("value is not bool");
        }
    }
}