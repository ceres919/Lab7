using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace GraphicsEditor.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string color && targetType.IsAssignableTo(typeof(IBrush)) == true)
            {
                return new SolidColorBrush(Color.Parse(color));
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
