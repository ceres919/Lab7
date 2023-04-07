using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace GraphicsEditor.Converters
{
    public class StringToGeometryConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string str_gem && targetType.IsAssignableTo(typeof(Geometry)) == true)
            {
                Geometry gem;
                gem = Geometry.Parse(str_gem);
                
                return gem;
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
