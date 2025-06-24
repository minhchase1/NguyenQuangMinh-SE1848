using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace minhnqWPF.Converters
{
    public class BooleanToVisibilityParameterConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool bValue)
            {
                bool invert = parameter != null && parameter.ToString()?.ToLower() == "invert";
                
                if (invert)
                {
                    bValue = !bValue;
                }
                
                return bValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                bool invert = parameter != null && parameter.ToString()?.ToLower() == "invert";
                bool result = visibility == Visibility.Visible;
                
                if (invert)
                {
                    result = !result;
                }
                
                return result;
            }
            return false;
        }
    }
} 