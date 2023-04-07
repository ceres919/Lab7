using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace GraphicsEditor.Converters
{
    public class StringToPointConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == "" || value == null) return null;
            if (value is string str_point && targetType.IsAssignableTo(typeof(Point)) == true)
            {
                var point = str_point.Split(',');
                var x = double.Parse(point[0], CultureInfo.InvariantCulture.NumberFormat);
                var y = double.Parse(point[1], CultureInfo.InvariantCulture.NumberFormat);
                return new Point(x, y);
            }
            string par = (string)parameter;
            if (value is string str_pointX && targetType.IsAssignableTo(typeof(double)) == true && par == "x")
            {
                var point = str_pointX.Split(',');
                var x = double.Parse(point[0], CultureInfo.InvariantCulture.NumberFormat);
                return x;
            }
            if (value is string str_pointY && targetType.IsAssignableTo(typeof(double)) == true && par == "y")
            {
                var point = str_pointY.Split(',');
                var x = double.Parse(point[1], CultureInfo.InvariantCulture.NumberFormat);
                return x;
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
