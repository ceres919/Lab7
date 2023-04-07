using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace GraphicsEditor.Converters
{
    public class StringToPointsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string str_point)
            {
                Points points = new Points();

                var split_points = str_point.Split(" ");
                foreach (var point in split_points)
                {
                    var st_point = point.Split(",");
                    var x = double.Parse(st_point[0], CultureInfo.InvariantCulture.NumberFormat);
                    var y = double.Parse(st_point[1], CultureInfo.InvariantCulture.NumberFormat);
                    points.Add(new Point(x, y));
                }
                return points;
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
