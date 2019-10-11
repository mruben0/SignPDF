using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SignPDF.Converters
{
    public class InvertedBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Hidden : Visibility.Visible;
            }
            throw new ArgumentException("value is not bool");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibilityValue)
            {
                return visibilityValue != Visibility.Visible;
            }
            throw new ArgumentException("value is not Visibility");
        }
    }
}