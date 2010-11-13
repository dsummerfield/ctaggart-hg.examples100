using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RyoushinSamples.Converters
{
public class AreaConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is PointCollection)
        {
            double area = Pacem.Science.Geometry.EuclideanGeometry.Area2D((PointCollection)value);
            if (targetType == typeof(double))
                return area;
            if (targetType == typeof(string))
            {
                if (parameter is string)
                    return area.ToString((string)parameter, culture);
                else
                    return area.ToString(culture);
            }
        }
        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}
