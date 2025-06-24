using System;
using System.Globalization;
using System.Windows.Data;

namespace minhnqWPF.Converters
{
    public class LoginLabelConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isEmployee && parameter is string defaultLabel)
            {
                return isEmployee ? defaultLabel : "Phone Number";
            }
            return "Username";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 